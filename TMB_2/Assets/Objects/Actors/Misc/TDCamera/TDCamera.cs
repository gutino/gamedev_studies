using Godot;

namespace TowerDefense{
	public class TDCamera : Spatial{

		#region Exports
		[Export]
		public float MinDistance {get;set;} = 1f;
		[Export]
		public float MaxDistance {get;set;} = 4.0f;
		[Export]
		public float CamMovSpeed { get; set; } = 5.0f;
		[Export]
		public float CamZoomSpeed { get; set; } = 7.0f;
		[Export]
		public float MouseSensitivity { get; set; } = 0.005f;
		#endregion
		
		#region Privates
	private bool IsRotating;
		private Camera ChildCamera { get{ return this.GetNode<Camera>("Camera"); } }

		private Vector3 CamInitLoc {get;set;}
	#endregion

	public override void _Ready(){
	  this.ChildCamera.LookAt(this.GlobalTransform.origin, Vector3.Up);
			this.CamInitLoc = this.ChildCamera.Transform.origin;
	}
	public override void _Input(InputEvent @event){
			if (@event is InputEventMouseMotion && this.IsRotating){
				this.RotateY((MouseSensitivity * (@event as InputEventMouseMotion)?.Relative.x).Value);
			}
		}
		public override void _Process(float delta){

			#region Pivot Movement
			if (Input.IsActionPressed("ui_left"))
				this.TranslateObjectLocal(Vector3.Left * CamMovSpeed * delta);
			if (Input.IsActionPressed("ui_right"))
				this.TranslateObjectLocal(Vector3.Right * CamMovSpeed * delta);
			if (Input.IsActionPressed("ui_up"))
				this.TranslateObjectLocal(Vector3.Forward * CamMovSpeed * delta);
			if (Input.IsActionPressed("ui_down"))
				this.TranslateObjectLocal(Vector3.Back * CamMovSpeed * delta);
			#endregion

			#region Camera Zoom
			var currCamTransform = this.ChildCamera.Transform;
			var relativeY = this.ChildCamera.GlobalTransform.origin.y - this.GlobalTransform.origin.y;

			//GD.Print(relativeY);


			if (Input.IsActionJustReleased("ui_scroll_up") && relativeY > (this.MinDistance + this.GlobalTransform.origin.y))
					currCamTransform.origin -= currCamTransform.basis.z * this.CamZoomSpeed * delta;
			else if (Input.IsActionJustReleased("ui_scroll_down") && relativeY < this.MaxDistance + this.GlobalTransform.origin.y)
				currCamTransform.origin += currCamTransform.basis.z * this.CamZoomSpeed * delta;
			
			this.ChildCamera.Transform = currCamTransform;
			#endregion
			
			if (Input.IsActionPressed("ui_right_click"))
				IsRotating = true;

			if (Input.IsActionJustReleased("ui_right_click"))
				IsRotating = false;
		}
	}
}
