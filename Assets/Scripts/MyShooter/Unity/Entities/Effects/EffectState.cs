using MyShooter.Core.Entities.GeneralEnums;
using System;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects
{
	[Serializable]
	public class EffectState
	{
		public StackabilityType StackabilityType;
		public int MaxStackAmount;
	}
}
