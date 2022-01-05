//Iron Beetle Beta Release 
using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a beetle corpse")]
    public class UnrestBeetle : BaseCreature
    {
        [Constructable]
        public UnrestBeetle()
            : base(AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.185, 0.3)// AI Type??
        {
            Name = "a death beetle";
            Body = 714;
            Hue = 1012;

            SetStr(400, 400);
            SetDex(125, 125);
            SetInt(60);

            SetHits(350, 350);
            SetStam(125, 125);
            SetMana(60);

            SetDamage(15);

            SetSkill(SkillName.Anatomy, 80.6, 89.5);
            SetSkill(SkillName.MagicResist, 85.2, 90.4);
            SetSkill(SkillName.Tactics, 83.4, 96.4);
            SetSkill(SkillName.Wrestling, 90.0, 95.0);

            LootTier(11);

            this.TithingPoints = 7;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 91.1;

            QLPoints = 20;
        }

        public UnrestBeetle(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound() { return 0x66D; }
        public override int GetAttackSound() { return 0x66C; }
        public override int GetDeathSound() { return 0x65E; }

        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.Meat;
            }
        }

        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.Disease;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
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