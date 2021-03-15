using MyShooter.Core.Entities.States.Modified;
using System;

namespace MyShooter.Core.Entities.States
{
	[Serializable]
	public class SkillsState
	{
		public ModifiableValue MaximumMana;
		public float CurrentMana;
		public float ManaRegeneration;
	}
}
