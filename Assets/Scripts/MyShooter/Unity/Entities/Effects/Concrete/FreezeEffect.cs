using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public class FreezeEffect : DurationalEffect
	{
		private const string ModifierName = "Freeze";

		[SerializeField] private float _freezePowerPerecent;
		[SerializeField] private GameObject _graphics; // mb this is a bit unappropriate solution... but searching por a proper one is a bit of overkill, I think

		public float FreezePower => _freezePowerPerecent;

		public override bool Compare(Effect another)
		{
			var anotherTyped = (FreezeEffect)another;
			return anotherTyped != null
				? FreezePower > anotherTyped.FreezePower
				: false;
		}

		protected override void OnApply()
		{
			_graphics.SetActive(true);
			Holder.MovementState.Speed.AddModifier(ModifierName, -(_freezePowerPerecent / 100f), GetInstanceID());
		}

		protected override void OnRemove()
		{
			_graphics.SetActive(false);
			Holder.MovementState.Speed.RemoveModifier(ModifierName, GetInstanceID());
		}
	}
}
