using MyShooter.Unity.Service.Extensions;
using UnityEngine;

namespace MyShooter.Unity.Entities.Components.Decisioning.Rotation
{
	public class MouseLookRotationDecisionMaker : RotationDecisionMaker
	{
		private Camera _camera;

		protected override void AwakeInternal()
		{
			base.AwakeInternal();
			_camera = Camera.main;
		}

		protected override void UpdateComponentInternal()
		{
			Vector3 mousePositionInWorldCoords = Input.mousePosition - _camera.WorldToScreenPoint(RotationOrigin.position);
			RotationOrigin.LookAt2D(mousePositionInWorldCoords);
		}
	}
}
