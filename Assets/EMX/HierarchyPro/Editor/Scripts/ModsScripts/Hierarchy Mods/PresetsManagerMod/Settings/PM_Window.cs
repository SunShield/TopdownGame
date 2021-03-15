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
    class PM_Window : ScriptableObject
    {
    }


         [CustomEditor( typeof(PM_Window) )]
    class PresetsManagerModSettingsEditor : MainRoot
    {
        internal static string set_text = "Use Presets Manager Mod (Hierarchy Window)";
        internal static string set_key = "USE_CUSTOM_PRESETS_MOD";
        public override VisualElement CreateInspectorGUI( )
        {
            return base.CreateInspectorGUI();
        }
        public override void OnInspectorGUI( )
        {
            Draw.RESET();

            Draw.BACK_BUTTON();
            Draw.TOG_TIT(set_text, set_key);
             Draw.HELP("This function is available in the same window as the Highlighter"); //, I will add a bit later the ability to save only selected variables, and common paste for different scripts
            Draw.Sp(10);

            using (ENABLE.USE(set_key))
            {

                HighlighterManualModSettingsEditor.QWE(this, p.par_e.HIER_HIGH_SET, 0);

                Draw.Sp(10);
                Draw.HRx2();

                Draw.TOG("Save objects references:", "PRESETS_SAVE_GAMEOBJEST");
                Draw.TOG("Skip assign if value is null:", "PRESETS_SKIP_NULL");
            }
        }
    }
}
