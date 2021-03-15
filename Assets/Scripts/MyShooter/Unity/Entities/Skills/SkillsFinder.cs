using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Structure;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Skills
{
	public class SkillsFinder
	{
		private readonly List<SkillType> _skillTypes = new List<SkillType>()
		{
			SkillType.Dash, SkillType.Main, SkillType.Secondary, SkillType.Sigil, SkillType.Utility, SkillType.Signature
		};

		private PlayerStructure _structure;
		private SkillStorage _storage;

		public SkillsFinder(PlayerStructure structure, SkillStorage storage)
		{
			_structure = structure;
			_storage = storage;
		}

		public void FindSkills()
		{
			foreach (var skillType in _skillTypes)
				FindSkillOnGoByType(_structure.OriginsBySkillType[skillType], skillType);
		}

		private void FindSkillOnGoByType(GameObject go, SkillType type)
		{
			var skill = go.GetComponentInChildren<Skill>();
			if (skill == null) return;
			var skillType = skill.Type;
			if (skillType != type)
				throw new Exception($"Something went wrong! You have a skill of type ({skillType}) on ({go.name}) Go!");
			_storage.AddSkill(skill);
		}
	}
}
