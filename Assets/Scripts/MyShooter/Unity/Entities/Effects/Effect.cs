using MyShooter.Core.Environment.Events;
using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using MyShooter.Core.Service.Utitilities;
using MyShooter.Unity.Service.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace MyShooter.Unity.Entities.Effects
{
	public abstract class Effect : ManuallyUpdatableBehaviour
	{
		private static int _id;
		public static int GetNextId() => _id++;

		private static Dictionary<Type, EffectReactionType> _reactions = new Dictionary<Type, EffectReactionType>();
		private static Dictionary<EffectReactionType, string> _reactionMethodNames = new Dictionary<EffectReactionType, string>()
		{
			{ EffectReactionType.OnApply,                "OnApply" },
			{ EffectReactionType.OnRemove,               "OnRemove" },
			{ EffectReactionType.OnEntityCollide,        "OnEntityCollide" },
			{ EffectReactionType.OnHolderCollide,        "OnHolderCollide" },
			{ EffectReactionType.OnHolderUncollide,      "OnHolderUncollide" },
			{ EffectReactionType.OnEntityHit,            "OnEntityHit" },
			{ EffectReactionType.OnHolderHit,            "OnHolderHit" },
			{ EffectReactionType.OnEntityReceiveDamage,  "OnEntityRecieveDamage" },
			{ EffectReactionType.OnHolderReceiveDamage,  "OnHolderReceiveDamage" },
			{ EffectReactionType.OnEntityHealthChanged,  "OnEntityHealthChanged" },
			{ EffectReactionType.OnHolderHealthChanged,  "OnHolderHealthChanged" },
			{ EffectReactionType.OnEntityManaChanged,    "OnEntityManaChanged" },
			{ EffectReactionType.OnHolderManaChanged,    "OnHolderManaChanged" },
			{ EffectReactionType.OnEntityStartMovement,  "OnEntityStartMovement" },
			{ EffectReactionType.OnHolderStartMovement,  "OnHolderStartMovement" },
			{ EffectReactionType.OnEntityEndMovement,    "OnEntityEndMovement" },
			{ EffectReactionType.OnHolderEndMovement,    "OnHolderEndMovement" },
			{ EffectReactionType.OnEntityDeath,          "OnEntityDeath" },
			{ EffectReactionType.OnHolderDeath,          "OnHolderDeath" },
		};

		[Inject] protected EventBus EventBus;

		[SerializeField] [ReadOnly] protected BattleEntity Holder;
		[SerializeField] private string _name;
		[SerializeField] private EffectType _type;
		[SerializeField] private EffectState _state;

		protected EffectReactionType Reactions => _reactions[GetType()];
		public string Name => _name;
		public int EffectId { get; private set; }
		public int HolderId { get; private set; }

		public EffectType Type => _type;
		public EffectState State => _state;

		#region Initialization/Deinitialization

		protected override void InitializeAutomatically()
		{
			StoreHolderId();
			SetEffectSetId();
			GetReactions();
			SubscribeForEvents();
		}
		private void SetEffectSetId()
		{
			EffectId = GetNextId();
		}

		private void StoreHolderId()
		{
			HolderId = transform.root.gameObject.GetInstanceID();
		}

		private void GetReactions()
		{
			var type = GetType();
			if (!_reactions.ContainsKey(type))
				DetermineReactions(type);
		}

		private void DetermineReactions(Type type)
		{
			EffectReactionType reactions = EffectReactionType.None;
			foreach (var reactionType in _reactionMethodNames.Keys)
			{
				var method = GetReactionMethodOverride(type, reactionType);
				if (method != null && ReflectionUtilities.IsOverride(method))
					reactions = reactions | reactionType;
			}
			_reactions.Add(type, reactions);
		}

		private MethodInfo GetReactionMethodOverride(Type type, EffectReactionType reactionType)
			=> type.GetMethod(_reactionMethodNames[reactionType], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// not the best code I have ever wrote, to be honest...
		private void SubscribeForEvents()
		{
			if (Reactions.HasFlag(EffectReactionType.OnEntityCollide))
				EventBus.GetEvent<EntityStartCollideEvent>().SubscribeForGlobal(OnEntityCollide);
			if (Reactions.HasFlag(EffectReactionType.OnHolderCollide))
				EventBus.GetEvent<EntityStartCollideEvent>().SubscribeForId(HolderId, OnHolderCollide);
			if (Reactions.HasFlag(EffectReactionType.OnHolderUncollide))
				EventBus.GetEvent<EntityEndCollideEvent>().SubscribeForId(HolderId, OnHolderUncollide);

			if (Reactions.HasFlag(EffectReactionType.OnEntityHit))
				EventBus.GetEvent<EntityHitEvent>().SubscribeForGlobal(OnEntityHit);
			if (Reactions.HasFlag(EffectReactionType.OnHolderHit))
				EventBus.GetEvent<EntityHitEvent>().SubscribeForId(HolderId, OnHolderHit);

			if (Reactions.HasFlag(EffectReactionType.OnEntityReceiveDamage))
				EventBus.GetEvent<EntityRecieveDamageEvent>().SubscribeForGlobal(OnEntityReceiveDamage);
			if (Reactions.HasFlag(EffectReactionType.OnHolderReceiveDamage))
				EventBus.GetEvent<EntityRecieveDamageEvent>().SubscribeForId(HolderId, OnHolderReceiveDamage);

			if (Reactions.HasFlag(EffectReactionType.OnEntityHealthChanged))
				EventBus.GetEvent<EntityHealthChangedEvent>().SubscribeForGlobal(OnEntityHealthChanged);
			if (Reactions.HasFlag(EffectReactionType.OnHolderHealthChanged))
				EventBus.GetEvent<EntityHealthChangedEvent>().SubscribeForId(HolderId, OnHolderHealthChanged);

			if (Reactions.HasFlag(EffectReactionType.OnEntityManaChanged))
				EventBus.GetEvent<EntityManaChangedEvent>().SubscribeForGlobal(OnEntityManaChanged);
			if (Reactions.HasFlag(EffectReactionType.OnHolderManaChanged))
				EventBus.GetEvent<EntityManaChangedEvent>().SubscribeForId(HolderId, OnHolderManaChanged);

			if (Reactions.HasFlag(EffectReactionType.OnEntityStartMovement))
				EventBus.GetEvent<EntityStartMovementEvent>().SubscribeForGlobal(OnEntityStartMovement);
			if (Reactions.HasFlag(EffectReactionType.OnHolderStartMovement))
				EventBus.GetEvent<EntityStartMovementEvent>().SubscribeForId(HolderId, OnHolderStartMovement);

			if (Reactions.HasFlag(EffectReactionType.OnEntityEndMovement))
				EventBus.GetEvent<EntityEndMovementEvent>().SubscribeForGlobal(OnEntityEndMovement);
			if (Reactions.HasFlag(EffectReactionType.OnHolderEndMovement))
				EventBus.GetEvent<EntityEndMovementEvent>().SubscribeForId(HolderId, OnHolderEndMovement);

			if (Reactions.HasFlag(EffectReactionType.OnEntityDeath))
				EventBus.GetEvent<EntityDeathEvent>().SubscribeForGlobal(OnEntityDeath);
			if (Reactions.HasFlag(EffectReactionType.OnHolderDeath))
				EventBus.GetEvent<EntityDeathEvent>().SubscribeForId(HolderId, OnHolderDeath);
		}

		private void OnDestroy()
		{
			var type = GetType();
			var reactions = _reactions[type];

			if (reactions.HasFlag(EffectReactionType.OnEntityCollide))
				EventBus.GetEvent<EntityStartCollideEvent>().UnsubscribeFromGlobal(OnEntityCollide);
			if (reactions.HasFlag(EffectReactionType.OnHolderCollide))
				EventBus.GetEvent<EntityStartCollideEvent>().UnsubscribeFromId(HolderId, OnHolderCollide);
			if (Reactions.HasFlag(EffectReactionType.OnHolderUncollide))
				EventBus.GetEvent<EntityEndCollideEvent>().UnsubscribeFromId(HolderId, OnHolderUncollide);

			if (reactions.HasFlag(EffectReactionType.OnEntityHit))
				EventBus.GetEvent<EntityHitEvent>().UnsubscribeFromGlobal(OnEntityHit);
			if (reactions.HasFlag(EffectReactionType.OnHolderHit))
				EventBus.GetEvent<EntityHitEvent>().UnsubscribeFromId(HolderId, OnHolderHit);

			if (reactions.HasFlag(EffectReactionType.OnEntityReceiveDamage))
				EventBus.GetEvent<EntityRecieveDamageEvent>().UnsubscribeFromGlobal(OnEntityReceiveDamage);
			if (reactions.HasFlag(EffectReactionType.OnHolderReceiveDamage))
				EventBus.GetEvent<EntityRecieveDamageEvent>().UnsubscribeFromId(HolderId, OnHolderReceiveDamage);

			if (reactions.HasFlag(EffectReactionType.OnEntityHealthChanged))
				EventBus.GetEvent<EntityHealthChangedEvent>().UnsubscribeFromGlobal(OnEntityHealthChanged);
			if (reactions.HasFlag(EffectReactionType.OnHolderHealthChanged))
				EventBus.GetEvent<EntityHealthChangedEvent>().UnsubscribeFromId(HolderId, OnHolderHealthChanged);

			if (reactions.HasFlag(EffectReactionType.OnEntityManaChanged))
				EventBus.GetEvent<EntityManaChangedEvent>().UnsubscribeFromGlobal(OnEntityManaChanged);
			if (reactions.HasFlag(EffectReactionType.OnHolderManaChanged))
				EventBus.GetEvent<EntityManaChangedEvent>().UnsubscribeFromId(HolderId, OnHolderManaChanged);

			if (reactions.HasFlag(EffectReactionType.OnEntityStartMovement))
				EventBus.GetEvent<EntityStartMovementEvent>().UnsubscribeFromGlobal(OnEntityStartMovement);
			if (reactions.HasFlag(EffectReactionType.OnHolderStartMovement))
				EventBus.GetEvent<EntityStartMovementEvent>().UnsubscribeFromId(HolderId, OnHolderStartMovement);

			if (reactions.HasFlag(EffectReactionType.OnEntityEndMovement))
				EventBus.GetEvent<EntityEndMovementEvent>().UnsubscribeFromGlobal(OnEntityEndMovement);
			if (reactions.HasFlag(EffectReactionType.OnHolderEndMovement))
				EventBus.GetEvent<EntityEndMovementEvent>().UnsubscribeFromId(HolderId, OnHolderEndMovement);

			if (reactions.HasFlag(EffectReactionType.OnEntityDeath))
				EventBus.GetEvent<EntityDeathEvent>().UnsubscribeFromGlobal(OnEntityDeath);
			if (reactions.HasFlag(EffectReactionType.OnHolderDeath))
				EventBus.GetEvent<EntityDeathEvent>().UnsubscribeFromId(HolderId, OnHolderDeath);
		}

		#endregion

		public void Apply(BattleEntity newHolder)
		{
			Holder = newHolder;
			InternalApplyActions();
			OnApply();
		}

		public void Remove()
		{
			OnRemove();
			InternalRemoveActions();
			Destroy(gameObject);
		}

		#region Reactions

		// use those to add "non-reactional" apply/remove actions 
		protected virtual void InternalApplyActions() { }
		protected virtual void InternalRemoveActions() { }

		protected virtual void OnApply() { }
		protected virtual void OnRemove() { }

		protected virtual void OnEntityCollide(EntityCollideEventArgs args) { }
		protected virtual void OnHolderCollide(EntityCollideEventArgs args) { }
		protected virtual void OnHolderUncollide(EntityCollideEventArgs args) { }
		protected virtual void OnEntityHit(EntityHitEventArgs args) { }
		protected virtual void OnHolderHit(EntityHitEventArgs args) { }
		protected virtual void OnEntityReceiveDamage(EntityRecieveDamageEventArgs args) { }
		protected virtual void OnHolderReceiveDamage(EntityRecieveDamageEventArgs args) { }
		protected virtual void OnEntityHealthChanged(EntityHealthChangedEventArgs args) { }
		protected virtual void OnHolderHealthChanged(EntityHealthChangedEventArgs args) { }
		protected virtual void OnEntityManaChanged(EntityManaChangedEventArgs args) { }
		protected virtual void OnHolderManaChanged(EntityManaChangedEventArgs args) { }
		protected virtual void OnEntityStartMovement(EntityStartMovementEventArgs args) { }
		protected virtual void OnHolderStartMovement(EntityStartMovementEventArgs args) { }
		protected virtual void OnEntityEndMovement(EntityEndMovementEventArgs args) { }
		protected virtual void OnHolderEndMovement(EntityEndMovementEventArgs args) { }
		protected virtual void OnEntityDeath(EntityDeathEventArgs args) { }
		protected virtual void OnHolderDeath(EntityDeathEventArgs args) { }

		#endregion

		// Todo: this concept seems to be BAD.
		// ...do something with this?
		public abstract bool Compare(Effect another);
	}
}
