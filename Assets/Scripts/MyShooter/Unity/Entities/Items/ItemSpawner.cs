using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Items
{
	[CreateAssetMenu(menuName = "My Assets/Item Spawner")]
	public class ItemSpawner : ScriptableObject
	{
		[SerializeField] private List<Item> _fullItemPool;
		[NonSerialized] private List<Item> _actualItemPool;
		[NonSerialized] private bool _isInitialized;

		public Item SpawnRandom(Vector3 position, Transform parent)
		{
			InitializeIfNeeded();

			int newItemIndex = GetRandomIndex();
			var newItemPrototype = _actualItemPool[newItemIndex];
			_actualItemPool.RemoveAt(newItemIndex);
			var newItem = Instantiate(newItemPrototype, position, Quaternion.identity, parent);
			return newItem;
		}

		// Very simple approach, all items have identical weight in pool
		private int GetRandomIndex() => UnityEngine.Random.Range(0, _actualItemPool.Count - 1);

		private void InitializeIfNeeded()
		{
			if (_isInitialized) return;

			_actualItemPool = new List<Item>(_fullItemPool);
			_isInitialized = true;
		}
	}
}
