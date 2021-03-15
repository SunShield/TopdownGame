using MyShooter.Core.Entities.States;

namespace MyShooter.Core.Environment.Events.Entity.EffectReactional
{
	public class EntityDeathEventArgs : GameEventArgs
	{
		public EntityType Type;
	}
}
