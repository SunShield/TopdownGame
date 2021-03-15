using MyShooter.Core.Entities.States;

namespace MyShooter.Core.Environment.Events.Entity.Functional
{
	public class EntitySkillPlayDecidedEventArgs : GameEventArgs
	{
		public SkillType? Type;
	}
}
