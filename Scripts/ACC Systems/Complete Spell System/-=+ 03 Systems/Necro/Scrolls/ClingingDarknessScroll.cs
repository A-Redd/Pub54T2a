using System;

namespace Server.ACC.CSS.Systems.Necro
{
    public class NecroClingingDarknessScroll : CSpellScroll
    {
        [Constructable]
        public NecroClingingDarknessScroll()
            : this(1)
        {
        }

        [Constructable]
        public NecroClingingDarknessScroll(int amount)
            : base(typeof(NecroClingingDarknessSpell), 0x1F2E, amount)
        {
            Name = "Clinging Darkness";
            Hue = 1355;
        }

        public NecroClingingDarknessScroll(Serial serial)
            : base(serial)
        {
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
    }
}
