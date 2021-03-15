using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Components.Decisioning.Skills;
using MyShooter.Unity.Entities.Components.Skills;
using MyShooter.Unity.Entities.Items;
using MyShooter.Unity.Entities.Skills;
using MyShooter.Unity.Entities.Structure;
using System.Collections.Generic;

namespace MyShooter.Unity.Entities.Concrete.Player
{
	public class PlayerEntity : BattleEntity
	{
		public override EntityType Type => EntityType.Player;

		protected SkillPlayer SkillPlayer { get; private set; }
		protected SkillPlayDecisionMaker skillPlayDecisionMaker { get; private set; }
		protected SkillStorage SkillStorage { get; private set; }
		protected ItemStorage ItemStorage { get; private set; }
		public PlayerStructure PlayerStructure { get; private set; }
		public IReadOnlyDictionary<SkillType, Skill> Skills => SkillStorage.SkillsByType;
		public SkillsState SkillsState => SkillPlayer?.SkillsState;

		#region Initialization

		protected override void InitializeAutomatically()
		{
			base.InitializeAutomatically();
			InitStructure();
			RegisterSelf();
		}

		private void InitStructure()
		{
			PlayerStructure = new PlayerStructure(this);
		}

		private void RegisterSelf()
		{
			Registry.RegisterPlayer(this);
		}

		protected override void FindComponentsInternal()
		{
			skillPlayDecisionMaker = GetComponentInChildren<SkillPlayDecisionMaker>();
			SkillPlayer = GetComponent<SkillPlayer>();
		}

		protected override void AwakeComponentsInternal()
		{
			SkillPlayer?.AwakeComponentManually(this);
			skillPlayDecisionMaker?.AwakeComponentManually(this);
		}

		protected override void InitializeFieldsInternal()
		{
			SkillStorage = new SkillStorage();
			FindSkills();
			ItemStorage = new ItemStorage();
		}

		private void FindSkills()
		{
			SkillsFinder finder = new SkillsFinder(PlayerStructure, SkillStorage);
			finder.FindSkills();
		}

		#endregion

		#region Control Methods

		public void AddSkill(Skill skill)
		{
			var skillTran = skill.Tran;
			var skillType = skill.Type;
			if(Skills.ContainsKey(skill.Type)) // we can have only one skill of each type
				RemoveSkill(Skills[skillType]);
			ApplyInHierarchy(skillTran, PlayerStructure.OriginsBySkillType[skill.Type]);
			SkillStorage.AddSkill(skill);
		}

		public void RemoveSkill(Skill skill)
		{
			SkillStorage.RemoveSkill(skill);
			Destroy(skill);
		}

		public void AddItem(Item item)
		{
			var itemTran = item.Tran;
			ApplyInHierarchy(itemTran, Structure.Items);
			ItemStorage.AddItem(item);

			foreach(var effect in item.Effects)
				AddEffect(effect);

			foreach (var skill in item.Skills)
				AddSkill(skill);
		}

		public void RemoveItem(Item item)
		{
			ItemStorage.RemoveItem(item);

			foreach (var effect in item.Effects)
				RemoveEffect(effect);

			foreach (var skill in item.Skills)
				RemoveSkill(skill);

			Destroy(item);
		}

		#endregion
	}
}
