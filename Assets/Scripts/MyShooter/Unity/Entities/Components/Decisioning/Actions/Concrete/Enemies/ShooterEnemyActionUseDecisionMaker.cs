using MyShooter.Core.Environment.Events;
using MyShooter.Core.Environment.Events.Entity.Functional;
using MyShooter.Unity.Entities.Actions;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Actions.Concrete.Enemies
{
	public class ShooterEnemyActionUseDecisionMaker : ActionUseDecisionMaker
	{
		[SerializeField] private EntityAction _action;

		protected override void AwakeInternal()
		{
			EventBus.GetEvent<EntityMoveDecisionStartDelayEvent>().SubscribeForId(GoId, OnEntityMovementDelay);
		}

		protected override void OnDestroyAutomatically()
		{
			EventBus.GetEvent<EntityMoveDecisionStartDelayEvent>().UnsubscribeFromId(GoId, OnEntityMovementDelay);
		}

		private void OnEntityMovementDelay(GameEventArgs args) => _action.TryStartExecution();
	}
}
