using System;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a solen warrior corpse")]
    public class RedSolenWarrior : BaseCreature
    {
        private bool m_BurstSac;
        [Constructable]
        public RedSolenWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a red solen warrior";
            this.Body = 782;
            this.BaseSoundID = 959;

            this.SetStr(196, 220);
            this.SetDex(101, 125);
            this.SetInt(36, 60);

            this.SetHits(96, 107);

            this.SetDamage(5, 15);

            this.SetDamageType(ResistanceType.Physical, 80);
            this.SetDamageType(ResistanceType.Poison, 20);

            this.SetResistance(ResistanceType.Physical, 20, 35);
            this.SetResistance(ResistanceType.Fire, 20, 35);
            this.SetResistance(ResistanceType.Cold, 10, 25);
            this.SetResistance(ResistanceType.Poison, 20, 35);
            this.SetResistance(ResistanceType.Energy, 10, 25);

            this.SetSkill(SkillName.MagicResist, 60.0);
            this.SetSkill(SkillName.Tactics, 80.0);
            this.SetSkill(SkillName.Wrestling, 80.0);

            this.Fame = 3000;
            this.Karma = -3000;

            this.VirtualArmor = 35;

            SolenHelper.PackPicnicBasket(this);
            this.PackItem(new ZoogiFungus((0.05 < Utility.RandomDouble()) ? 3 : 13));

            if (Utility.RandomDouble() < 0.05)
                this.PackItem(new BraceletOfBinding());
        }

        public RedSolenWarrior(Serial serial)
            : base(serial)
        {
        }

        public bool BurstSac
        {
            get
            {
                return this.m_BurstSac;
            }
        }
        public override int GetAngerSound()
        {
            return 0xB5;
        }

        public override int GetIdleSound()
        {
            return 0xB5;
        }

        public override int GetAttackSound()
        {
            return 0x289;
        }

        public override int GetHurtSound()
        {
            return 0xBC;
        }

        public override int GetDeathSound()
        {
            return 0xE4;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich);
            this.AddLoot(LootPack.Gems, Utility.RandomMinMax(1, 4));
        }

        public override bool IsEnemy(Mobile m)
        {
            if (SolenHelper.CheckRedFriendship(m))
                return false;
            else
                return base.IsEnemy(m);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            SolenHelper.OnRedDamage(from);

            if (!willKill)
            {
                if (!this.BurstSac)
                {
                    if (this.Hits < 50)
                    {
                        this.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "* The solen's acid sac is burst open! *");
                        this.m_BurstSac = true;
                    }
                }
                else if (from != null && from != this && this.InRange(from, 1))
                {
                    this.SpillAcid(from, 1);
                }
            }

            base.OnDamage(amount, from, willKill);
        }

        public override bool OnBeforeDeath()
        {
            this.SpillAcid(4);

            return base.OnBeforeDeath();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1);
            writer.Write(this.m_BurstSac);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			
            switch( version )
            {
                case 1:
                    {
                        this.m_BurstSac = reader.ReadBool();
                        break;
                    }
            }
        }
    }
}