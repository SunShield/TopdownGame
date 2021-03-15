using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Rotation
{
	public class RotationDecisionMaker : DecisionMaker
	{
		protected Transform RotationOrigin { get; private set; }

		protected override void AwakeInternal()
		{
			RotationOrigin = HolderEntity.Structure.Base.transform;
		}
	}
}
