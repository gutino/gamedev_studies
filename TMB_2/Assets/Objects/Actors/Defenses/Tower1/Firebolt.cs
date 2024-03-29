using Godot;
using System;

namespace TowerDefense{
	public class Firebolt : TowerProjectile{

		private Particles FireParticles { get { return this.GetNode<Particles>("Particles"); } }
		private Timer FreeTimer { get { return this.GetNode<Timer>("FreeTimer"); } }

		public override void _Ready(){
			Console.WriteLine("Ready");
			this.FreeTimer.Connect("timeout", this, nameof(this.FreeTimerTimeout));
			base._Ready();
		}

		protected override void SelfDestruct(){
			//this.FireParticles.Emitting = false;
			Console.WriteLine("Firebolt");
			this.FreeTimer.Start(this.FireParticles.Lifetime);
		}

		private void FreeTimerTimeout(){
			Console.WriteLine("Timeout");
			base.SelfDestruct();
		}

	}
}
