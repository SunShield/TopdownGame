
using MyShooter.Unity.Environment;
using UnityEngine;
using Zenject;

/// <summary>
/// This class provides a possibility to get manually updated from Updater class.
/// (the main purpose of putting this into the MyShooter.Unity is providing a simple way of inheriting from it, without wasting time adding usings).
/// </summary>
namespace MyShooter.Unity
{
	public class ManuallyUpdatableBehaviour : MonoBehaviour
	{
		public Transform Tran { get; private set; }
		protected Vector2 Position => (Vector2)Tran.position;

		[Inject]
		private void Initialize(Updater updater)
		{
			updater.RegisterUpdatebleBehaviour(this);
			InitializeComponents();
			InitializeAutomatically();
		}

		private void InitializeComponents()
		{
			Tran = GetComponent<Transform>();
		}

		protected void OnDestroy()
		{
			OnDestroyAutomatically();
		}

		/// <summary>
		/// Put all per-frame actions here
		/// </summary>
		public virtual void UpdateManually() { }

		/// <summary>
		/// Put all physics calculations here.
		/// </summary>
		public virtual void FixedUpdateManually() { }

		/// <summary>
		/// Use this instead of Start() method. This method is called right after behaviour is registered.
		/// </summary>
		protected virtual void InitializeAutomatically() { }

		/// <summary>
		/// Ise this to do on-destroy actions.
		/// </summary>
		protected virtual void OnDestroyAutomatically() { }
	}
}
