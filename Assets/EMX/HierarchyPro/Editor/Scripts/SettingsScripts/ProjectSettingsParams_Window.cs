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
	class ProjectSettingsParams_Window : ScriptableObject
	{

	}



	[CustomEditor(typeof(ProjectSettingsParams_Window))]
	class ProjectSettingsParamsEditor : MainRoot
	{


		internal static string set_text = "Use Project Settings (Project Window)";
		internal static string set_key = "USE_PROJECT_SETTINGS";


		public override void OnInspectorGUI()
		{

			Draw.RESET();

			Draw.BACK_BUTTON();
			Draw.TOG_TIT(set_text, set_key);

		//	GUI.Button(Draw.R2, "Project Settings", Draw.s("preToolbar"));
			// GUI.Button( Draw.R, "Common Settings", Draw.s( "insertionMarker" ) );
			using (GRO.UP())
			{



				MainSettingsParamsEditor.QWE(this, p.par_e.PROJ_WIN_SET, () =>
				{
				}, () =>
				{
				}, () =>
				{
				});




			}
		}

	}
}
