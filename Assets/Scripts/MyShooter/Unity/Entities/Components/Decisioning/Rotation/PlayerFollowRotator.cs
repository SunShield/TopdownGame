using MyShooter.Unity.Service.Extensions;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Rotation
{
	public class PlayerFollowRotator : RotationDecisionMaker
	{
		protected Vector2 PlayerPosition
			=> Registry.Player != null ? (Vector2)Registry.Player.Tran.position : Vector2.zero;

		protected override void UpdateComponentInternal()
		{
			RotationOrigin.LookAt2D(PlayerPosition - (Vector2)RotationOrigin.position);
		}
	}
}
