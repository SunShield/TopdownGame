using MyShooter.Unity.Entities.Concrete.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyShooter.Unity.LevelLayout
{
	[CreateAssetMenu(menuName = "My Assets/Room Switcher")]
	public class RoomSwitcher : ScriptableObject
	{
		[SerializeField] private LayoutGenerator Generator;
		[System.NonSerialized] private string _currentSceneName;

		public void SwitchRoom()
		{
			_currentSceneName = SceneManager.GetActiveScene().name;
			var newRoomName = Generator.GenerateNewRoomName();
			SceneManager.LoadSceneAsync(newRoomName, LoadSceneMode.Additive);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene loadedeScene, LoadSceneMode mode)
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;

			SceneManager.UnloadSceneAsync(_currentSceneName);
			SceneManager.sceneUnloaded += OnOldSceneUnloaded;
		}

		private void OnOldSceneUnloaded(Scene unloadedScene)
		{
			SceneManager.sceneUnloaded -= OnOldSceneUnloaded;

			var character = GameObject.FindObjectOfType<PlayerEntity>();
			var room = GameObject.FindObjectOfType<Room>();
			character.Tran.position = room.EnterPoint;
		}
	}
}
