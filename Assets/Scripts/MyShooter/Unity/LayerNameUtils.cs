using UnityEngine;

namespace MyShooter.Unity
{
	public static class LayerNameUtils
	{
		public const string Characters = "Characters";
		public const string Enemies = "Enemies";
		public const string FlyingEnemies = "FlyingEnemies";
		public const string Obstacles = "Obstacles";
		public const string Bullets = "Bullets";
		public const string Walls = "Walls";
		public const string EdgeWalls = "EdgeWalls";

		public static readonly LayerMask EntityMask = 1 << LayerMask.NameToLayer(Characters) |
													  1 << LayerMask.NameToLayer(Enemies) |
													  1 << LayerMask.NameToLayer(FlyingEnemies) |
													  1 << LayerMask.NameToLayer(Obstacles) |
													  1 << LayerMask.NameToLayer(Bullets) |
													  1 << LayerMask.NameToLayer(Walls) |
													  1 << LayerMask.NameToLayer(EdgeWalls);

		public static bool IsEntity(GameObject collidedObject) => (EntityMask.value & 1 << collidedObject.layer) > 0;
	}
}
