using Godot;
using System;

namespace TowerDefense{
	public abstract class Enemy : Spatial{

		#region Node Childs
		private Tween Tween { get { return this.GetNode<Tween>("Tween"); } }
		private AnimationPlayer AnimPlayer { get { return this.GetNode<AnimationPlayer>("AnimationPlayer"); } }
		#endregion

		#region Exported Properties
		[Export]
		public float MovDur { get; set; } = 0.2f;
		[Export]
		public float RotDur { get; set; } = 0.01f;
		[Export]
		public float Height { get; set; } = 0.45f;
		[Export]
		public int HitPoints { get; set; } = 30;
		#endregion

		#region Private Properties
		private Vector3Int CurrGridPosition { get; set; }
		private GridMap CurrGridMap { get; set; }
		[Signal]
		public delegate void EnemyDied(Enemy enemy);
		#endregion

		public Enemy Init(DefenseGridMap currGridMap){
			this.Tween.Connect("tween_completed", this, nameof(this.TweenActionCompleted));

			this.CurrGridMap = currGridMap;
			this.CurrGridPosition = currGridMap.SpawnPoint;
			this.Transform = this.Transform.Translated(
				currGridMap.MapToWorld(CurrGridPosition.X, CurrGridPosition.Y, CurrGridPosition.Z) +
				new Vector3(0f, Height, 0f)
			);

			return this;
		}

		public void TweenActionCompleted(Godot.Object _, NodePath key){

			if (key == ":translation"){
				this.CurrGridPosition = new Vector3Int(
					this.CurrGridMap.WorldToMap(
						this.GlobalTransform.origin - new Vector3(0f, Height, 0f)
					)
				);
				this.MoveToNextTile();
			}
			else if (key == ":rotation_degrees:y"){
				this.MoveForward();
			}
		}

		public void MoveToNextTile(){
			var currTileInfo = this.GetTileInfo(this.CurrGridPosition);

			if (currTileInfo.Type == TileType.GOAL){
				this.AnimPlayer.Play("EndReached");
				this.EmitSignal("EnemyDied", WeakRef(this));
				return;
			}

			var exit = this.GetExit(currTileInfo.Exits);
			if (exit != Exit.FORWARD){
				this.Tween.InterpolateProperty(
					this,
					"rotation_degrees:y",
					this.RotationDegrees.y,
					this.RotationDegrees.y + (exit == Exit.LEFT ? 90f : -90f),
					this.RotDur
				);
				this.Tween.Start();
			}
			else{
				this.MoveForward();
			}
		}

		private void MoveForward(){
			this.Tween.InterpolateProperty(
				this,
				"translation",
				this.Translation,
				this.Translation - this.Transform.basis.x,
				this.MovDur
			);
			this.Tween.Start();
		}

		public Exit GetExit(Exit[] possible_Exits){
			if (possible_Exits.Length > 1)
				return possible_Exits[GD.Randi() % possible_Exits.Length];
			else
				return possible_Exits[0];
		}

		private MapTile GetTileInfo(Vector3Int tile){
			var tileIndex = this.CurrGridMap.GetCellItem(tile.X, tile.Y, tile.Z);
			var tileName = this.CurrGridMap.MeshLibrary.GetItemName(tileIndex);
			return TileDict.GetTile(tileName);
		}

		public void TakeDmg(int damage){
			Console.WriteLine($"Damaged! Current HP: {this.HitPoints}. Damage Taken: {damage}");
			HitPoints -= damage;
			if (HitPoints <= 0){
				this.Die();
			}
		}

		private void Die(){
			EmitSignal(nameof(EnemyDied), WeakRef(this));
			this.QueueFree();
		}
		
	}

}
