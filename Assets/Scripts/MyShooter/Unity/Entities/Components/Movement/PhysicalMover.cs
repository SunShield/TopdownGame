using MyShooter.Unity.Entities.Components.Physics;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Movement
{
	public class PhysicalMover : Mover
	{
		protected const string MovementForceName = "Movement";

		protected ForceProcessor Physics { get; private set; }

		protected override void AwakeInternal()
		{
			base.AwakeInternal();
			InitializePhysics();
		}

		private void InitializePhysics()
		{
			Physics = GetComponentInParent<ForceProcessor>();
			if (Physics == null)
			{
				Debug.Log($"Physical mover on entity {HolderEntity.name} can't find physics component!");
				// do something - probably throw an error
				return;
			}
		}
	}
}
