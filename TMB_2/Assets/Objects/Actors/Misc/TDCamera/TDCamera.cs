using Godot;
using System;

namespace TowerDefense{
	public class TDCamera : Spatial{
		[Export]
		public float CamSpeed { get; set; } = 5.0f;
		[Export]
		public float MouseSensitivity { get; set; } = 0.005f;

		private bool IsRotating { get; set; } = false;

		public override void _Input(InputEvent @event){
			if (@event is InputEventMouseMotion){
				this.Transform = this.Transform.Rotated(Vector3.Up, MouseSensitivity * (@event as InputEventMouseMotion).Relative.x);
			}
		}
		public override void _Process(float delta){
			if (Input.IsActionPressed("ui_left")){
				this.Transform = this.Transform.Translated(this.Transform.basis.x * -CamSpeed * delta);
			}
			if (Input.IsActionPressed("ui_right")){
				this.Transform = this.Transform.Translated(this.Transform.basis.x * CamSpeed * delta);
			}
			if (Input.IsActionPressed("ui_up")){
				this.Transform = this.Transform.Translated(this.Transform.basis.z * -CamSpeed * delta);
			}
			if (Input.IsActionPressed("ui_down")){
				this.Transform = this.Transform.Translated(this.Transform.basis.z * CamSpeed * delta);
			}

			if (Input.IsActionPressed("ui_right_click")){
				IsRotating = true;
			}
			if (Input.IsActionJustReleased("ui_right_click")){
				IsRotating = false;
			}
		}
	}
}
