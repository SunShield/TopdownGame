using MyShooter.Core.Entities.States;
using MyShooter.Core.Environment.Events.Entity.Functional;
using MyShooter.Unity.Entities.Concrete.Player;
using MyShooter.Unity.Entities.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Skills
{
	public class SkillPlayer : EntityComponent
	{
		[SerializeField] private SkillsState _skillsState;

		private IReadOnlyDictionary<SkillType, Skill> _playerSkills;
		public SkillsState SkillsState => _skillsState;

		#region Initializing/Deinitializing

		protected override void AwakeInternal()
		{
			_playerSkills = (HolderEntity as PlayerEntity).Skills;
			EventBus.GetEvent<EntitySkillPlayDecidedEvent>().SubscribeForId(GoId, TryPlaySkill);
		}

		protected override void FixedUpdateComponentInternal()
		{
			_skillsState.CurrentMana = Mathf.Clamp(_skillsState.CurrentMana + _skillsState.ManaRegeneration * Time.fixedDeltaTime, 0f, _skillsState.MaximumMana.FinalValue);
		}

		protected override void OnDestroyAutomatically()
		{
			EventBus.GetEvent<EntitySkillPlayDecidedEvent>().UnsubscribeFromId(GoId, TryPlaySkill);
		}

		#endregion

		public void TryPlaySkill(EntitySkillPlayDecidedEventArgs args)
		{
			var type = (SkillType)args.Type;
			if (!_playerSkills.ContainsKey(type)) return;
			var skill = _playerSkills[type];
			if (!skill.IsExecutable) return;
			if (!CheckSkillPayable(skill)) return;

			PlaySkill(skill);
		}

		private bool CheckSkillPayable(Skill skill) => skill.ManaCost <= SkillsState.CurrentMana;

		private void PlaySkill(Skill skill)
		{
			PaySkill(skill);
			skill.TryStartExecution();
		}

		private void PaySkill(Skill skill)
		{
			SkillsState.CurrentMana -= skill.ManaCost;
		}
	}
}
