using System;

namespace Server.Items
{
    [FlipableAttribute(0x13ee, 0x13ef)]
    public class ChainArms : BaseArmor
    {
        [Constructable]
        public ChainArms()
            : base(0x13EE)
        {
            this.Weight = 10.0;
            this.Name = "chainmail sleeves";
            Hue = 1001;

        }

        public ChainArms(Serial serial)
            : base(serial)
        {
        }

        public override int BasePhysicalResistance
        {
            get
            {
                return 3;
            }
        }
        public override int BaseFireResistance
        {
            get
            {
                return 3;
            }
        }
        public override int BaseColdResistance
        {
            get
            {
                return 1;
            }
        }
        public override int BasePoisonResistance
        {
            get
            {
                return 5;
            }
        }
        public override int BaseEnergyResistance
        {
            get
            {
                return 3;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 40;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 50;
            }
        }
        public override int AosStrReq
        {
            get
            {
                return 40;
            }
        }
        public override int OldStrReq
        {
            get
            {
                return 20;
            }
        }
        public override int ArmorBase
        {
            get
            {
                return 26;
            }
        }
        public override ArmorMaterialType MaterialType
        {
            get
            {
                return ArmorMaterialType.Ringmail;
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

            if (this.Weight == 1.0)
                this.Weight = 15.0;
        }
    }
}