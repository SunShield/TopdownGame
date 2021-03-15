using UnityEngine;

namespace MyShooter.Unity.Service.Extensions
{
	public static class TransformExtensions
	{
		public static void LookAt2D(this Transform tran, Vector2 direction)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			Quaternion newRotation = Quaternion.AngleAxis((angle), Vector3.forward);
			tran.rotation = Quaternion.Slerp(tran.rotation, newRotation, 1.0f);
		}
	}
}
