using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyShooter.Unity.UI.Player.Skills
{
	public class SkillCooldownUi : PlayerUiElement
	{
		[SerializeField] private GameObject _graphics;
		[SerializeField] private Image _image;
		[SerializeField] private Image _fill;
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private SkillType _type;

		protected Skill CurrentSkill
		{
			get
			{
				if (Player != null && Player.Skills.ContainsKey(_type)) return Player.Skills[_type];
				return null;
			}
		}
		protected bool HasSkill => CurrentSkill != null;

		protected override void FixedUpdateElement()
		{
			SetGraphics();
			if (!HasSkill) return;

			SetImage();
			SetFill();
			SetText();
		}

		private void SetImage()
		{
			if (_image.sprite != CurrentSkill.Icon)
				_image.sprite = CurrentSkill.Icon;
		}

		private void SetFill()
		{
			_fill.fillAmount = CurrentSkill.CooldownTimer / CurrentSkill.State.CooldownTime;
		}

		private void SetText()
		{
			_text.text = CurrentSkill.CooldownTimer != 0f ? $"{System.Math.Round(CurrentSkill.CooldownTimer, 2)}" : "";
		}

		private void SetGraphics()
		{
			if (HasSkill && !_graphics.activeSelf)
				_graphics.SetActive(true);
			else if (!HasSkill && _graphics.activeSelf)
				_graphics.SetActive(false);
		}
	}
}
