using System;

namespace Server.Mobiles
{
    [CorpseName("a tentacle terror corpse")]
    public class UnrestTentacleTerror : BaseCreature
    {
        [Constructable]
        public UnrestTentacleTerror()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.185, 0.3)
        {
            this.Name = "a tentacle terror";
            this.Body = 66;
            this.BaseSoundID = 352;

            this.SetStr(400, 400);
            this.SetDex(75, 75);
            this.SetInt(200, 200);

            this.SetHits(1200);
            this.SetMana(200);

            this.SetDamage(30);

            this.SetSkill(SkillName.EvalInt, 45.1, 100.0);
            this.SetSkill(SkillName.Magery, 95.5, 100.0);
            this.SetSkill(SkillName.MagicResist, 100.5, 125.0);
            this.SetSkill(SkillName.Tactics, 90.1, 100.0);
            this.SetSkill(SkillName.Wrestling, 90.1, 100.0);

            this.Fame = 31000;
            this.Karma = -31000;

            this.VirtualArmor = 28;
            LootTier(22);
            this.PackReg(25);
            this.PackReg(25);
            this.PackReg(25);
        }

        public UnrestTentacleTerror(Serial serial)
            : base(serial)
        {
        }



        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Greater;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich,2);
            this.AddLoot(LootPack.FilthyRich);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGotMeleeAttack(defender);

            if (0.20 > Utility.RandomDouble())
            {
                //defender.FixedEffect(0x375A, 10, 5);
                this.PlaySound(0x5BF);             
                Rush(this);
                defender.FixedParticles(0x375A, 20, 50, 1194, EffectLayer.Waist);
                defender.SendAsciiMessage("you are hit by a powerful force!");
                defender.Damage(33);


            }
        }

        public override int TreasureMapLevel
        {
            get
            {
                return 5;
            }
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