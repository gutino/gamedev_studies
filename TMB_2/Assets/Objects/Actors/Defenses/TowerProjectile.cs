using Godot;
using System;

namespace TowerDefense {
	public abstract class TowerProjectile : Area {
		[Export]
		public float Speed { get; set; } = 5.0f;
		[Export]
		public int Damage { get; set; } = 1;
		public WeakRef Target { get; set; }

		public override void _Process(float delta) {

			if (this.Target.GetRef() is Enemy targetRef) {
				this.LookAt(targetRef.GlobalTransform.origin, this.Transform.basis.y);
				this.Translate(Vector3.Forward * Speed * delta);
			} else {
				this.SelfDestruct();
			}

		}

		public TowerProjectile Init(WeakRef target) {
			this.Connect("body_entered", this, nameof(this.On_Enemy_Hit));
			this.Target = target;
			return this;
		}

		protected virtual void SelfDestruct() {
			this.Monitoring = false;
			this.QueueFree();
		}

		public virtual void On_Enemy_Hit(Node body) {

			if (
				this.Target.GetRef() is Enemy targetRef &&
				body.Owner is Enemy enemyBody &&
				enemyBody == targetRef
			) {
				targetRef.TakeDmg(this.Damage);
				this.SelfDestruct();
			}
		}
	}
}
