using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a skeletal corpse")]
    public class NecroSkeleton : BaseCreature
    {
        [Constructable]
        public NecroSkeleton()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.3)
        {
            this.Name = NameList.RandomName("summon");
            this.Body = Utility.RandomList(50, 56);
            this.BaseSoundID = 0x48D;

            this.SetStr(100);
            this.SetDex(100);
            this.SetInt(100);

            this.SetHits(50);

            this.SetDamage(15);

            this.SetSkill(SkillName.Tactics, 50);
            this.SetSkill(SkillName.Wrestling, 0);

            this.VirtualArmor = 0;
            this.ControlSlots = 5;
        }

        public override void OnGaveMeleeAttack(Mobile attacker)
        {
            Mobile tt = this.Combatant;

            if (Utility.RandomDouble() < 0.20)
            {
                tt.Combatant = this;
                tt.Say("*Taunted*");
            }

            base.OnGotMeleeAttack(attacker);
        }


        public NecroSkeleton(Serial serial)
            : base(serial)
        {

        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}