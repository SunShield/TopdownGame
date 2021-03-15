using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Collision
{
	public class CollisionDetector : EntityComponent
	{
		private EntityCollideEventArgs _args = new EntityCollideEventArgs();

		private void OnCollisionEnter2D(Collision2D collision)
		{
			var entity = GetEntity(collision.gameObject);
			InvokeStartCollisionEventsIfNeeded(entity);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var entity = GetEntity(collision.gameObject);
			InvokeStartCollisionEventsIfNeeded(entity);
		}

		private void InvokeStartCollisionEventsIfNeeded(BattleEntity entity)
		{
			if (entity == null) return;
			
			SetArgs(entity);
			EventBus.GetEvent<EntityStartCollideEvent>().InvokeForId(GoId, _args);
			EventBus.GetEvent<EntityStartCollideEvent>().InvokeForGlobal(_args);
		}

		private void SetArgs(BattleEntity entity)
		{
			_args.CollidedEntity = entity;
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			var entity = GetEntity(collision.gameObject);
			InvokeEndCollisionEventsIfNeeded(entity);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			var entity = GetEntity(collision.gameObject);
			InvokeEndCollisionEventsIfNeeded(entity);
		}

		private void InvokeEndCollisionEventsIfNeeded(BattleEntity entity)
		{
			if (entity == null) return;

			SetArgs(entity);
			EventBus.GetEvent<EntityEndCollideEvent>().InvokeForId(GoId, _args);
			EventBus.GetEvent<EntityEndCollideEvent>().InvokeForGlobal(_args);
		}

		private BattleEntity GetEntity(GameObject collidedObject)
		{
			return CheckEntityByLayer(collidedObject)
				? SeekEntity(collidedObject)
				: null;
		}

		private static bool CheckEntityByLayer(GameObject collidedObject) => (LayerNameUtils.EntityMask.value & 1 << collidedObject.layer) > 0;

		private BattleEntity SeekEntity(GameObject go)
		{
			var entity = go.GetComponent<BattleEntity>();
			if (entity == null)
				entity = go.GetComponentInParent<BattleEntity>();
			return entity;
		}
	}
}
