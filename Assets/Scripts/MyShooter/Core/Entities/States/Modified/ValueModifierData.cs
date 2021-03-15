using MyShooter.Core.Entities.GeneralEnums;
using System;

namespace MyShooter.Core.Entities.States.Modified
{
	[Serializable]
	public class ValueModifierData
	{
		public int Priority;
		public ValueModifierType Type;
		public StackabilityType StackabilityType;
		public int MaxStacksAmount;
		public bool HasMinValue;
		public float MinStackedInnerValue;
		public bool HasMaxValue;
		public float MaxStackedInnerValue;
	}
}
