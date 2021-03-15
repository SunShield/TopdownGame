using UnityEngine.UI;

namespace MyShooter.Unity.UI.MainMenu
{
	public abstract class ButtonAction : ManuallyUpdatableBehaviour
	{
		private Button _button;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnClickAction);
		}

		protected override void OnDestroyAutomatically()
		{
			_button.onClick.RemoveAllListeners();
		}

		protected abstract void OnClickAction();
	}
}
