using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Necro
{
    public class NecroReanimatedBonesSpell : NecroSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                                                        "Reanimated Bones", " ",
                                                        //SpellCircle.Third,
                                                        266,
                                                        9040,
                                                        false,
                                                        Reagent.BlackPearl,
                                                        Reagent.Bloodmoss,
                                                        CReagent.Bone
                                                       );

        public override SpellCircle Circle
        {
            get { return SpellCircle.Fifth; }
        }

        public override double CastDelay { get { return 9.0; } }
        public override double RequiredSkill { get { return 51.0; } }
        public override int RequiredMana { get { return 45; } }

        public NecroReanimatedBonesSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        private static Type[] m_Types = new Type[]
        {
            typeof( NecroSkeleton ),
            typeof( NecroSkeleton )

        };

        public override void OnCast()
        {
            PlayerMobile pm = (PlayerMobile)Caster;
            if (CheckSequence())
            {
                try
                {
                    
                    Type beasttype = (m_Types[Utility.Random(m_Types.Length)]);
                    double mod;
                    mod = Caster.Skills[SupportSkill].Value;
                    BaseCreature creaturea = (BaseCreature)Activator.CreateInstance(beasttype);

                    SpellHelper.Summon(creaturea, Caster, 0x215, TimeSpan.FromSeconds(20.0 * Caster.Skills[CastSkill].Value), false, false);

                    creaturea.SetSkill(SkillName.Anatomy, mod, mod);
                    creaturea.SetSkill(SkillName.Tactics, mod + 10 , mod + 50);
                    creaturea.SetSkill(SkillName.Wrestling, mod -10 , mod);
                    creaturea.VirtualArmor += (int)mod / 3;
                    creaturea.Str += (int)mod;
                    creaturea.HitsMaxSeed += (int)(75 + mod);
                    creaturea.Hits += 3000;
                    creaturea.SetDamage((int)mod /4);

                }
                catch
                {
                }
            }

            FinishSequence();
        }

        public override TimeSpan GetCastDelay()
        {
            return TimeSpan.FromSeconds(7.5);
        }
    }
}
