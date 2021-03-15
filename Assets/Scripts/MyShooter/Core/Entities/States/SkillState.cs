using System;

namespace MyShooter.Core.Entities.States
{
	[Serializable]
	public class SkillState
	{
		public string Name;
		public SkillType Type;
		public int ManaCost;

		public float PreparationTime;
		public float ExecutionTime;
		public float CooldownTime;
	}
}
