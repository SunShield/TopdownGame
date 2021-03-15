using MyShooter.Core.Environment.Events.Entity.Functional;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class DelayedMovementMoveDecisionMaker : MoveDecisionMaker
	{
		[SerializeField] protected float MovementDelay;

		protected float MovementDelayTimer = 0f;
		protected bool DelayStarted => MovementDelayTimer > 0f;
		protected bool DelayEnded => MovementDelayTimer == 0f;

		protected override Vector2 DecideMovementDirectionInternal()
		{
			if(CheckNeedStartDelay())
			{
				if (!DelayStarted)
					StartDelay();

				ProceedDelayTimer();

				if (DelayEnded)
					OnDelayEnded();
			}
			else
			{
				return DecideMovementDirectionWhileNotDelayed();
			}

			return Vector2.zero;
		}

		protected void StartDelay()
		{
			MovementDelayTimer = MovementDelay;
			ThrowStartDelayEvent();
		}

		private void ThrowStartDelayEvent() => EventBus.GetEvent<EntityMoveDecisionStartDelayEvent>().InvokeForId(GoId, null);
		protected void ProceedDelayTimer() => MovementDelayTimer = Mathf.Clamp(MovementDelayTimer - Time.fixedDeltaTime, 0f, MovementDelayTimer);
		protected virtual bool CheckNeedStartDelay() => false;
		protected virtual void OnDelayEnded() { }
		protected virtual Vector2 DecideMovementDirectionWhileNotDelayed() => Vector2.zero;
	}
}
