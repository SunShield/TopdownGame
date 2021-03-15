using MyShooter.Core.Service.Serialiazation;
using UnityEditor;

namespace MyShooter.Unity.Editor.CustomPropertyDrawers
{
	[CustomPropertyDrawer(typeof(DamageTypeIntDictionary))]
	public class DamageTypeIntDictionaryPriopertyDrawer : SerializableDictionaryPropertyDrawer { }
}
