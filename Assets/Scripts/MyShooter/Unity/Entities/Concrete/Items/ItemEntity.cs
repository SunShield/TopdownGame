using MyShooter.Core.Entities.States;

namespace MyShooter.Unity.Entities.Concrete.Items
{
	public class ItemEntity : BattleEntity
	{
		public override EntityType Type => EntityType.Item;
	}
}
