using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Necromancy;

namespace Server.Items
{
    public class Flurry : WeaponAbility
    {
        private static readonly Hashtable m_Table = new Hashtable();
        public Flurry()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 20;
            }
        }
		
		
		public static void BeginFlurry(Mobile m, Mobile from)
        {
            Timer t = (Timer)m_Table[m];

            if (t != null)
                t.Stop();

            t = new InternalTimer(from, m);
            m_Table[m] = t;

            t.Start();
        }

        public static void DoFlurry(Mobile m, Mobile from)
        {
            int level = 0;
            level = from.TithingPoints; 
                
            if (m.Alive)
            {
                int damage = Utility.RandomMinMax(level +1 , level +3 );

                m.PlaySound(0x13D);
                m.Damage(damage, from);                
            }
        }

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!Validate(attacker) || !CheckMana(attacker, true))
                return;

            ClearCurrentAbility(attacker);

            attacker.Emote("*Flurry of blows*");
        			
			BeginFlurry(defender, attacker);
        }

        public static void EndFlurry(Mobile m, bool message)
        {
            Timer t = (Timer)m_Table[m];

            if (t == null)
                return;

            t.Stop();
            m_Table.Remove(m);
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_From;
            private readonly Mobile m_Mobile;
            private int m_Count;
            public InternalTimer(Mobile from, Mobile m)
                : base(TimeSpan.FromSeconds(0.25), TimeSpan.FromSeconds(0.25))
            {
                m_From = from;
                m_Mobile = m;
                Priority = TimerPriority.TwoFiftyMS;
			}

            protected override void OnTick()
            {

                DoFlurry(m_Mobile, m_From);

                if (++m_Count == 4)
                    EndFlurry(m_Mobile, false);
                    
            }
        }
    }
}