using UnityEngine;

namespace MyShooter.Unity.Entities.Actions.Concrete
{
	public class BasicDash : EntityActionWithTimer
	{
		private const string DashForceName = "DashForce";

		[SerializeField] private float _dashPower = 3000f;

		private bool IsMoving => HolderEntity.MoveDirection != Vector2.zero;

		protected override void StartExecutionInternal()
		{
			if (!IsMoving) return;

			HolderEntity.StatsState.IsHittable = false;
			HolderEntity.AddForce(DashForceName, HolderEntity.MoveDirection * _dashPower, ExecutionTime);
		}

		protected override void EndExecutionInternal()
		{
			HolderEntity.StatsState.IsHittable = true;
		}
	}
}
