using MyShooter.Core.Environment.Events.Entity.Functional;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class MoveDecisionMaker : DecisionMaker
	{
		// we use those to reuse one class, and generate less garbage.
		protected EntityMoveDecisionChangedEventArgs MovementArgs { get; private set; }
			= new EntityMoveDecisionChangedEventArgs() { Direction = Vector2.zero };

		protected sealed override void FixedUpdateComponentInternal()
		{
			DecideMovementDirection();
		}

		private void DecideMovementDirection()
		{
			var moveDirection = DecideMovementDirectionInternal();
			if (moveDirection == Vector2.zero) return;

			MovementArgs.Direction = moveDirection;
			EventBus.GetEvent<EntityMoveDecisionChangedEvent>().InvokeForId(GoId, MovementArgs);
		}

		protected virtual Vector2 DecideMovementDirectionInternal() { return Vector2.zero; }

		// actually hacky solution to pass some data into MoveDecisionMaker.
		// Is unused most of a time.
		public virtual void Configure(Vector2 configureData) { }
	}
}
