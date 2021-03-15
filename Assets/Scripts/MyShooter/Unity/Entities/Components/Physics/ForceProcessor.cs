using System.Collections.Generic;
using UnityEngine;

// Actually, I believe there's a better approach... But I can't find one.
namespace MyShooter.Unity.Entities.Components.Physics
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ForceProcessor : ManuallyUpdatableBehaviour
	{
		private Rigidbody2D _rigidbody;
		private Dictionary<string, ForceOnCreature> _activeForces = new Dictionary<string, ForceOnCreature>();
		private List<string> _inactiveForceNames = new List<string>();
		private Vector2 _resultingVector = Vector2.zero;

		protected override void InitializeAutomatically()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		public void AddForce(string name, Vector2 velocity, float duration = 0.02f)
		{
			if (CheckForcePresent(name))
			{
				var force = CreateForce(name, velocity, duration);
				StoreForce(name, force);
				ApplyForce(force);
			}
			else
			{
				var force = _activeForces[name];
				if (force.ForceVector != velocity)
				{
					RemoveForce(force);
					OverrideForceVector(velocity, force);
					ApplyForce(force);
				}
				RestoreForceDuration(duration, force);
			}
		}

		private bool CheckForcePresent(string name)
		{
			return !_activeForces.ContainsKey(name);
		}

		private ForceOnCreature CreateForce(string name, Vector2 velocity, float duration)
		{
			return new ForceOnCreature(new Force(name, velocity, duration));
		}

		private void StoreForce(string name, ForceOnCreature force)
		{
			_activeForces.Add(name, force);
		}

		private void ApplyForce(ForceOnCreature force)
		{
			_resultingVector += force.ForceVector;
		}

		private void RemoveForce(ForceOnCreature force)
		{
			_resultingVector -= force.ForceVector;
		}

		private void OverrideForceVector(Vector2 velocity, ForceOnCreature force)
		{
			force.ForceVector = velocity;
		}

		private void RestoreForceDuration(float duration, ForceOnCreature force)
		{
			force.Duration = duration;
		}

		public override void FixedUpdateManually()
		{
			RemoveInertia();
			TickForces();
			RemoveExpiredForces();
		}

		private void RemoveInertia()
		{
			_rigidbody.velocity = Vector2.zero;
			_rigidbody.AddForce(_resultingVector);
		}

		private void TickForces()
		{
			foreach(var forceName in _activeForces.Keys)
			{
				var force = _activeForces[forceName];
				if(!force.IsExpired)
				{
					force.Duration -= Time.fixedDeltaTime;
				}
				else
				{
					_inactiveForceNames.Add(forceName);
				}
			}
		}

		private void RemoveExpiredForces()
		{
			for(int i = _inactiveForceNames.Count - 1; i >=0; i--)
			{
				var forceName = _inactiveForceNames[i];
				_resultingVector -= _activeForces[forceName].ForceVector;
				_activeForces.Remove(forceName);
				_inactiveForceNames.RemoveAt(i);
			}
		}
	}
}
