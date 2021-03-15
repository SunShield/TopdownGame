using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement.Concrete.Enemies
{
	public class ShooterEnemyMoveDecisionMaker : DelayedMovementMoveDecisionMaker
	{
		private const float FieldBoundOffset = 0.6f;
		private const float MinimumMovementLength = 1;
		private const float PositionTolerance = 0.05f;

		[SerializeField] private Transform _topLeftZonePoint;
		[SerializeField] private Transform _bottomRightZonePoint;

		private Vector2 _nextTargetPosition;
		private Vector2 _direction;

		private bool CloseToDestination => ((Vector2)Tran.position - _nextTargetPosition).sqrMagnitude <= PositionTolerance;
		private bool IsFloughtOverDestination => (_nextTargetPosition - (Vector2)Tran.position).normalized == -_direction;

		protected override void AwakeInternal()
		{
			_nextTargetPosition = Tran.position;
		}

		#region Movement

		protected override void OnDelayEnded()
		{
			GenerateNewDestination();
		}

		private void GenerateNewDestination()
		{
			var xCoord = Random.Range(_topLeftZonePoint.position.x + FieldBoundOffset, _bottomRightZonePoint.position.x - FieldBoundOffset);
			var yCoord = Random.Range(_topLeftZonePoint.position.y - FieldBoundOffset, _bottomRightZonePoint.position.y + FieldBoundOffset);
			var newDestination = new Vector2(xCoord, yCoord);
			if (newDestination.sqrMagnitude < MinimumMovementLength)
			{
				GenerateNewDestination();
			}
			else
			{
				_nextTargetPosition = newDestination;
				_direction = (_nextTargetPosition - (Vector2)Tran.position).normalized;
			}
		}

		protected override bool CheckNeedStartDelay() => CloseToDestination || IsFloughtOverDestination;
		protected override Vector2 DecideMovementDirectionWhileNotDelayed() => _direction;

		#endregion

		#region Gizmos

		private void OnDrawGizmosSelected()
		{
			if (_topLeftZonePoint == null || _bottomRightZonePoint == null) return;

			var tlPos = _topLeftZonePoint.position;
			var brPos = _bottomRightZonePoint.position;
			Gizmos.color = Color.red;
			Gizmos.DrawLine(new Vector3(tlPos.x + FieldBoundOffset, tlPos.y - FieldBoundOffset), new Vector3(brPos.x - FieldBoundOffset, tlPos.y - FieldBoundOffset));
			Gizmos.DrawLine(new Vector3(tlPos.x + FieldBoundOffset, tlPos.y - FieldBoundOffset), new Vector3(tlPos.x + FieldBoundOffset, brPos.y + FieldBoundOffset));
			Gizmos.DrawLine(new Vector3(tlPos.x + FieldBoundOffset, brPos.y + FieldBoundOffset), new Vector3(brPos.x - FieldBoundOffset, brPos.y + FieldBoundOffset));
			Gizmos.DrawLine(new Vector3(brPos.x - FieldBoundOffset, tlPos.y - FieldBoundOffset), new Vector3(brPos.x - FieldBoundOffset, brPos.y + FieldBoundOffset));
		}

		#endregion
	}
}