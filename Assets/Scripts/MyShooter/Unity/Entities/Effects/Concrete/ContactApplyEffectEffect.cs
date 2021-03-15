using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public class ContactApplyEffectEffect : ContactActionEffect
	{
		[SerializeField] private Effect EffectToApply;

		public override bool Compare(Effect another) => true;

		protected override void DoOnHolderCollide(EntityCollideEventArgs args)
		{
			var entity = args.CollidedEntity;
			var newEffect = Instantiate(EffectToApply);
			entity.AddEffect(newEffect);
		}
	}
}
