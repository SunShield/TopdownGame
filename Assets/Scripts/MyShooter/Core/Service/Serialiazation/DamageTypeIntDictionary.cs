using MyShooter.Core.Entities.States;
using System;

namespace MyShooter.Core.Service.Serialiazation
{
	[Serializable]
	public class DamageTypeIntDictionary : SerializableDictionary<DamageType, int> { }
}
