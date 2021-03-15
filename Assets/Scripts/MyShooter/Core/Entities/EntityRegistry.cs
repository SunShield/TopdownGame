using MyShooter.Unity.Entities.Concrete.Player;
using System.Collections.Generic;

namespace MyShooter.Core.Entities
{
	/// <summary>
	/// This class hold all the game entities: character, enemies, etc.
	/// </summary>
	public class EntityRegistry
	{
		public PlayerEntity Player { get; private set; }
		private List<EnemyEntity> _enemies = new List<EnemyEntity>();
		public IReadOnlyList<EnemyEntity> Enemies => _enemies;

		public void RegisterPlayer(PlayerEntity player)
		{
			Player = player;
		}

		public void RegisterEnemy(EnemyEntity enemy)
		{
			_enemies.Add(enemy);
		}

		public void UnregisterEnemy(EnemyEntity enemy)
		{
			_enemies.Remove(enemy);
		}
	}
}
