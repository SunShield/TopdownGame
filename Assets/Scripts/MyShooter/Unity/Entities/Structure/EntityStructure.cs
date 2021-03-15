using UnityEngine;

namespace MyShooter.Unity.Entities.Structure
{
	public class EntityStructure
	{
		public const string BaseGo = "Base";
		public const string DynamicComponentsGo = "DynamicComponents";
		public const string GraphicsGo = "Graphics";
		public const string CollisionGo = "Collision";
		public const string EffectsGo = "Effects";
		public const string ItemsGo = "Items";
		public const string ActionsGo = "Actions";

		public GameObject Base { get; private set; }
		public GameObject DynamicComponents { get; private set; }
		public GameObject Graphics { get; private set; }
		public GameObject Collision { get; private set; }
		public GameObject Effects { get; private set; }
		public GameObject Items { get; private set; }
		public GameObject Actions { get; private set; }

		public EntityStructure(BattleEntity entity)
		{
			var tran = entity.transform;
			Base = tran.Find(BaseGo).gameObject;
			var baseTran = Base.transform;
			DynamicComponents = baseTran.Find(DynamicComponentsGo)?.gameObject;
			Graphics = baseTran.Find(GraphicsGo)?.gameObject;
			Collision = baseTran.Find(CollisionGo)?.gameObject;
			Effects = baseTran.Find(EffectsGo)?.gameObject;
			Items = baseTran.Find(ItemsGo)?.gameObject;
			Actions = baseTran.Find(ActionsGo)?.gameObject;
		}
	}
}
