using MyShooter.Core.Entities.States;
using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Unity.Entities.Effects.Concrete
{
	public abstract class ContactActionEffect : Effect
	{
		[SerializeField] protected List<EntityType> ContactableTypes = new List<EntityType>();
		protected HashSet<BattleEntity> CollidedEntities = new HashSet<BattleEntity>();

		protected sealed override void OnHolderCollide(EntityCollideEventArgs args)
		{
			var entity = args.CollidedEntity;
			if (!ContactableTypes.Contains(entity.Type)) return;
			
			CollidedEntities.Add(entity);
			DoOnHolderCollide(args);
			CollidedEntities.Remove(null);
		}

		protected sealed override void OnHolderUncollide(EntityCollideEventArgs args)
		{
			if (!CollidedEntities.Contains(args.CollidedEntity)) return;

			DoOnHolderUncollide(args);
			CollidedEntities.Remove(args.CollidedEntity);
		}

		protected virtual void DoOnHolderUncollide(EntityCollideEventArgs args) { }
		protected virtual void DoOnHolderCollide(EntityCollideEventArgs args) { }
	}
}
