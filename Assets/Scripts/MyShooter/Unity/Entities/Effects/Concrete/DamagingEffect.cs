using MyShooter.Unity.Entities.Components.Breaking;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public abstract class DamagingEffect : Effect
	{
		[SerializeField] protected DamageInstance Damage;

		public void SetDamage(DamageInstance damage)
		{

		}
	}
}
