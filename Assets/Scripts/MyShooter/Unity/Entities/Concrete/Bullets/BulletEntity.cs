using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Components.Breaking;
using MyShooter.Unity.Entities.Effects;
using MyShooter.Unity.Entities.Effects.Concrete;
using UnityEngine;

namespace MyShooter.Unity.Entities.Concrete.Bullets
{
	/// <summary>
	/// Bullets are special entities meant to "transfer affections" - deal damage, heal, emit light, etc.
	/// In general, they are regular entitites (and even can have hp and mana in special cases mb), 
	/// and the only key difference is design-level recognition of the entity
	/// </summary>
	public class BulletEntity : BattleEntity
	{
		public override EntityType Type => EntityType.Bullet;

		// THANK YOU UNITY for not serialiaing interface-type fields and forcing people to draw custom drawers for each of them. ><
		[SerializeField] private Effect[] _configurableEffects;

		public void SetDamage(DamageInstance damage)
		{
			if (_configurableEffects.Length <= 0) return;

			foreach (var damagingEffect in _configurableEffects)
			{
				var effectTyped = damagingEffect as IDamagingEffect;
				effectTyped?.SetDamage(damage);
			}
		}

		public void ConfigureMoveDecisionMaker(Vector2 configureData) => MoveDecisionMaker.Configure(configureData);
	}
}
