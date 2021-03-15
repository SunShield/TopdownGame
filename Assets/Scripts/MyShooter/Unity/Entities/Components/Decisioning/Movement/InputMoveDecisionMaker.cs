using MyShooter.Core.GameInput;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class InputMoveDecisionMaker : MoveDecisionMaker
	{
		protected override Vector2 DecideMovementDirectionInternal()
		{
			var moveDirection = Vector2.zero;
			if (BasicInput.UpPressed) moveDirection += Vector2.up;
			if (BasicInput.RightPressed) moveDirection += Vector2.right;
			if (BasicInput.DownPressed) moveDirection += Vector2.down;
			if (BasicInput.LeftPressed) moveDirection += Vector2.left;

			return moveDirection;
		}
	}
}
