using MyShooter.Core.Environment.Events;
using Zenject;

namespace MyShooter.Unity
{
	public class EventReceivingBehaviour : ManuallyUpdatableBehaviour
	{
		[Inject] protected EventBus EventBus;
		protected int GoId { get; private set; }

		protected override void InitializeAutomatically()
		{
			GoId = transform.root.gameObject.GetInstanceID();
		}
	}
}
