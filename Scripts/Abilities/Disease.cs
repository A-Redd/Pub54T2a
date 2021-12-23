using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Necromancy;

namespace Server.Items
{
    /// <summary>
    /// Make your opponent bleed profusely with this wicked use of your weapon.
    /// When successful, the target will bleed for several seconds, taking damage as time passes for up to ten seconds.
    /// The rate of damage slows down as time passes, and the blood loss can be completely staunched with the use of bandages. 
    /// </summary>
    public class Disease : WeaponAbility
    {
        private static readonly Hashtable m_Table = new Hashtable();
        public Disease()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 30;
            }
        }
		
		public static bool IsDiseased(Mobile m)
        {
            return m_Table.Contains(m);
        }
		
		public static void BeginDisease(Mobile m, Mobile from)
        {
            Timer t = (Timer)m_Table[m];

            if (t != null)
                t.Stop();

            t = new InternalTimer(from, m);
            m_Table[m] = t;

            t.Start();
        }

        public static void DoDisease(Mobile m, Mobile from, int level)
        {
            if (m.Alive)
            {
                int damage = Utility.RandomMinMax(level, level * 2);

                if (!m.Player)
                    damage *= 2;

                m.PlaySound(0x133);
                AOS.Damage(m, from, damage, false, 0, 0, 0, 0, 0, 0, 100, false, false, false);

                 m.PrivateOverheadMessage(MessageType.Regular, 1882, true, "-" + (damage), m.NetState);//onhitgiven
                 m.PrivateOverheadMessage(MessageType.Regular, 1882, true, "-" + (damage), from.NetState);//onhit

            }
            else
            {
                EndDisease(m, false);
            }
        }

        public static void EndDisease(Mobile m, bool message)
        {
            Timer t = (Timer)m_Table[m];

            if (t == null)
                return;

            t.Stop();
            m_Table.Remove(m);

            if (message)
                m.SendAsciiMessage("The disease has run its course"); 
        }
		

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!Validate(attacker) || !CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);

            // Who is immune?
            /*
            TransformContext context = TransformationSpellHelper.GetContext(defender);

            if ((context != null && (context.Type == typeof(LichFormSpell) || context.Type == typeof(WraithFormSpell))) ||
                (defender is BaseCreature && ((BaseCreature)defender).BleedImmune))
            {
                attacker.SendLocalizedMessage(1062052); // Your target is not affected by the bleed attack!
                return;
            }
            */
           // attacker.SendLocalizedMessage(1060159); // Your target is bleeding!
            defender.SendAsciiMessage("you are diseased!"); 

            if (defender is PlayerMobile)
            {
                defender.SendAsciiMessage("you have been diseased!"); 
            }

            defender.PlaySound(0x133);
            defender.FixedParticles(0x377A, 1882, 25, 9950, 31, 0, EffectLayer.Waist);
			
			BeginDisease(defender, attacker);
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_From;
            private readonly Mobile m_Mobile;
            private int m_Count;
            public InternalTimer(Mobile from, Mobile m)
                : base(TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(5.0))
            {
                m_From = from;
                m_Mobile = m;
                Priority = TimerPriority.TwoFiftyMS;
			}

            protected override void OnTick()
            {
                DoDisease(m_Mobile, m_From, 3 - m_Count);

                if (++m_Count == 3)
                    EndDisease(m_Mobile, true);
            }
        }
    }
}