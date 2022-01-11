using System;

namespace Server.ACC.CSS.Systems.Ancient
{
    public class NecroClingingDarknesScroll : CSpellScroll
    {
        [Constructable]
        public NecroClingingDarknesScroll()
            : this(1)
        {
        }

        [Constructable]
        public NecroClingingDarknesScroll(int amount)
            : base(typeof(AncientAwakenAllSpell), 0x1F2E, amount)
        {
            Name = "Clinging Darkness";
            Hue = 1355;
        }

        public NecroClingingDarknesScroll(Serial serial)
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
