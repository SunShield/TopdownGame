using MyShooter.Unity.Entities.Concrete.Player;
using MyShooter.Unity.Entities.Effects;
using MyShooter.Unity.Entities.Skills;
using UnityEngine;

namespace MyShooter.Unity.Entities.Items
{
	public class Item : ManuallyUpdatableBehaviour
	{
		[SerializeField] private GameObject FieldGraphics; // used while item is on field.
		[SerializeField] private GameObject PlayerGraphics; // used while item is on player.
		[SerializeField] private string _name;

		private Collider2D _collider;
		private bool _isApplied;

		public string Name => _name;
		public Effect[] Effects { get; private set; }
		public Skill[] Skills { get; private set; }

		protected override void InitializeAutomatically()
		{
			_collider = GetComponentInChildren<Collider2D>();
			Effects = GetComponentsInChildren<Effect>();
			Skills = GetComponentsInChildren<Skill>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var go = collision.gameObject;
			if(go.layer == LayerMask.NameToLayer(LayerNameUtils.Characters))
			{
				var entity = go.GetComponentInParent<PlayerEntity>();
				ApplyToPlayer(entity);
			}
		}

		private void ApplyToPlayer(PlayerEntity player)
		{
			ChangeGraphics();
			DisableCollider();
			player.AddItem(this);
			_isApplied = true;
		}

		private void DisableCollider() => _collider.enabled = false;

		private void ChangeGraphics()
		{
			FieldGraphics.SetActive(false);
			PlayerGraphics.SetActive(true);
		}
	}
}
