using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Necromancy;

namespace Server.Items
{
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

        public static void DoDisease(Mobile m, Mobile from)
        {
            int level = 0;
            level = from.TithingPoints;
            if (m.Alive)
            {
                int damage = Utility.RandomMinMax(level +1 , level +1 * 2);

                m.PlaySound(0x133);
                m.Hits -= damage;

                 m.PrivateOverheadMessage(MessageType.Regular, 1882, true, "-" + (damage), from.NetState);//onhitgiven
                // m.PrivateOverheadMessage(MessageType.Regular, 1882, true, "-" + (damage), from.NetState);//onhit

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
                m.SendAsciiMessage("The disease ends"); 
        }
		

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!Validate(attacker) || !CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);


            if (defender is PlayerMobile)
            {
                defender.SendAsciiMessage("you have been diseased!"); 
            }


            defender.PlaySound(0x5CC);
            defender.FixedParticles(0x377A, 1882, 25, 9950, 31, 0, EffectLayer.Waist);
			
			BeginDisease(defender, attacker);
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_From;
            private readonly Mobile m_Mobile;
            private int m_Count;
            public InternalTimer(Mobile from, Mobile m)
                : base(TimeSpan.FromSeconds(8.0), TimeSpan.FromSeconds(12.0))
            {
                m_From = from;
                m_Mobile = m;
                Priority = TimerPriority.TwoFiftyMS;
			}

            protected override void OnTick()
            {
                DoDisease(m_Mobile, m_From);

                if (++m_Count == 4)
                    EndDisease(m_Mobile, true);
            }
        }
    }
}