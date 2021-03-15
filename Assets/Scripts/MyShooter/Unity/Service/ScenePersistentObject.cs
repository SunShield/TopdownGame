using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyShooter.Unity.Service
{
	public class ScenePersistentObject : ManuallyUpdatableBehaviour
	{
		[SerializeField] private List<string> _scenesWhereToDestroy;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (!_scenesWhereToDestroy.Contains(scene.name)) return;

			SceneManager.sceneLoaded -= OnSceneLoaded;
			if(this != null)
				Destroy(gameObject);
		}
	}
}
