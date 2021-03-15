using UnityEngine;

namespace MyShooter.Unity.Data
{
	/// <summary>
	/// Testing architecture with global ScriptableObjects - this makes our game data accessable from any point.
	/// This approach is interesting in terms of "systemless" architecture I am trying to approach in this project.
	/// </summary>
	public class GlobalScriptableObject<TObject> : ScriptableObject
		where TObject : ScriptableObject
	{
		private static TObject _instance;
		public static TObject Instance
		{
			get
			{
				if(_instance == null)
				{
					TObject[] foundObjects = null;
#if UNITY_EDITOR
					string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(TObject)}");
					foundObjects = new TObject[guids.Length];
					for (int i = 0; i < guids.Length; i++)
						foundObjects[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<TObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]));
#else
					foundObjects = Resources.FindObjectsOfTypeAll<ValueModifiersDataObject>();
#endif

					if (foundObjects.Length == 0 || foundObjects.Length > 1)
						Debug.LogError($"You must have just one object of type {typeof(TObject)}. You have {foundObjects.Length} instead.");

					_instance = foundObjects.Length > 0 ? foundObjects[0] : null;
				}

				return _instance;
			}
		}

		// If we are sure we don't need this loaded anymore.
		public static void Free() => Resources.UnloadAsset(_instance);
	}
}
