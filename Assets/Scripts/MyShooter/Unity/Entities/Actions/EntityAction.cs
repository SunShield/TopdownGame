using System;

namespace MyShooter.Unity.Entities.Actions
{
	public class EntityAction : ManuallyUpdatableBehaviour
	{
		private Lazy<BattleEntity> _lazyHolder;
		protected BattleEntity HolderEntity => _lazyHolder.Value;

		public virtual bool IsExecuting { get; protected set; }

		protected override void InitializeAutomatically()
		{
			SetHolder();
		}

		private void SetHolder()
		{
			_lazyHolder = new Lazy<BattleEntity>(() => GetComponentInParent<BattleEntity>());
		}

		public void TryStartExecution()
		{
			if (IsExecuting) return;

			IsExecuting = true;
			StartExecutionInternal();
		}

		public sealed override void UpdateManually()
		{
			UpdateEntityAction();

			if (IsExecuting && CheckEndExecution())
			{
				IsExecuting = false;
				EndExecutionInternal();
			}
		}

		protected virtual bool CheckEndExecution() => true;
		protected virtual void StartExecutionInternal() { }
		protected virtual void UpdateEntityAction() { }
		protected virtual void EndExecutionInternal() { }
	}
}
