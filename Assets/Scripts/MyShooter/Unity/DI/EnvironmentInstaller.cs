using MyShooter.Core.Entities;
using MyShooter.Core.Environment.Events;
using MyShooter.Unity.Environment;
using Zenject;

namespace MyShooter.Unity.DI
{
	public class EnvironmentInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<Updater>().FromNewComponentOnNewGameObject().AsSingle();
			Container.Bind<EventBus>().AsSingle();
			Container.Bind<EntityRegistry>().AsSingle();
		}
	}
}
