using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Physics
{
	public class Force
	{
		public string Name;
		public Vector2 ForceVector;
		public float Duration;

		public Force(string name, Vector2 vector, float duration)
		{
			Name = name;
			ForceVector = vector;
			Duration = duration;
		}
	}
}
