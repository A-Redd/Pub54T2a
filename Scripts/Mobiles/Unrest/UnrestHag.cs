using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a festering corpse")]
    public class Hag : BaseCreature
    {
        [Constructable]
        public Hag()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.18, 0.3)
        {
            this.Name = "a festering hag";
            this.Body = 310;
            this.BaseSoundID = 0x482;

            this.Hue = 1401;

            this.SetStr(400);
            this.SetDex(75);
            this.SetInt(200);

            this.SetHits(745);

            this.SetDamage(20);


            this.SetSkill(SkillName.MagicResist, 75, 100.0);
            this.SetSkill(SkillName.Tactics, 100.0, 100.0);
            this.SetSkill(SkillName.Wrestling, 100.0, 100.0);
            this.SetSkill(SkillName.Magery, 90.1, 99.0);

            LootTier(14);

            this.Fame = 14500;
            this.Karma = -14500;

            this.VirtualArmor = 20;
        }

        public Hag(Serial serial)
            : base(serial)
        {
        }

        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich);
            this.AddLoot(LootPack.Average);
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