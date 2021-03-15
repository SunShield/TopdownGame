using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public class MaxHealthIncreaseEffect : Effect
	{
		private const string ModifierName = "MaxHealthIncreasement";

		[SerializeField] private int _value;

		public override bool Compare(Effect another) => true;

		protected override void OnApply()
		{
			Holder.StatsState.MaxHealth.AddModifier(ModifierName, _value, GetInstanceID());
			Holder.StatsState.CurrentHealth += _value;
		}

		protected override void OnRemove()
		{
			Holder.StatsState.MaxHealth.RemoveModifier(ModifierName, GetInstanceID());
			// TODO: Move this logic into the Breaker, I guess.
			if (Holder.StatsState.CurrentHealth >= Holder.StatsState.MaxHealth.FinalValue)
				Holder.StatsState.CurrentHealth = (int)Holder.StatsState.MaxHealth.FinalValue;
		}
	}
}
