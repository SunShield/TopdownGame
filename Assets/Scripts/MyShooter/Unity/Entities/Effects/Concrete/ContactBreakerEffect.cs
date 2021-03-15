using MyShooter.Core.Environment.Events.Entity.EffectReactional;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	// TODO: find better, less ambigous name
	public class ContactBreakerEffect : ContactActionEffect
	{
		public override bool Compare(Effects.Effect another) => true;

		protected override void DoOnHolderCollide(EntityCollideEventArgs args)
		{
			Destroy(Tran.root.gameObject);
		}
	}
}
