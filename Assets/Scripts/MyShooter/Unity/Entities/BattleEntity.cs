using Zenject;
using UnityEngine;
using MyShooter.Core.Entities;
using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Structure;
using MyShooter.Unity.Entities.Effects;
using MyShooter.Unity.Entities.Components.Breaking;
using MyShooter.Unity.Entities.Components.Movement;
using MyShooter.Unity.Entities.Components.Physics;
using MyShooter.Unity.Entities.Components.Collision;
using MyShooter.Unity.Entities.Components.Decisioning.Movement;
using MyShooter.Unity.Entities.Components.Decisioning.Rotation;
using MyShooter.Unity.Entities.Components.Decisioning.Actions;

namespace MyShooter.Unity.Entities
{
	/// <summary>
	/// Class for all on-field Unity objects: character, enemies, bullets, interactibles, items etc.
	/// </summary>
	// TODO: ADD RequireComponents
	public class BattleEntity : ManuallyUpdatableBehaviour
	{
		public virtual EntityType Type { get; } = EntityType.Obstacle;

		[Inject] protected EntityRegistry Registry { get; }

		public EntityStructure Structure { get; private set; }

		protected ForceProcessor Physics { get; private set; }
		protected CollisionDetector CollisionDetector { get; private set; }
		protected Mover Mover { get; private set; }
		protected Breaker Breaker { get; private set; }
		public MoveDecisionMaker MoveDecisionMaker { get; private set; }
		protected RotationDecisionMaker RotationDecisonMaker { get; private set; }
		protected ActionUseDecisionMaker ActionUseDecisonMaker { get; private set; }
		public EffectStorage EffectStorage { get; private set; }

		public StatsState StatsState => Breaker?.StatsState;
		public MovementState MovementState => Mover?.MovementState;
		public Vector2 MoveDirection => Mover != null ? Mover.Direction : Vector2.zero;
		public bool IsDamagable => Breaker != null;

		#region Inner Class Methods

		protected override void InitializeAutomatically()
		{
			InitStructure();
			FindComponents();
		}

		private void Awake()
		{
			InitializeFields();
			AwakeComponents();
		}

		private void InitStructure()
		{
			Structure = new EntityStructure(this);
		}

		private void FindComponents()
		{
			Physics = GetComponent<ForceProcessor>();
			Breaker = GetComponent<Breaker>();
			Mover = GetComponentInChildren<Mover>();
			CollisionDetector = GetComponentInChildren<CollisionDetector>();
			MoveDecisionMaker = GetComponentInChildren<MoveDecisionMaker>();
			RotationDecisonMaker = GetComponentInChildren<RotationDecisionMaker>();
			ActionUseDecisonMaker = GetComponentInChildren<ActionUseDecisionMaker>();
			FindComponentsInternal();
		}

		private void InitializeFields()
		{
			EffectStorage = new EffectStorage();
			InitializeFieldsInternal();
		}

		protected virtual void InitializeFieldsInternal() { }

		protected virtual void FindComponentsInternal() { }

		private void AwakeComponents()
		{
			Breaker?.AwakeComponentManually(this);
			Mover?.AwakeComponentManually(this);
			MoveDecisionMaker?.AwakeComponentManually(this);
			RotationDecisonMaker?.AwakeComponentManually(this);
			ActionUseDecisonMaker?.AwakeComponentManually(this);
			AwakeComponentsInternal();
		}

		protected virtual void AwakeComponentsInternal() { }

		#endregion

		#region Control Methods

		public void AddForce(string forceName, Vector2 forceVector, float forceDuration)
		{
			Physics.AddForce(forceName, forceVector, forceDuration);
		}

		public void TryReceiveDamage(DamageInstance damage)
		{
			if (!IsDamagable) return;

			Breaker.RecieveDamage(damage);
		}

		public void AddEffect(Effect effect)
		{
			ApplyInHierarchy(effect.Tran, Structure.Effects);
			EffectStorage.AddEffect(effect);
			effect.Apply(this);
		}

		public void RemoveEffect(Effect effect)
		{
			effect.Remove();
			EffectStorage.RemoveEffect(effect);
		}

		protected void ApplyInHierarchy(Transform tran, GameObject structureElement)
		{
			tran.parent = structureElement.transform;
			tran.localPosition = Vector3.zero;
			tran.localRotation = Quaternion.identity;
		}

		#endregion
	}
}
