using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public abstract class DurationalEffect : Effect
	{
		[SerializeField] protected float Duration;
		protected float DurationTimer;
		protected bool DurationStarted { get; private set; }
		protected bool DurationEnded => DurationStarted && DurationTimer == 0f;

		protected override void InternalApplyActions()
		{
			DurationStarted = true;
			DurationTimer = Duration;
		}

		public sealed override void UpdateManually()
		{
			if(DurationStarted)
				ProceedTimer();

			UpdateDurationalEffect();

			if (DurationEnded)
			{
				EndTimer();
				Remove();
			}
		}

		private void ProceedTimer()
		{
			DurationTimer = Mathf.Clamp(DurationTimer - Time.deltaTime, 0f, DurationTimer);
		}

		private void EndTimer()
		{
			DurationStarted = false;
		}

		protected virtual void UpdateDurationalEffect() { }
	}
}
