using MyShooter.Core.Entities.States;
using MyShooter.Core.Environment.Events;
using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Breaking
{
	/// <summary>
	/// This component determines entitie's ability to break (die).
	/// It holds all the related data: health and defences
	/// </summary>
	public class Breaker : EntityComponent
	{
		private readonly EntityDeathEventArgs Args = new EntityDeathEventArgs();
		private readonly List<DamageType> DamageTypes = new List<DamageType>()
		{ 
			DamageType.Physical, DamageType.Fire, DamageType.Cold, DamageType.Acid, DamageType.Lightnng, DamageType.Magical 
		};

		[SerializeField] private StatsState _stats;

		public StatsState StatsState => _stats;
		public bool IsBroken => _stats.CurrentHealth == 0;

		protected override void AwakeInternal()
		{
			Args.Type = HolderEntity.Type;
		}

		public void RecieveDamage(DamageInstance incomingDamage)
		{
			if (!_stats.IsHittable || !_stats.IsDamagable) return; // TODO: Separate "hit" and "damage" and create events for both

			var resultingDamage = CalculateResultingDamage(incomingDamage);
			ReceiveDamageInternal(resultingDamage);
		}

		private DamageInstance CalculateResultingDamage(DamageInstance incomingDamage)
		{
			var resultingDamage = new DamageInstance();
			foreach (var damageType in DamageTypes)
			{
				resultingDamage[damageType] = GetDamageByType(incomingDamage, damageType);
			}
			return resultingDamage;
		}

		private int GetDamageByType(DamageInstance damage, DamageType type)
		{
			var damageValue = damage[type];
			var defences = _stats.Defences;
			return (damageValue - defences.Armors[type]) * (1 - defences.Resistances[type]);
		}

		private void ReceiveDamageInternal(DamageInstance damage)
		{
			int resultingDamage = 0;
			foreach (var damageType in DamageTypes)
			{
				resultingDamage += damage[damageType];
			}

			_stats.CurrentHealth = Mathf.Clamp(_stats.CurrentHealth - resultingDamage, 0, _stats.CurrentHealth);
			// On Damage event here

			if(IsBroken)
			{
				Break();
			}
		}

		private void Break()
		{
			Destroy(gameObject); // TODO: temporary, before pooling is implemented
			EventBus.GetEvent<EntityDeathEvent>().InvokeForGlobal(Args);
			EventBus.GetEvent<EntityDeathEvent>().InvokeForId(GoId, Args); // TOOD: Store all IDs subscribed for each event, to make InvokeForGlobal affect methods subscribed with "SubscribeForId"!
		}
	}
}
