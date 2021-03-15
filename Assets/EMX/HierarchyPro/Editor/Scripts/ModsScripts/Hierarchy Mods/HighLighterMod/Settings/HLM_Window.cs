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
	class HLM_Window : ScriptableObject
	{
	}


	[CustomEditor(typeof(HLM_Window))]
	class HighlighterManualModSettingsEditor : MainRoot
	{
		internal static string set_text = "Use Manual Highlighter Mod (Hierarchy Window)";
		internal static string set_key = "USE_HIERARCHY_MANUAL_HIGHLIGHTER_MOD";
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



				ASD(this, p.par_e.HIER_HIGH_SET, 0);








				//	adapter.par.COLOR_ICON_SIZE = Mathf.Clamp(EditorGUI.IntField(lineRect, "<b>Custom Icons</b> size '" + EditorGUIUtility.singleLineHeight + "'", adapter.par.COLOR_ICON_SIZE), 10, 30);
				//	adapter._S_USEdefaultIconSize = adapter.TOGGLE_LEFT(lineRect, "<i>Use Default Icons size:</i>", adapter._S_USEdefaultIconSize);
				// adapter._S_defaultIconSize = Mathf.Clamp(EditorGUI.IntField(lineRect, "<b>Defaul Iconst</b> size '" + EditorGUIUtility.singleLineHeight + "'", adapter._S_defaultIconSize), 10, 30);

				//if (adapter.IS_HIERARCHY())
				//{
				//
				//	EditorGUI.BeginChangeCheck();
				//	lineRect.y += lineRect.height;
				//	adapter.par.SHOW_NULLS = adapter.TOGGLE_LEFT(lineRect, "Show Locator for Object without Component", adapter.par.SHOW_NULLS);
				//
				//	lineRect.y += lineRect.height;
				//	adapter.par.SHOW_PREFAB_ICON = adapter.TOGGLE_LEFT(lineRect, "Show Prefab icon", adapter.par.SHOW_PREFAB_ICON);
				//
				//	lineRect.y += lineRect.height;
				//	adapter.par.SHOW_MISSINGCOMPONENTS = adapter.TOGGLE_LEFT(lineRect, "Show Warning if Object has missing Component", adapter.par.SHOW_MISSINGCOMPONENTS);
				//	if (EditorGUI.EndChangeCheck())
				//	{
				//		if (adapter.OnClearObjects != null) adapter.OnClearObjects();
				//	}
				//}



			}


		}


		internal static void QWE(MainRoot r, EditorSettingsAdapter.HighlighterSettings KEY, int pluginID)
		{
			GUI.Label(Draw.R, "Open left Drow-Down window button location:");
			Settings.Draw.TOOLBAR(new[] { "None", "Left 16x16", "Left Wide", "Icon 16x16" }, /*-*/"HIGHLIGHTER_HIERARCHY_BUTTON_LOCATION", overrideObject: KEY);
			Draw.HELP("You also can open window using right click");
			GUI.Label(Settings.Draw.R, "Draw little marker at button:");
			Settings.Draw.TOOLBAR(new[] { "None", "Only Opened", "Always Hovered" }, /*-*/"HIGHLIGHTER_HIERARCHY_DRAW_BUTTON_RECTMARKER", overrideObject: KEY, tooltips: new[] { "None", "The marker will be displayed when the left Drow-Down window is open", "The marker will be displayed when the mouse is hover of the line of hierarshy window" });
			Draw.FIELD("Marker Size '3':", /*-*/"HIGHLIGHTER_HIERARCHY_BUTTON_RECTMARKER_SIZE", 1, 10, overrideObject: KEY);
		}

		//TEXTURE_STYLE

		internal static void ASD(MainRoot r, EditorSettingsAdapter.HighlighterSettings KEY, int pluginID)
		{

			Draw.FIELD("HighLighter Opacity:", /*-*/"HIGHLIGHTER_COLOR_OPACITY", 0, 1, overrideObject: KEY);
			Draw.TOG("Overlap left unity column:", /*-*/"HIGHLIGHTER_LEFT_OVERLAP", overrideObject: KEY);
			Draw.TOG("Group broadcasted child:", /*-*/"HIGHLIGHTER_GROUPING_CHILD_MODE", overrideObject: KEY);
			using (r.ENABLE.USE(/*-*/"HIGHLIGHTER_GROUPING_CHILD_MODE", 0, inverce: true, overrideObject: KEY)) Draw.FIELD("Vertical each line padding '1':", /*-*/"HIGHLIGHTER_BGCOLOR_PADDING", 0, 16, overrideObject: KEY);
			using (r.ENABLE.USE(/*-*/"HIGHLIGHTER_GROUPING_CHILD_MODE", 0, overrideObject: KEY)) Draw.FIELD("Grouped texture grow size '3':", /*-*/"HIGHLIGHTER_TEXTURE_GROW", 0, 16, overrideObject: KEY);

			Draw.Sp(10);
			Draw.HRx2();

			QWE( r,  KEY,  pluginID);

			Draw.Sp(10);
			Draw.HRx2();

			GUI.Label(Draw.R, "Custom icons location:");
			Settings.Draw.TOOLBAR(new[] { "Next to 'Label'", "Next to 'Foldout'", "Align 'Left'" }, /*-*/"HIGHLIGHTER_CUSTOM_ICON_LOCATION", overrideObject: KEY);
			Draw.TOG("Draw marker if custom icons assigned:", /*-*/"HIGHLIGHTER_DRAW_ICON_IF_CUSTOM_ASIGNED", overrideObject: KEY);

			Draw.Sp(10);
			Draw.HRx2();

			using (r.GRO.UP())
			{
				GUI.Label(Draw.R, "Highlighter external bg texture:");
				Settings.Draw.TOOLBAR(new[] { "None'", "Box", "TextArea", "External" }, /*-*/"HIGHLIGHTER_TEXTURE_STYLE", overrideObject: KEY);

				using (r.ENABLE.USE(/*-*/"HIGHLIGHTER_TEXTURE_GUID_ALLOW", 0, overrideObject: KEY))
				{
					Texture2D newScript = r.p.HL_SET_G(pluginID).HIghlighterExternalTexture;
					try
					{
						var c = GUI.color;
						if (!newScript) GUI.color *= Color.red;
						newScript = (Texture2D)EditorGUILayout.ObjectField(r.p.HL_SET_G(pluginID).HIghlighterExternalTexture, typeof(Texture2D), false);
						GUI.color = c;
					}
					catch
					{
						newScript = r.p.HL_SET_G(pluginID).HIghlighterExternalTexture;
					}
					if (newScript != r.p.HL_SET_G(pluginID).HIghlighterExternalTexture) r.p.HL_SET_G(pluginID).HIghlighterExternalTexture = newScript;
				}

				using (r.ENABLE.USE(/*-*/"HIGHLIGHTER_TEXTURE_BORDER_ALLOW", 0, overrideObject: KEY))
				{
					Draw.FIELD("Texture Borders:", /*-*/"HIGHLIGHTER_TEXTURE_BORDER", 0, 16, overrideObject: KEY);

					Draw.Sp(10);
					Draw.HRx2();

					Draw.TOG("Use Special Shader:", /*-*/"HIGHLIGHTER_USE_SPECUAL_SHADER", overrideObject: KEY);
					using (r.ENABLE.USE(/*-*/"HIGHLIGHTER_USE_SPECUAL_SHADER", 0, overrideObject: KEY))
					{
						GUI.Label(Draw.R, "Highlighter Special Drawing Shader:");
						Settings.Draw.TOOLBAR(new[] { "Normal", "Additive" }, /*-*/"HIGHLIGHTER_USE_SPECUAL_SHADER_TYPE", overrideObject: KEY);


						Shader newScript = r.p.HL_SET_G(pluginID).SHADER_A.ExternalShaderReference;
						var rect = EditorGUILayout.GetControlRect();
						rect.width /= 2;
						var og = GUI.enabled;
						GUI.enabled = og & r.p.HL_SET_G(pluginID).HIGHLIGHTER_USE_SPECUAL_SHADER_TYPE == 0;

						try
						{
							newScript = (Shader)EditorGUI.ObjectField(rect, r.p.HL_SET_G(pluginID).SHADER_A.ExternalShaderReference, typeof(Shader), false);
						}

						catch
						{
							newScript = r.p.HL_SET_G(pluginID).SHADER_A.ExternalShaderReference;
						}

						if (newScript != r.p.HL_SET_G(pluginID).SHADER_A.ExternalShaderReference)
						{
							r.p.HL_SET_G(pluginID).SHADER_A.ExternalShaderReference = newScript;
						}

						GUI.enabled = og & r.p.HL_SET_G(pluginID).HIGHLIGHTER_USE_SPECUAL_SHADER_TYPE == 1;
						newScript = r.p.HL_SET_G(pluginID).SHADER_B.ExternalShaderReference;
						rect.x += rect.width;

						try
						{
							newScript = (Shader)EditorGUI.ObjectField(rect, r.p.HL_SET_G(pluginID).SHADER_B.ExternalShaderReference, typeof(Shader), false);
						}

						catch
						{
							newScript = r.p.HL_SET_G(pluginID).SHADER_B.ExternalShaderReference;
						}

						GUI.enabled = og;

						if (newScript != r.p.HL_SET_G(pluginID).SHADER_B.ExternalShaderReference)
						{
							r.p.HL_SET_G(pluginID).SHADER_B.ExternalShaderReference = newScript;
						}
					}
				}
			}


		}
	}
}















/*
internal static Rect DrawIconAligmentSettingsLine(Rect lineRect, PluginInstance adapter)
{


	var nv = adapter.TOOGLE_POP_INVERCE(ref lineRect, "Icons Placement", _S_bgIconsPlace, "Next to 'Label'", "Next to 'Foldout'", "Align 'Left'");
	if (nv != _S_bgIconsPlace)
	{
		_S_bgIconsPlace = nv;
		adapter.RepaintAllViews();
	}


	return lineRect;
}
internal static void DrawHoverPlaceSettingLine(PluginInstance adapter)
{

	lineRect.height *= 10;
	GUI.Label(Settings.Draw.R, "Open highlighter button location:");
#if UNITY_2018_3_OR_NEWER
	Settings.Draw.TOOLBAR(new[] { "<Left", "-Icon-", "<Left and -Icon-" }, "HIGHLIGHTER_HIERARCHY_BUTTON_LOCATION");
#else
			Settings.Draw.TOOLBAR(new[] { "<Left" }, "HIGHLIGHTER_HIERARCHY_BUTTON_LOCATION");
#endif
	GUI.Label(Settings.Draw.R, "Draw rect marker at HighLighter Button");
	Settings.Draw.TOOLBAR(new[] { "None", "Window Open Only", "Window and Hover" }, "HIGHLIGHTER_HIERARCHY_BUTTON_RECTMARKER");
}




void DrawDettings(Rect inputrect)
{
	inputrect.width -= 4;
	var lineRect = inputrect;
	//	lineRect.height = EditorGUIUtility.singleLineHeight;
	lineRect.x += 20;
	lineRect.width -= 40;

	GUILayout.BeginArea(lineRect);

	GUILayout.EndArea();

	adapter.InitializeStyles();
	EditorGUI.BeginChangeCheck();


	lineRect.y += lineRect.height;
	adapter.par.highligterOpacity = Mathf.Clamp(EditorGUI.FloatField(lineRect, "HighLighter Opacity:", adapter.par.highligterOpacity), 0, 1);
	lineRect.y += lineRect.height;
	adapter._S_BottomPaddingForBgColor = Mathf.Clamp(EditorGUI.IntField(lineRect, "Vertical Padding '1':", adapter._S_BottomPaddingForBgColor), 0, 16);
	lineRect.y += lineRect.height;

	var on2 = GUI.enabled;
	GUI.enabled = !adapter.IS_PROJECT();
	lineRect.y += lineRect.height;
	lineRect = DrawHoverPlaceSettingLine(lineRect, adapter);
	lineRect.y += (EditorGUIUtility.singleLineHeight);
	lineRect.y += (EditorGUIUtility.singleLineHeight);
	GUI.enabled = on2;

	lineRect.y += lineRect.height;
	adapter.ENABLE_RICH();
	adapter.par.COLOR_ICON_SIZE = Mathf.Clamp(EditorGUI.IntField(lineRect, "<b>Custom Icons</b> size '" + EditorGUIUtility.singleLineHeight + "'", adapter.par.COLOR_ICON_SIZE), 10, 30);
	adapter.DISABLE_RICH();
	lineRect.y += lineRect.height;
	adapter._S_USEdefaultIconSize = adapter.TOGGLE_LEFT(lineRect, "<i>Use Default Icons size:</i>", adapter._S_USEdefaultIconSize);
	lineRect.y += lineRect.height;
	var on = GUI.enabled;
	GUI.enabled &= adapter._S_USEdefaultIconSize;
	adapter.ENABLE_RICH();
	adapter._S_defaultIconSize = Mathf.Clamp(EditorGUI.IntField(lineRect, "<b>Defaul Iconst</b> size '" + EditorGUIUtility.singleLineHeight + "'", adapter._S_defaultIconSize), 10, 30);
	adapter.DISABLE_RICH();
	GUI.enabled = on;

	lineRect.y += lineRect.height;
	lineRect = DrawIconAligmentSettingsLine(lineRect, adapter);

	lineRect.y += lineRect.height;




	on = GUI.enabled;
	GUI.enabled = HAS_LABEL_ICON();
	var nv = adapter.TOOGLE_POP(ref lineRect, "Draw yellow marks next to the assigned icons", adapter.par.BottomParams.DRAW_FOLDER_STARMARK ? 1 : 0, "Disable", "Enable") == 1;
	if (nv != adapter.par.BottomParams.DRAW_FOLDER_STARMARK)
	{
		adapter.par.BottomParams.DRAW_FOLDER_STARMARK = nv;
		adapter.RepaintAllViews();
	}

	GUI.enabled = on;

	lineRect.y += (EditorGUIUtility.singleLineHeight);
	lineRect.y += (EditorGUIUtility.singleLineHeight);



	if (adapter.IS_HIERARCHY())
	{

		EditorGUI.BeginChangeCheck();
		lineRect.y += lineRect.height;
		adapter.par.SHOW_NULLS = adapter.TOGGLE_LEFT(lineRect, "Show Locator for Object without Component", adapter.par.SHOW_NULLS);

		lineRect.y += lineRect.height;
		adapter.par.SHOW_PREFAB_ICON = adapter.TOGGLE_LEFT(lineRect, "Show Prefab icon", adapter.par.SHOW_PREFAB_ICON);

		lineRect.y += lineRect.height;
		adapter.par.SHOW_MISSINGCOMPONENTS = adapter.TOGGLE_LEFT(lineRect, "Show Warning if Object has missing Component", adapter.par.SHOW_MISSINGCOMPONENTS);
		if (EditorGUI.EndChangeCheck())
		{
			if (adapter.OnClearObjects != null) adapter.OnClearObjects();
		}
	}



	if (EditorGUI.EndChangeCheck())
	{
		adapter.SavePrefs();
		adapter.RepaintAllViews();
	}


}*/
