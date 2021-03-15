using MyShooter.Core.Service.Serialiazation;
using UnityEngine;

namespace MyShooter.Unity.Data
{
	[CreateAssetMenu(menuName = "My Assets/Value Modifiers Data Object")]
	public class ValueModifiersDataObject : GlobalScriptableObject<ValueModifiersDataObject>
	{
		public StringToValueModifierDataDictionary ValueModifiers;
	}
}
