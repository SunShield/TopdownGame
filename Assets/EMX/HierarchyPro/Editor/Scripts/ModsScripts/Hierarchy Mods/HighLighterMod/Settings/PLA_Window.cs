using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace EMX.HierarchyPlugin.Editor.Settings
{
	class PLA_Window : ScriptableObject
	{
	}


	[CustomEditor(typeof(PLA_Window))]
	class ProjectlighterAutoModSettingsEditor : MainRoot
	{
		internal static string set_text = "Use Auto Highlighter Mod (Project Window)";
		internal static string set_key = "USE_PROJECT_AUTO_HIGHLIGHTER_MOD";
		public override VisualElement CreateInspectorGUI()
		{
			return base.CreateInspectorGUI();
		}
		public override void OnInspectorGUI()
		{
			Draw.RESET();

			Draw.BACK_BUTTON();
			Draw.TOG_TIT(set_text, set_key);
			Draw.Sp(10);

			using (ENABLE.USE(set_key))
			{

				//HighlighterManualModSettingsEditor.ASD(this, "HIGHLIGHTER_PROJECT", 1);
				//HighlighterManualModSettingsEditor.ASD(this, "HIGHLIGHTER_PROJECT", 1);
				HighlighterManualModSettingsEditor.ASD(this, p.par_e.PROJ_HIGH_SET, 1);

			}
		}
	}
}

