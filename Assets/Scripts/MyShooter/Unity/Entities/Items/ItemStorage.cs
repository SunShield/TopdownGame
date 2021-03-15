using System.Collections.Generic;

namespace MyShooter.Unity.Entities.Items
{
	public class ItemStorage
	{
		private Dictionary<string, Item> _items = new Dictionary<string, Item>();

		public void AddItem(Item item) => _items.Add(item.Name, item);
		public void RemoveItem(Item item) => _items.Remove(item.Name);
		public Item this[string name] => _items[name];
	}
}
