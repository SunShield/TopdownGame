using UnityEngine;

namespace MyShooter.Core.Environment.Events.Entity.Functional
{
	public class EntityMoveDecisionChangedEventArgs : GameEventArgs
	{
		public Vector2 Direction { get; set; }
	}
}
