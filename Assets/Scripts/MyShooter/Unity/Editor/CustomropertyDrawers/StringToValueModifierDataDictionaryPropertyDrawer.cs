using MyShooter.Core.Service.Serialiazation;
using UnityEditor;

namespace MyShooter.Unity.Editor.CustomropertyDrawers
{
	[CustomPropertyDrawer(typeof(StringToValueModifierDataDictionary))]
	public class StringToValueModifierDataDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}
