using MyShooter.Core.Entities.States;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Skills
{
	public class SkillStorage
	{
		private Dictionary<SkillType, Skill> _skillsByType = new Dictionary<SkillType, Skill>();
		public IReadOnlyDictionary<SkillType, Skill> SkillsByType => _skillsByType;

		public void AddSkill(Skill skill)
		{
			var skillType = skill.Type;
			if (_skillsByType.ContainsKey(skillType))
				RemoveSkill(skill);
			_skillsByType.Add(skillType, skill);
		}

		public void RemoveSkill(Skill skill)
		{
			var skillType = skill.Type;
			if (!_skillsByType.ContainsKey(skillType))
			{
				Debug.Log($"Trying to remove non-existing skill: {skillType}");
				return;
			}

			_skillsByType.Remove(skillType);
		}

		public bool Contains(SkillType type) => _skillsByType.ContainsKey(type);
		public Skill this[SkillType type] => _skillsByType[type];
	}
}
