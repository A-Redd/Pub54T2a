using System;
using Server;

namespace Server.ACC.CSS.Systems.Necro
{
    public class NecroInitializer : BaseInitializer
    {
        public static void Configure()
        {

            Register(typeof(NecroClingingDarknessSpell), "Clinging Darkness", "The caster calls upon the dark powers of the dead to smother their target in a corosive tar.", null, "Mana: 20; Skill: 30;", 2295, 3500, School.Necro);
            Register(typeof(NecroReanimatedBonesSpell), "Reanimated Bones", "The caster calls upon the dark powers of the dead summon an undead defender.", null, "Mana: 45; Skill: 51;", 2295, 3500, School.Necro);
            Register(typeof(NecroMendBonesSpell), "MendBones", "The caster calls upon the dark powers of the dead summon an undead defender.", null, "Mana: 30; Skill: 40;", 2295, 3500, School.Necro);
            //Lifetap 10 damage heal 10 damage
            //banshee 100 hp caster casts necro spells.
            //poisonbolt -4-10 hp 10 ticks at *rank

            //rares:
            //boil blood; 20 per tick dot. 10 ticks.
            //trall of bones(charm)

        }
    }
}
