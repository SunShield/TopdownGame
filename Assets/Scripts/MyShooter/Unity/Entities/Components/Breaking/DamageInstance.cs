using MyShooter.Core.Entities.States;
using MyShooter.Core.Service.Serialiazation;
using System;
using System.Linq;

namespace MyShooter.Unity.Entities.Components.Breaking
{
	// single instance of damage can contain more than one element
	[Serializable]
	public class DamageInstance
	{
		public DamageTypeIntDictionary Values = new DamageTypeIntDictionary()
		{
			{ DamageType.Physical, 0 },
			{ DamageType.Fire, 0 },
			{ DamageType.Cold, 0 },
			{ DamageType.Acid, 0 },
			{ DamageType.Lightnng, 0 },
			{ DamageType.Magical, 0 },
		};

		public int this[DamageType type]
		{
			get => Values[type];
			set => Values[type] = value;
		}

		public int SummaryDamage => Values.Values.Sum();

		public void Copy(DamageInstance instance)
		{
			foreach(var damageType in (DamageType[])Enum.GetValues(typeof(DamageType)))
			{
				Values[damageType] = instance.Values[damageType];
			}
		}
	}
}
