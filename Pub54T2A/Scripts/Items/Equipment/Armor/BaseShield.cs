using System;
using Server.Network;

namespace Server.Items
{
    public class BaseShield : BaseArmor
    {
        public BaseShield(int itemID)
            : base(itemID)
        {
        }

        public BaseShield(Serial serial)
            : base(serial)
        {
        }

        public override ArmorMaterialType MaterialType
        {
            get
            {
                return ArmorMaterialType.Plate;
            }
        }
        public override double ArmorRating
        {
            get
            {
                Mobile m = this.Parent as Mobile;
                double ar = base.BaseArmorRating;

                if (m != null)
                    return ((m.Skills[SkillName.Parry].Value * ar) / 200.0) + 1.0;
                else
                    return ar;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version < 1)
            {
                if (this is Aegis)
                    return;

                // The 15 bonus points to resistances are not applied to shields on OSI.
                this.PhysicalBonus = 0;
                this.FireBonus = 0;
                this.ColdBonus = 0;
                this.PoisonBonus = 0;
                this.EnergyBonus = 0;
            }
        }

        public override int OnHit(BaseWeapon weapon, int damage)
        {
            Mobile owner = this.Parent as Mobile;

            if (owner == null)
                return damage;

            if (Utility.Random(4) == 1)
                owner.CheckSkill(SkillName.Parry, 0.0, 100.0);

            double skil = ((owner.Skills[SkillName.Parry].Value / 100 * 0.2));
            if (skil <= 0.00)
                skil = 0.00;
            // Console.WriteLine("{0}",skil);  debug patch
            bool archery = weapon.Skill == SkillName.Archery;
            double ar = this.ArmorRatingScaled;

            damage = (int)(archery ? (damage * 0.35) : (damage * 0.85));

            if (damage < 0)
                damage = 0;

            owner.FixedEffect(0x37B9, 10, 16);

            if (HitPoints > 0 && Utility.RandomDouble() < .70 - skil) // 70 % minus 20 percent chance (at gm parry) to lose durability
            {
                int wear = 1;

                if (weapon.Type == WeaponType.Bashing)
                    wear = 3;

                if (wear > 0)
                {
                    if (HitPoints > wear)
                    {
                        HitPoints -= wear;

                        if (HitPoints == 10 || HitPoints == 6 || HitPoints == 2)
                        {
                            if (Parent is Mobile)
                                ((Mobile)Parent).LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Your shield is severely damaged.");
                        }
                    }
                    else
                    {
                        if (Parent is Mobile)
                            ((Mobile)Parent).LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Your shield has been destroyed!");
                        Delete();
                    }
                }
            }
            return damage;
        }
    }
}