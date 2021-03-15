using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Movement
{
	public class MousePositionFlightDecisionMaker : OneDirectionFlightDecisionMaker
	{
		protected override Vector2 DetermineDirection() 
			=> Camera.main.ScreenToWorldPoint(Input.mousePosition) - Tran.position;
	}
}
