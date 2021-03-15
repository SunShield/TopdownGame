using MyShooter.Core.Entities.States;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Structure
{
	public class PlayerStructure
	{
		public const string SkillsGo = "Skills";
		public const string DashGo = "Dash";
		public const string MainGo = "Main";
		public const string SecondaryGo = "Secondary";
		public const string SigilGo = "Sigil";
		public const string UtilityGo = "Utility";
		public const string SignatureGo = "Signature";

		// generally, it is a bad idea to give a "structure" knowledge about a "skill type" existance...
		// but I think, it's OK for now
		private Dictionary<SkillType, GameObject> _originsBySkillType;

		public GameObject Skills { get; private set; }
		public GameObject Dash { get; private set; }
		public GameObject Main { get; private set; }
		public GameObject Secondary { get; private set; }
		public GameObject Sigil { get; private set; }
		public GameObject Utility { get; private set; }
		public GameObject Signature { get; private set; }
		public IReadOnlyDictionary<SkillType, GameObject> OriginsBySkillType => _originsBySkillType;

		public PlayerStructure(BattleEntity entity)
		{
			var baseTran = entity.Structure.Base.transform;
			Skills = baseTran.Find(SkillsGo)?.gameObject;
			var skillsTran = Skills.transform;
			Dash = skillsTran.Find(DashGo)?.gameObject;
			Main = skillsTran.Find(MainGo)?.gameObject;
			Secondary = skillsTran.Find(SecondaryGo)?.gameObject;
			Sigil = skillsTran.Find(SigilGo)?.gameObject;
			Utility = skillsTran.Find(UtilityGo)?.gameObject;
			Signature = skillsTran.Find(SignatureGo)?.gameObject;

			_originsBySkillType = new Dictionary<SkillType, GameObject>()
			{
				{ SkillType.Dash, Dash },
				{ SkillType.Main, Main },
				{ SkillType.Secondary, Secondary },
				{ SkillType.Sigil, Sigil },
				{ SkillType.Utility, Utility },
				{ SkillType.Signature, Signature },
			};
		}
	}
}
