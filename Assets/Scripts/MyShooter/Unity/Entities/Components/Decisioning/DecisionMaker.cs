using MyShooter.Core.Entities;
using Zenject;

namespace MyShooter.Unity.Entities.Components.Decisioning
{
	public class DecisionMaker : EntityComponent
	{
		[Inject] protected EntityRegistry Registry { get; }
	}
}
