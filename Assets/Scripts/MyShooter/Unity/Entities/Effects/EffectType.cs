namespace MyShooter.Unity.Entities.Effects
{
	public enum EffectType
	{
		Fundamental = 0, // fundamental effects rule the most basic properties of entuty - like, is it breakable on wall contact, etc | items grant fundamental effects
		Granted = 1 // effects granted by skills or other effects
	}
}
