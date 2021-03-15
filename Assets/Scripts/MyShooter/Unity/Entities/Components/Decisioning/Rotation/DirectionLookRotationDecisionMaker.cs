using MyShooter.Core.Environment.Events.Entity.Functional;
using MyShooter.Unity.Service.Extensions;

namespace MyShooter.Unity.Entities.Components.Decisioning.Rotation
{
	public class DirectionLookRotationDecisionMaker : RotationDecisionMaker
	{
		protected sealed override void InitializeComponent()
		{
			EventBus.GetEvent<EntityMoveDecisionChangedEvent>().SubscribeForId(GoId, OnEntityMoveDecision);
		}

		protected virtual void OnEntityMoveDecision(EntityMoveDecisionChangedEventArgs args)
		{
			RotationOrigin.LookAt2D(args.Direction);
		}

		protected override void OnDestroyAutomatically()
		{
			EventBus.GetEvent<EntityMoveDecisionChangedEvent>().UnsubscribeFromId(GoId, OnEntityMoveDecision);
		}
	}
}
