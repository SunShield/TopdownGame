using MyShooter.Core.Constants;
using UnityEngine.SceneManagement;

namespace MyShooter.Unity.UI.MainMenu
{
	public class StartNewGameButtonAction : ButtonAction
	{
		protected override void OnClickAction()
		{
			SceneManager.LoadSceneAsync(SceneNameConstants.StartingSceneName);
		}
	}
}
