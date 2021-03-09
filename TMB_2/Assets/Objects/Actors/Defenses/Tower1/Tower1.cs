using Godot;
using System;

namespace TowerDefense{
	public class Tower1 : Tower{
		private PackedScene ProjectileScene { get; } = GD.Load<PackedScene>(
			"res://Assets/Objects/Actors/Defenses/Tower1/Firebolt.tscn"
		);

		public override void FireProjectile(WeakRef target){
			var projectile = ProjectileScene.Instance() as TowerProjectile;
			projectile.Init( target );
			this.ProjectileSpawner.AddChild(projectile);
		}
	}
}
