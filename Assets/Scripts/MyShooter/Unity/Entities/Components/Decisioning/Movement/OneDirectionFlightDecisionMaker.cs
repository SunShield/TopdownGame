using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class OneDirectionFlightDecisionMaker : MoveDecisionMaker
	{
		private Vector2 _direction;

		protected override void AwakeInternal()
		{
			_direction = DetermineDirection();
		}

		protected override Vector2 DecideMovementDirectionInternal()
		{
			return _direction;
		}

		public override void Configure(Vector2 configureData)  =>_direction = configureData;
		protected virtual Vector2 DetermineDirection() => Vector2.zero;
	}
}
