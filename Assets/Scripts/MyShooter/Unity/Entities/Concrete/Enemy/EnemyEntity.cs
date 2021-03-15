using MyShooter.Core.Entities.States;

namespace MyShooter.Unity.Entities.Concrete.Player
{
	public class EnemyEntity : BattleEntity
	{
		public override EntityType Type => EntityType.Enemy;

		protected override void InitializeAutomatically()
		{
			base.InitializeAutomatically();
			Registry.RegisterEnemy(this);
		}

		private void OnDestroy()
		{
			Registry.UnregisterEnemy(this);
		}
	}
}
