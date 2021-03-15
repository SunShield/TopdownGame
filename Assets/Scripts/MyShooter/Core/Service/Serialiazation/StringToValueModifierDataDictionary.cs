using MyShooter.Core.Entities.States.Modified;
using System;

namespace MyShooter.Core.Service.Serialiazation
{
	[Serializable]
	public class StringToValueModifierDataDictionary : SerializableDictionary<string, ValueModifierData> { }
}
