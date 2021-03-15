using UnityEngine;

namespace MyShooter.Unity.Entities.Items
{
	public class ItemSpawnPoint : ManuallyUpdatableBehaviour
	{
		[SerializeField] private ItemSpawner Spawner;

		protected override void InitializeAutomatically()
		{
			Spawner.SpawnRandom(Tran.position, Tran);
		}
	}
}
