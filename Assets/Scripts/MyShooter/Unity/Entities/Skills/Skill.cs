using MyShooter.Core.Entities;
using MyShooter.Core.Entities.States;
using MyShooter.Unity.Entities.Actions;
using MyShooter.Unity.Entities.Concrete.Player;
using UnityEngine;
using Zenject;

namespace MyShooter.Unity.Entities.Skills
{
	public class Skill : ManuallyUpdatableBehaviour
	{
		[SerializeField] private Sprite _icon;
		[SerializeField] private SkillState _state;
		[SerializeField] private EntityAction _skillAction;

		protected PlayerEntity Holder;

		[Inject] protected EntityRegistry Registry { get; private set; }

		public float CooldownTimer { get; protected set; }
		public bool ExecutionStarted { get; private set; }
		public Sprite Icon => _icon;
		public SkillState State => _state;
		public SkillType Type => State.Type;
		public int ManaCost => State.ManaCost;
		public bool IsExecuting => _skillAction.IsExecuting;
		public bool IsCooldowning => CooldownTimer > 0f;
		public bool IsExecutable => !IsExecuting && !IsCooldowning;

		protected override void InitializeAutomatically()
		{
			Holder = GetComponentInParent<PlayerEntity>();
			SetPreparationTime();
		}

		private void SetPreparationTime()
		{
			CooldownTimer = _state.PreparationTime;
		}

		// playing consists of preparation and execution
		public void TryStartExecution()
		{
			if (IsExecuting || IsCooldowning) return;

			ExecutionStarted = true;
			_skillAction.TryStartExecution();
		}

		public override void UpdateManually()
		{
			if(ExecutionStarted && !IsCooldowning && !IsExecuting) 
				StartCooldowning();

			if (IsCooldowning) 
				AdvanceCooldownTimer();
		}

		private void AdvanceCooldownTimer()
		{
			CooldownTimer = Mathf.Clamp(CooldownTimer - Time.deltaTime, 0f, CooldownTimer);
		}

		private void StartCooldowning()
		{
			ExecutionStarted = false;
			CooldownTimer = State.CooldownTime;
		}
	}
}
