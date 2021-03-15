using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class PlayerPositionFlightDecisionMaker : OneDirectionFlightDecisionMaker
	{
		protected Vector2 PlayerPosition
			=> Registry.Player != null ? (Vector2)Registry.Player.Tran.position : Vector2.zero;

		protected override Vector2 DetermineDirection() => PlayerPosition - (Vector2)Tran.root.position;
	}
}
