
#region References
using System;
using Server.Items;
#endregion

namespace Server.Mobiles
{
	[CorpseName("a ghoul")]
	public class UnrestGhoul : BaseCreature
	{
		[Constructable]
		public UnrestGhoul()
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.3)
		{
			Name = "a ghoul";
			Body = 0xB5;
			//BaseSoundID = 0x45A;
            Hue = 1437;

			SetStr(200);
			SetDex(125);
			SetInt(25);

			SetHits(475);
			SetMana(100);
            
			SetDamage(20);
            VirtualArmor = 30;

            LootTier(15);

            SetSkill(SkillName.Tactics, 80.1, 95.0);
			SetSkill(SkillName.Wrestling, 100.0, 100.0);
            SetSkill(SkillName.MagicResist, 75.0, 100.0);

            Fame = 11500;
			Karma = -11500;

		}

		public UnrestGhoul(Serial serial)
			: base(serial)
		{ }

        public override int Meat
        {
            get
            {
                return 1;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich,2);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Meager);
        }

        public override int GetAttackSound() { return 0x65C; }

        public override int GetAngerSound() { return 0x65d; }

        public override int GetDeathSound() { return 0x65E;  }
                       
        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.25 > Utility.RandomDouble())
            {
                defender.FixedEffect(0x37B9, 10, 5);
                defender.PlaySound(0x5BD);
                defender.Freeze(TimeSpan.FromSeconds(6.0));
                defender.SendAsciiMessage("your feet are rooted to the ground!");                           
            }
        }

        public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}