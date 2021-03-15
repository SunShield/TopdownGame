namespace MyShooter.Unity.UI.Player
{
	public class PlayerManaUi : PlayerStatUi
	{
		protected override float CurrentValue => Player.SkillsState.CurrentMana;
		protected override float MaxValue => Player.SkillsState.MaximumMana.FinalValue;
	}
}
