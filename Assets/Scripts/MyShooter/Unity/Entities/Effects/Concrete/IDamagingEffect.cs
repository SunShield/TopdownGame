using MyShooter.Unity.Entities.Components.Breaking;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public interface IDamagingEffect
	{
		DamageInstance Damage { get; }
		void SetDamage(DamageInstance damage);
	}
}
