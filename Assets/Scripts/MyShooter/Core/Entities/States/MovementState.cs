using MyShooter.Core.Entities.States.Modified;
using System;

namespace MyShooter.Core.Entities.States
{
	[Serializable]
	public class MovementState
	{
		public ModifiableValue Speed;
		public bool IsStopped;
	}
}
