using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.ACC.CSS.Systems.Necro
{
    public class NecroClingingDarknessSpell : NecroSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                                                        "Clinging Darkness", "*Clinging Darkness*",
                                                        203,
                                                        9051,
                                                        Reagent.Nightshade,
                                                        Reagent.MandrakeRoot,
                                                        Reagent.Bloodmoss
                                                        //Reagent.SulfuricAsh
                                                       );

        public override SpellCircle Circle
        {
            get { return SpellCircle.Third; }
        }

        public NecroClingingDarknessSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (CheckSequence())
                Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHSequence(m))
            {
                if (this.Scroll != null)
                    Scroll.Consume();
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                if (CheckResisted(m))
                {
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }
                else
                {
                    if (!m_Table.Contains(m))
                    {
                        Timer t = new InternalTimer(m, Caster);
                        t.Start();

                        m_Table[m] = t;
                    }

                    m.SendMessage("Clinging darkness!");
                    m.FixedParticles(0x91B, 1, 240, 9916, 0, 3, EffectLayer.Head);
                    m.PlaySound(0x595);
                }
            }

            FinishSequence();
        }

        public virtual bool CheckResisted(Mobile target)
        {
            double n = GetResistPercent(target, Circle);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)Circle) * 10;
            maxSkill += (1 + ((int)Circle / 6)) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill)
                target.CheckSkill(SkillName.MagicResist, 0.0, 120.0);

            return (n >= Utility.RandomDouble());
        }

        public virtual double GetResistPercent(Mobile target, SpellCircle circle)
        {
            double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
            double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
        }

        private static Hashtable m_Table = new Hashtable();

        public static void RemoveCurse(Mobile m)
        {
            Timer t = (Timer)m_Table[m];

            if (t == null)
                return;

            t.Stop();
            m.SendMessage("Clinging darkness has worn off.");

            m_Table.Remove(m);
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Target, m_From;
            private double m_MinBaseDamage, m_MaxBaseDamage;

            private DateTime m_NextHit;
            private int m_HitDelay;

            private int m_Count, m_MaxCount;

            public InternalTimer(Mobile target, Mobile from)
                : base(TimeSpan.FromSeconds(4.0), TimeSpan.FromSeconds(4.0))
            {
                Priority = TimerPriority.FiftyMS;

                m_Target = target;
                m_From = from;

                double eval = from.Skills[SkillName.EvalInt].Value / 50;
                double foren = from.Skills[SkillName.Forensics].Value / 75;

                m_MinBaseDamage = 4 * (eval + foren +1);
                m_MaxBaseDamage = 8 * (eval + foren +1);

                m_HitDelay = 5;    
                m_NextHit = DateTime.Now + TimeSpan.FromSeconds(m_HitDelay);

                m_Count += (int)(from.Skills[SkillName.EvalInt].Value / 20);
                m_Count += (int)(from.Skills[SkillName.Forensics].Value / 20);

                if (m_Count < 5)
                    m_Count = 5;

                m_MaxCount = m_Count;
            }

            protected override void OnTick()
            {
                if (!m_Target.Alive)
                {
                    m_Table.Remove(m_Target);
                    Stop();
                }

                if (!m_Target.Alive || DateTime.Now < m_NextHit)
                    return;

                --m_Count;

                if (m_HitDelay > 1)
                {
                    m_Target.FixedParticles(0x91B, 1, 240, 9916, 1159, 3, EffectLayer.Head);
                    m_Target.PlaySound(0x595);
                    if (m_MaxCount < 5)
                    {
                        --m_HitDelay;
                    }
                    else
                    {
                        int delay = (int)(Math.Ceiling((1.0 + (5 * m_Count)) / m_MaxCount));

                        if (delay <= 5)
                            m_HitDelay = delay;
                        else
                            m_HitDelay = 5;
                    }
                }
      
                if (m_Count == 2)
                {
                    m_From.SendMessage("Your Clinging Darkeness spell is about to wear off!.");
                }

                if (m_Count == 0)
                {
                    m_Target.SendMessage("Clinging Darkness has fallen off..");
                    m_Table.Remove(m_Target);
                    Stop();
                }
            
                {
                    m_NextHit = DateTime.Now + TimeSpan.FromSeconds(m_HitDelay);

                    double damage = m_MinBaseDamage + (Utility.RandomDouble() * (m_MaxBaseDamage - m_MinBaseDamage));                    

                    if (damage < 1)
                        damage = 1;

                    AOS.Damage(m_Target, m_From, (int)damage, 0, 0, 0, 100, 0);
                }
            }
        }

        public override TimeSpan GetCastDelay()
        {
            return TimeSpan.FromSeconds(2.5);
        }

        private class InternalTarget : Target
        {
            private NecroClingingDarknessSpell m_Owner;

            public InternalTarget(NecroClingingDarknessSpell owner)
                : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
