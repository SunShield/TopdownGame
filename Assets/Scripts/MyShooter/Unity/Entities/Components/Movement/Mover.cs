using MyShooter.Core.Entities.States;
using MyShooter.Core.Environment.Events.Entity.Functional;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Movement
{
	public class Mover : EntityComponent
	{
		[SerializeField] private MovementState _movementState;

		public Vector2 Direction { get; protected set; }
		public MovementState MovementState => _movementState;

		#region Initializing/Deinitializing

		protected override void AwakeInternal()
		{
			EventBus.GetEvent<EntityMoveDecisionChangedEvent>().SubscribeForId(GoId, Move);
		}

		protected override void OnDestroyAutomatically()
		{
			EventBus.GetEvent<EntityMoveDecisionChangedEvent>().UnsubscribeFromId(GoId, Move);
		}

		#endregion

		private void Move(EntityMoveDecisionChangedEventArgs args)
		{
			Direction = args.Direction.normalized;
			MoveInternal(args);
		}

		protected virtual void MoveInternal(EntityMoveDecisionChangedEventArgs args) { }
	}
}
