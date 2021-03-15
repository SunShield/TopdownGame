using MyShooter.Core.Constants;
using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Components.Breaking;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public abstract class TickingEffect : DurationalEffect
	{
		private float _tickTimer;
		private bool IsWaitingForTick => _tickTimer > 0f;

		protected override void InternalApplyActions()
		{
			base.InternalApplyActions();
			SetTickTimer();
		}

		protected sealed override void UpdateDurationalEffect()
		{
			if (!DurationStarted) return;

			if (!IsWaitingForTick)
			{
				DoOnTick();
				SetTickTimer();
			}
			else
				ProceedTickTimer();
		}

		protected virtual void DoOnTick() { }
		private void SetTickTimer() => _tickTimer = GlobalGameConsts.TickTime;
		private void ProceedTickTimer() => _tickTimer = Mathf.Clamp(_tickTimer - Time.deltaTime, 0f, _tickTimer);

	}
}
