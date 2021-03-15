using MyShooter.Core.Entities;
using MyShooter.Core.Entities.States;
using MyShooter.Core.Environment.Events.Entity.EffectReactional;
using MyShooter.Unity.Entities.Concrete.Player;
using UnityEngine;
using Zenject;

namespace MyShooter.Unity.LevelLayout
{
	public class EscapeZone : EventReceivingBehaviour
	{
		[Inject] private EntityRegistry _registry;
		[SerializeField] private RoomSwitcher RoomSwitcher;
		[SerializeField] private GameObject[] Hiders;
		private bool _isActive;

		private bool RoomHasAliveEnemy => _registry.Enemies.Count != 0;

		private void Start()
		{
			if (!RoomHasAliveEnemy && !_isActive) // a bit hacky?..
				Activate();
		}

		public override void UpdateManually()
		{
			if (RoomHasAliveEnemy || _isActive) return;

			Activate();
		}

		private void Activate()
		{
			AnimateActivation();
			_isActive = true;
		}

		private void AnimateActivation()
		{
			foreach (var hider in Hiders)
				hider.SetActive(false);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!_isActive) return;
			var go = collision.gameObject;
			if (!LayerNameUtils.IsEntity(go)) return;
			var player = collision.gameObject.GetComponentInParent<PlayerEntity>();
			if (player == null) return;

			_isActive = false;
			RoomSwitcher.SwitchRoom();
		}
	}
}
