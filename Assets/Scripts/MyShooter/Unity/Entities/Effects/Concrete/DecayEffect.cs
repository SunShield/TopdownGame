using MyShooter.Unity.Entities.Components.Breaking;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public class DecayEffect : TickingEffect
	{
		[SerializeField] private DamageInstance _damagePerTick;
		[SerializeField] private GameObject _graphics;
		public DamageInstance DamagePerTick => _damagePerTick;

		public override bool Compare(Effect another)
		{
			var anotherTyped = (DecayEffect)another;
			if (anotherTyped == null) return false;
			return DamagePerTick.SummaryDamage > anotherTyped.DamagePerTick.SummaryDamage;
		}

		protected override void OnApply()
		{
			_graphics.SetActive(true);
		}

		protected override void DoOnTick()
		{
			Holder.TryReceiveDamage(_damagePerTick);
		}
	}
}
