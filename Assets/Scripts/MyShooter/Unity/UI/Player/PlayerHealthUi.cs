namespace MyShooter.Unity.UI.Player
{
	public class PlayerHealthUi : PlayerStatUi
	{
		protected override float CurrentValue => Player.StatsState.CurrentHealth;
		protected override float MaxValue => Player.StatsState.MaxHealth.FinalValue;
	}
}
