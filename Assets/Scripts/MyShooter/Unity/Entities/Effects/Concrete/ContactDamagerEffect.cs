using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using MyShooter.Unity.Entities.Components.Breaking;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	// TODO: Change this code, now this allows to affect only one entity each _damageDelay.
	public class ContactDamagerEffect : ContactActionEffect, IDamagingEffect
	{
		[field: SerializeField] public DamageInstance Damage { get; protected set; }
		[SerializeField] private float _damageDelay = 1f;
		private Dictionary<int, ContactEntityInfo> _damageDelayTimers2 = new Dictionary<int, ContactEntityInfo>();
		private List<int> _idsToRemove = new List<int>();

		public void SetDamage(DamageInstance damage) => Damage = damage;
		public override bool Compare(Effect another) => true;

		protected override void DoOnHolderCollide(EntityCollideEventArgs args)
		{
			var entity = args.CollidedEntity;
			var id = entity.GetInstanceID();
			_damageDelayTimers2.Add(id, new ContactEntityInfo(entity, 0f));
			DealDamage(id);
			StartTimerForEntity(id);
		}

		protected override void DoOnHolderUncollide(EntityCollideEventArgs args)
		{
			var entity = args.CollidedEntity;
			var id = entity.GetInstanceID();
			if (_damageDelayTimers2.ContainsKey(id))
				_damageDelayTimers2.Remove(id);
		}

		private void DealDamage(int id)
		{
			var entity = _damageDelayTimers2[id].Entity;
			if (!entity.IsDamagable) return;
			entity.TryReceiveDamage(Damage);
		}

		public override void UpdateManually()
		{
			ProcessEntities();
			ClearRemovedEntities();
		}

		private void ProcessEntities()
		{
			foreach (var id in _damageDelayTimers2.Keys)
			{
				bool entityRemoved = CheckEntityRemoved(id);
				if (entityRemoved) continue;

				ProcessEntity(id);
			}
		}

		private void ProcessEntity(int id)
		{
			ProceedTimerForEntity(id);
			if (CheckEntityNeedsBeDamaged(id))
			{
				DealDamage(id);
				StartTimerForEntity(id);
			}
		}

		private bool CheckEntityRemoved(int id)
		{
			if (_damageDelayTimers2[id].Entity == null)
			{
				_idsToRemove.Add(id);
				return true; // not sure if this is possible, but it's better to be aware
			}

			return false;
		}

		private void ClearRemovedEntities()
		{
			for (int i = _idsToRemove.Count - 1; i >= 0; i--)
			{
				_damageDelayTimers2.Remove(i);
				_idsToRemove.RemoveAt(i);
			}
		}

		private void StartTimerForEntity(int id) => _damageDelayTimers2[id].Timer = _damageDelay;
		private void ProceedTimerForEntity(int id) => _damageDelayTimers2[id].Timer = Mathf.Clamp(_damageDelayTimers2[id].Timer - Time.deltaTime, 0f, _damageDelayTimers2[id].Timer);
		private bool CheckEntityNeedsBeDamaged(int id) => _damageDelayTimers2[id].Timer == 0f;

		class ContactEntityInfo
		{
			public BattleEntity Entity;
			public float Timer;

			public ContactEntityInfo(BattleEntity entity, float timer)
			{
				Entity = entity;
				Timer = timer;
			}
		}
	}
}
