using MyShooter.Core.Service.Serialiazation;
using System;
using UnityEngine;

namespace MyShooter.Core.Entities.States
{
	// Thank you Unity for no built-in Dictionary serialization in 2021.
	[Serializable]
	public class DefencesState
	{
		public DamageTypeIntDictionary Resistances = new DamageTypeIntDictionary()
		{
			{ DamageType.Physical, 0 },
			{ DamageType.Fire, 0 },
			{ DamageType.Cold, 0 },
			{ DamageType.Acid, 0 },
			{ DamageType.Lightnng, 0 },
			{ DamageType.Magical, 0 },
		};

		public DamageTypeIntDictionary Armors = new DamageTypeIntDictionary()
		{
			{ DamageType.Physical, 0 },
			{ DamageType.Fire, 0 },
			{ DamageType.Cold, 0 },
			{ DamageType.Acid, 0 },
			{ DamageType.Lightnng, 0 },
			{ DamageType.Magical, 0 },
		};
	}
}
