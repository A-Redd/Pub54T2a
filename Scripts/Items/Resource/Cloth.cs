using System;
using Server.Network;

namespace Server.Items
{
    [FlipableAttribute(0x1766, 0x1768)]
    public class Cloth : Item, IScissorable, IDyable, ICommodity
    {
        [Constructable]
        public Cloth()
            : this(1)
        {
        }

        [Constructable]
        public Cloth(int amount)
            : base(0x1766)
        {
            this.Stackable = true;
            this.Amount = amount;
        }

        public Cloth(Serial serial)
            : base(serial)
        {
        }

        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
        }
        int ICommodity.DescriptionNumber
        {
            get
            {
                return this.LabelNumber;
            }
        }
        bool ICommodity.IsDeedable
        {
            get
            {
                return true;
            }
        }
        public bool Dye(Mobile from, DyeTub sender)
        {
            if (this.Deleted)
                return false;

            this.Hue = sender.DyedHue;

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void AppendClickName(System.Text.StringBuilder sb, bool plural)
        {
            if(this.Amount > 1)
            sb.Append("yards of cut cloth");

            else sb.Append("cut cloth");
        }


        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (this.Deleted || !from.CanSee(this))
                return false;

            base.ScissorHelper(from, new Bandage(), 1);

            return true;
        }
    }
}