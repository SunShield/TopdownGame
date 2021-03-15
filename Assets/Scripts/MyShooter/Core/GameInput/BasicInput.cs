using UnityEngine;

namespace MyShooter.Core.GameInput
{
	public static class BasicInput
	{
		public static bool UpPressed => Input.GetKey(KeyCode.W);
		public static bool RightPressed => Input.GetKey(KeyCode.D);
		public static bool DownPressed => Input.GetKey(KeyCode.S);
		public static bool LeftPressed => Input.GetKey(KeyCode.A);

		public static bool DashButtonPressed => Input.GetKeyDown(KeyCode.Space);
		public static bool MainSkillButtonPressed => Input.GetMouseButton(0);
		public static bool SecondarySkillButtonPressed => Input.GetMouseButton(1);
		public static bool SigilSkillButtonPressed => Input.GetKeyDown(KeyCode.E);
		public static bool UtilitySkillButtonPressed => Input.GetKeyDown(KeyCode.LeftShift);
		public static bool SignatureSkillButtonPressed => Input.GetKeyDown(KeyCode.R);
	}
}
