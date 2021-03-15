using MyShooter.Core.Entities;
using MyShooter.Unity.Entities.Concrete.Player;
using Zenject;

namespace MyShooter.Unity.UI.Player
{
	public class PlayerUiElement : ManuallyUpdatableBehaviour
	{
		[Inject] private EntityRegistry _registry;
		protected PlayerEntity Player { get; private set; }
		protected bool PlayerFound => Player != null;
		private bool _playerWasFound;

		public sealed override void FixedUpdateManually()
		{
			if (!PlayerFound)
			{
				FindPlayer();
				FixedUpdateElementNoPlayer();
				return;
			}

			FixedUpdateElement();
		}

		protected virtual void FixedUpdateElement() { }
		protected virtual void FixedUpdateElementNoPlayer() { }

		private void FindPlayer()
		{ 
			Player = _registry.Player;
			if (Player != null)
				_playerWasFound = true;
		}
	}
}
