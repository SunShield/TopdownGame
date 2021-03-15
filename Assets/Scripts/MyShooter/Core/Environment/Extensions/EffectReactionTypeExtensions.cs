using MyShooter.Unity.Entities.Effects;

namespace MyShooter.Core.Service.Extensions
{
	public static class EffectReactionTypeExtensions
	{
		public static EffectReactionType AddFlag(this EffectReactionType current, EffectReactionType toAdd)
		{
			return current | toAdd;
		}

		public static bool HasFlag(this EffectReactionType current, EffectReactionType toCheck)
		{
			return (current & toCheck) != 0;
		}
	}
}
