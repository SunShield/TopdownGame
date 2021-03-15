using MyShooter.Core.Entities.States;
using MyShooter.Core.Environment.Events.Entity.Functional;

namespace MyShooter.Unity.Entities.Components.Decisioning.Skills
{
	public class SkillPlayDecisionMaker : DecisionMaker
	{
		protected EntitySkillPlayDecidedEventArgs SkillArgs { get; private set; }
			= new EntitySkillPlayDecidedEventArgs() { Type = null };

		protected override void UpdateComponentInternal()
		{
			DecideSkillPlay();
		}

		protected void DecideSkillPlay()
		{
			var skillType = DecideSkillPlayInternal();
			if (skillType == null) return;

			SkillArgs.Type = skillType;
			EventBus.GetEvent<EntitySkillPlayDecidedEvent>().InvokeForId(GoId, SkillArgs);
		}

		protected virtual SkillType? DecideSkillPlayInternal() { return null; }
	}
}
