using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class PlayerPursueDecisionMaker : MoveDecisionMaker
	{
		protected Vector2 PlayerPosition 
			=> Registry.Player != null ? (Vector2)Registry.Player.Tran.position : Vector2.zero;

		protected override Vector2 DecideMovementDirectionInternal()
		{
			return PlayerPosition - (Vector2)Tran.position;
		}
	}
}
