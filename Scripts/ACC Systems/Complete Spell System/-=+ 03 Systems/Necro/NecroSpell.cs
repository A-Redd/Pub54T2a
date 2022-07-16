using System;
using Server;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Necro
{
	public abstract class NecroSpell : CSpell
	{
		public NecroSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}
        
        public abstract SpellCircle Circle { get; }
        
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(3 * CastDelaySecondsPerTick); } }
        public override SkillName CastSkill { get { return SkillName.Necromancy; } }
        public override SkillName DamageSkill { get { return SkillName.EvalInt; } }
        public virtual SkillName SupportSkill { get { return SkillName.SpiritSpeak; } }
        public virtual SkillName MultiSkill { get { return SkillName.Forensics; } }
        public virtual SkillName HealSkill { get { return SkillName.Anatomy; } }

        public override bool ClearHandsOnCast { get { return true; } }

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill;
		}

		public override int GetMana()
		{
			return RequiredMana;
		}

		public override TimeSpan GetCastDelay()
		{
			return TimeSpan.FromSeconds( CastDelay );
		}
	}
}

