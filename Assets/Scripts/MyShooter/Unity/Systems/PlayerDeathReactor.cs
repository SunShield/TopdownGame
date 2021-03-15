using MyShooter.Core.Constants;
using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using UnityEngine.SceneManagement;

namespace MyShooter.Unity.Systems
{
	public class PlayerDeathReactor : EventReceivingBehaviour
	{
		protected override void InitializeAutomatically()
		{
			base.InitializeAutomatically();
			EventBus.GetEvent<EntityDeathEvent>().SubscribeForGlobal(OnEntityDeath);
		}

		private void OnEntityDeath(EntityDeathEventArgs args)
		{
			if (args.Type != Core.Entities.States.EntityType.Player) return;

			OnPlayerDeath();
		}

		private void OnPlayerDeath()
		{
			SceneManager.LoadSceneAsync(SceneNameConstants.MainMenuSceneName);
		}

		protected override void OnDestroyAutomatically()
		{
			EventBus.GetEvent<EntityDeathEvent>().UnsubscribeFromGlobal(OnEntityDeath);
		}
	}
}
