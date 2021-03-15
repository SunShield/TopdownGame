using MyShooter.Unity.Entities;

namespace MyShooter.Core.Environment.Events.Entity.EffectReactional
{
	public class EntityCollideEventArgs : GameEventArgs
	{
		public BattleEntity CollidedEntity;
	}
}
