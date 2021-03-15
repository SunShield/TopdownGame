using MyShooter.Unity.Entities.Components.Breaking;
using MyShooter.Unity.Entities.Concrete.Bullets;
using UnityEngine;

namespace MyShooter.Unity.Entities.Actions.Concrete
{
	public class BasicShot : EntityActionWithTimer
	{
		[SerializeField] private Transform _origin;
		[SerializeField] private BulletEntity _bullet;
		[SerializeField] private DamageInstance _damage;

		protected override void StartExecutionInternal()
		{
			var bullet = Instantiate(_bullet, _origin.position, Quaternion.identity);
			bullet.SetDamage(_damage);
		}
	}
}
