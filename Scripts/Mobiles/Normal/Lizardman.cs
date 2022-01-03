using System;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("a lizardman corpse")]
    public class Lizardman : BaseCreature
    {
        [Constructable]
        public Lizardman()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = NameList.RandomName("lizardman");
            this.Body = Utility.RandomList(35, 36);
            this.BaseSoundID = 417;

            this.SetStr(96, 120);
            this.SetDex(86, 105);
            this.SetInt(36, 60);

            this.SetHits(58, 72);

            this.SetDamage(8, 14);

            this.SetSkill(SkillName.MagicResist, 35.1, 60.0);
            this.SetSkill(SkillName.Tactics, 55.1, 80.0);
            this.SetSkill(SkillName.Wrestling, 50.1, 70.0);

            this.Fame = 1500;
            this.Karma = -1500;

            this.VirtualArmor = 28;
        }

        public Lizardman(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType
        {
            get
            {
                return InhumanSpeech.Lizardman;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override int Hides
        {
            get
            {
                return 12;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Spined;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Meager);
            // TODO: weapon
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