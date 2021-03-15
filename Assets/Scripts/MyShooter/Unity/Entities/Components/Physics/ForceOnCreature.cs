using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Physics
{
	public class ForceOnCreature
	{
		public Force Force;
		public bool WasExecuted;

		public string Name
		{
			get { return Force.Name; }
			set { Force.Name = value; }
		}

		public Vector2 ForceVector
		{
			get { return Force.ForceVector; }
			set { Force.ForceVector = value; }
		}

		public float Duration
		{
			get { return Force.Duration; }
			set { Force.Duration = value; }
		}

		public bool IsExpired => Duration <= 0f;

		public ForceOnCreature(Force force)
		{
			Force = force;
		}
	}
}
