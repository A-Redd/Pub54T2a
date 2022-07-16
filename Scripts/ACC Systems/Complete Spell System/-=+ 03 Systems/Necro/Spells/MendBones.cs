using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Necro
{
    public class NecroMendBonesSpell : NecroSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                                                        "Mend Bones", "Mend Bones",
                                                        //SpellCircle.Eighth,
                                                        212,
                                                        9041
                                                       );

        public override SpellCircle Circle
        {
            get { return SpellCircle.Fourth; }
        }

       // public override int RequiredTithing { get { return 40; } }
        public override double RequiredSkill { get { return 40.0; } }

        public NecroMendBonesSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override TimeSpan GetCastDelay()
        {
            return TimeSpan.FromSeconds(4.5);
        }

        public void Target(Mobile m)
        {
            double mod = (Caster.Skills[HealSkill].Value + Caster.Skills[CastSkill].Value)+10;
            double scalar = 2.5;
            int modint = (int)mod; 

            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (!m.Alive) //new message
            {
                Caster.SendAsciiMessage("Target must be living!"); // Target is not dead.
            }
            else if (!Caster.InRange(m, 4))
            {
                Caster.SendLocalizedMessage(501042); // Target is not close enough.
            }
            else if (CheckBSequence(m, true))
            {
                SpellHelper.Turn(Caster, m);

                m.PlaySound(0x5C9);
                m.FixedParticles(0x376A, 1, 63, 0x480, 3, 1194, EffectLayer.Waist);
                m.FixedParticles(0x3779, 1, 63, 9502, 5, 1194, EffectLayer.Waist);

                Caster.FixedParticles(0x376A, 1, 63, 0x480, 3, 1194, EffectLayer.Waist);
                Caster.FixedParticles(0x3779, 1, 63, 9502, 5, 1194, EffectLayer.Waist);
                Caster.Hits -= (40 -modint /10);
                m.Heal((modint /5) +25, Caster);

                if (m is BaseCreature && ((BaseCreature)m).Controlled && ((BaseCreature)m).ControlMaster == Caster)
                {
                    m.Heal(modint/8);
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private NecroMendBonesSpell m_Owner;

            public InternalTarget(NecroMendBonesSpell owner) : base(12, false, TargetFlags.Beneficial)
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
