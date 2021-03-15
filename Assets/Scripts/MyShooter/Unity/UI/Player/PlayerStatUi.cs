using MyShooter.Core.Entities;
using MyShooter.Unity.Entities.Concrete.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyShooter.Unity.UI.Player
{
    public class PlayerStatUi : PlayerUiElement
    {
		[SerializeField] private Slider _slider;
		[SerializeField] private TextMeshProUGUI _text;

		protected virtual float CurrentValue => 0;
		protected virtual float MaxValue => 0;

		protected override void FixedUpdateElement()
		{
			SetSlider();
			SetText();
		}

		protected override void FixedUpdateElementNoPlayer()
		{
			SetDefaultSlider();
			SetDefaultText();
		}

		private void SetSlider() => _slider.value = CurrentValue / MaxValue;
		private void SetText() => _text.text = $"{(int)CurrentValue}/{(int)MaxValue}";
		private void SetDefaultSlider() => _slider.value = 0f;
		private void SetDefaultText() => _text.text = $"0/{(int)MaxValue}";
	}
}
