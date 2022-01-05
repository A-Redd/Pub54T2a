using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a balron corpse")]
    public class Balron : BaseCreature
    {
        [Constructable]
        public Balron()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.185, 0.3)
        {
            this.Name = NameList.RandomName("balron");
            this.Body = 40;
            this.BaseSoundID = 357;

            this.SetStr(200, 200);
            this.SetDex(100, 100);
            this.SetInt(151, 250);

            this.SetHits(700);

            this.SetDamage(30);

            this.Hue = Utility.RandomMinMax(1106, 1110);

            SetSkill(SkillName.Wrestling, 90.1, 100);
            SetSkill(SkillName.Tactics, 90.1, 100);
            SetSkill(SkillName.MagicResist, 90.1, 100);
            SetSkill(SkillName.Magery, 90.1, 100);

            this.Fame = 24000;
            this.Karma = -24000;

            LootTier(16);

            VirtualArmor = Utility.RandomMinMax(18, 33);
        }

        public Balron(Serial serial)
            : base(serial)
        {
        }

        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Deadly;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 5;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }

        
       
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich, 2);
            this.AddLoot(LootPack.Rich);
            this.AddLoot(LootPack.MedScrolls, 2);
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