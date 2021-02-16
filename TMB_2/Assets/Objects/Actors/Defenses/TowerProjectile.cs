using Godot;

namespace TowerDefense{
    public class TowerProjectile : StaticBody{
        public float Speed { get; } = 5.0f;
        public Enemy Target { get; set; }
        public override void _Process(float delta){
            this.LookAt(Target.GlobalTransform.origin, Vector3.Up);
            this.Translate(Vector3.Forward * Speed * delta);
        }
    }
} 