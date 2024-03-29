using System;

namespace Server.ACC.CSS.Systems.Necro
{
    public class NecroReanimatedBonesScroll : CSpellScroll
    {
        [Constructable]
        public NecroReanimatedBonesScroll()
            : this(1)
        {
        }

        [Constructable]
        public NecroReanimatedBonesScroll(int amount)
            : base(typeof(NecroReanimatedBonesSpell), 0x1F2E, amount)
        {
            Name = "Clinging Darkness";
            Hue = 1355;
        }

        public NecroReanimatedBonesScroll(Serial serial)
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
