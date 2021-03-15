using MyShooter.Core.Entities.States.Modified;
using System;

namespace MyShooter.Core.Entities.States
{
	[Serializable]
	public class StatsState
	{
		public ModifiableValue MaxHealth;
		public int CurrentHealth;
		public bool IsHittable = true;
		public bool IsDamagable = true;

		public DefencesState Defences;
	}
}
