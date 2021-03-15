using MyShooter.Core.Entities.States.Modified;
using UnityEditor;
using UnityEngine;

namespace MyShooter.Unity.Editor.CustomropertyDrawers
{
	[CustomPropertyDrawer(typeof(ModifiableValue))]
	public class ModifiableValueDrawer : PropertyDrawer
	{
		private const float LabelLength = 40;
		private const float InnerValueRectLength = 63;
		private const float FinalValueRectHorIndent = 68;

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			rect = EditorGUI.PrefixLabel(rect, new GUIContent(property.displayName));
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var innerValueLabelRect = new Rect(rect.x, rect.y, InnerValueRectLength, rect.height);
			EditorGUI.LabelField(innerValueLabelRect, "Inner:");
			var innerValueRect = new Rect(rect.x + LabelLength, rect.y, InnerValueRectLength, rect.height);
			EditorGUI.PropertyField(innerValueRect, property.FindPropertyRelative("_innerValue"), GUIContent.none);
			var finalValueLaberRect = new Rect(rect.x + LabelLength + FinalValueRectHorIndent, rect.y, InnerValueRectLength + FinalValueRectHorIndent, rect.height);
			EditorGUI.LabelField(finalValueLaberRect, "Final:");
			GUI.enabled = false;
			var finalValueRect = new Rect(rect.x + LabelLength * 2 + FinalValueRectHorIndent, rect.y, InnerValueRectLength, rect.height);
			var finalResult = property.FindPropertyRelative("_wasRecalculatedOnce").boolValue 
				? property.FindPropertyRelative("_lastCalculatedValue") 
				: property.FindPropertyRelative("_innerValue");
			EditorGUI.PropertyField(finalValueRect, finalResult, GUIContent.none);
			GUI.enabled = true;

			EditorGUI.indentLevel = indent;
		}
	}
}
