using UnityEngine;

namespace MyShooter.Unity.UI.MainMenu
{
	public class ExitGameButtonAction : ButtonAction
	{
		protected override void OnClickAction()
		{
			Application.Quit();
		}
	}
}
