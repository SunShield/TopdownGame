using MyShooter.Unity.Service.Attributes;
using UnityEngine;

namespace MyShooter.Unity.Entities.Actions
{
	public class EntityActionWithTimer : EntityAction
	{
		[SerializeField] protected float ExecutionTime;
		[SerializeField] [ReadOnly] protected float ExecutionTimer = 0f;

		protected override void StartExecutionInternal()
		{
			StartTimer();
		}

		protected override void UpdateEntityAction()
		{
			if (!IsExecuting) return;

			AdvanceTimer();
		}

		protected override bool CheckEndExecution() => ExecutionTimer == 0f;
		protected void StartTimer() => ExecutionTimer = ExecutionTime;
		protected void AdvanceTimer() => ExecutionTimer = Mathf.Clamp(ExecutionTimer - Time.deltaTime, 0f, ExecutionTimer);
	}
}
