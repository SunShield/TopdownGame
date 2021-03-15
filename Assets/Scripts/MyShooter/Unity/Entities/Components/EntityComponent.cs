namespace MyShooter.Unity.Entities.Components
{
	public class EntityComponent : EventReceivingBehaviour
	{
		public BattleEntity HolderEntity { get; private set; }
		public bool IsAwaken { get; private set; }

		public void AwakeComponentManually(BattleEntity holder)
		{
			HolderEntity = holder;
			IsAwaken = true;
			AwakeInternal();
		}

		/// <summary>
		/// Use this method to put there some stuff needed to be executed AFTER entity itself and all it's components is found and set-up.
		/// </summary>
		protected virtual void AwakeInternal() { }

		// we don't want to do automatical initialization for EntityComponents.
		// we have AwakeInternal for this
		protected sealed override void InitializeAutomatically()
		{
			base.InitializeAutomatically();
			InitializeComponent();
		}

		public sealed override void UpdateManually()
		{
			if (!IsAwaken || HolderEntity == null) return;
			UpdateComponentInternal();
		}

		public sealed override void FixedUpdateManually()
		{
			if (!IsAwaken || HolderEntity == null) return;
			FixedUpdateComponentInternal();
		}

		protected virtual void InitializeComponent() { }
		protected virtual void UpdateComponentInternal() { }
		protected virtual void FixedUpdateComponentInternal() { }
	}
}
