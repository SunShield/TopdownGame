using System;

namespace MyShooter.Unity.Entities.Effects
{
	[Flags]
	public enum EffectReactionType
	{
		None = 0,
		OnApply = 1,
		OnRemove = 2,
		OnEntityCollide = 4,
		OnHolderCollide = 8,
		OnHolderUncollide = 16,
		OnEntityHit = 32,
		OnHolderHit = 64,
		OnEntityReceiveDamage = 128,
		OnHolderReceiveDamage = 256,
		OnEntityHealthChanged = 512,
		OnHolderHealthChanged = 1024,
		OnEntityManaChanged = 2048,
		OnHolderManaChanged = 4096,
		OnEntityStartMovement = 8192,
		OnHolderStartMovement = 16384,
		OnEntityEndMovement = 32768,
		OnHolderEndMovement = 65536,
		OnEntityDeath = 131072,
		OnHolderDeath = 262144
	}
}
