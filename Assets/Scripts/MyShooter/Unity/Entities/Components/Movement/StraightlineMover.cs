using MyShooter.Core.Environment.Events.Entity.Functional;

namespace MyShooter.Unity.Entities.Components.Movement
{
	public class StraightlineMover : PhysicalMover
	{
		protected override void MoveInternal(EntityMoveDecisionChangedEventArgs args)
		{
			var moveVector = Direction * MovementState.Speed.FinalValue;
			Physics.AddForce(MovementForceName, moveVector);
		}
	}
}
