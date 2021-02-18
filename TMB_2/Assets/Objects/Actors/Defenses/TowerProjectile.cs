using Godot;

namespace TowerDefense{
	public class TowerProjectile : Area{
		public float Speed { get; } = 5.0f;
		public Enemy Target { get; set; }
		public override void _Process(float delta){
			this.LookAt(Target.GlobalTransform.origin, Vector3.Up);
			this.Translate(Vector3.Forward * Speed * delta);
		}
		
		public TowerProjectile Init(Enemy target){
			this.Target = target;
			this.Connect("body_entered", this, nameof(this.On_Enemy_Hit));
			return this;
		}

		private void On_Enemy_Hit(Object body){
			if ((body as Node).Owner == this.Target){
				this.QueueFree();
			} 
		}
	}
} 
