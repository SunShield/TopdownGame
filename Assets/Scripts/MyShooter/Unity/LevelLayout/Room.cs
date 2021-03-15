using UnityEngine;

namespace MyShooter.Unity.LevelLayout
{
	public class Room : ManuallyUpdatableBehaviour
	{
		[SerializeField] private RoomType _type;
		[SerializeField] private Transform _enterPoint;

		public Vector3 EnterPoint => _enterPoint.position;
		public RoomType Type => _type;
	}
}