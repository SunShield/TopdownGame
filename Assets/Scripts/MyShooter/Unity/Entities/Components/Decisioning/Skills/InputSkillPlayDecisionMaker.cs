using MyShooter.Core.Entities.States;
using MyShooter.Core.GameInput;

namespace MyShooter.Unity.Entities.Components.Decisioning.Skills
{
	public class InputSkillPlayDecisionMaker : SkillPlayDecisionMaker
	{
		protected override SkillType? DecideSkillPlayInternal()
		{
			if (BasicInput.DashButtonPressed) return SkillType.Dash;
			if (BasicInput.MainSkillButtonPressed) return SkillType.Main;
			if (BasicInput.SecondarySkillButtonPressed) return SkillType.Secondary;
			if (BasicInput.SigilSkillButtonPressed) return SkillType.Sigil;
			if (BasicInput.UtilitySkillButtonPressed) return SkillType.Utility;
			if (BasicInput.SignatureSkillButtonPressed) return SkillType.Signature;

			return null;
		}
	}
}
