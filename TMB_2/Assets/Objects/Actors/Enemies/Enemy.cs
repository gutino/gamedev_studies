using Godot;
using System;

namespace TowerDefense {
  public abstract class Enemy : Spatial {

	#region Signals
	[Signal]
	delegate void EndReached();
	#endregion

	#region Node Childs
	private Tween Tween {get;set;}
	private AnimationPlayer AnimPlayer {get;set;}
	#endregion

	#region Exported Properties
	[Export]
	public float MovSpeed { get; set; } = 0.5f;
	[Export]
	public float RotDur { get; set; } = 0.1f;
	#endregion

	#region Private Properties
	private Vector3Int CurrGridPosition { get; set; }
	private GridMap CurrGridMap {get;set;}
	#endregion

	public Enemy Init(DefenseGridMap currGridMap) {

	  this.Tween = this.GetNode<Tween>("Tween");
	  this.Tween.Connect( "tween_completed", this, nameof(this.TweenActionCompleted) );

	  this.AnimPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");

	  this.CurrGridMap = currGridMap;
	  this.CurrGridPosition = currGridMap.SpawnPoint;
	  this.Transform = this.Transform.Translated( currGridMap.MapToWorld( CurrGridPosition.x, CurrGridPosition.y, CurrGridPosition.z ) );

	  return this;
	}
  
	public void TweenActionCompleted(Godot.Object _, NodePath key){
	  
	  if (key == ":translation"){
		this.CurrGridPosition = new Vector3Int(this.CurrGridMap.WorldToMap(this.GlobalTransform.origin));
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
	  this.Tween.InterpolateProperty(this, "translation", this.Translation, this.Translation + (-1*this.Transform.basis.x), this.MovSpeed);
	  this.Tween.Start();
	}

	public Exit GetExit(Exit[] possible_Exits){
	  if (possible_Exits.Length > 1)
		return possible_Exits[GD.Randi()%possible_Exits.Length];
	  else
		return possible_Exits[0];
	}

	private MapTile GetTileInfo(Vector3Int tile){
	  var tileIndex = this.CurrGridMap.GetCellItem( tile.x, tile.y, tile.z);
	  var tileName = this.CurrGridMap.MeshLibrary.GetItemName(tileIndex);
	  return TileDict.GetTile(tileName);
	}

  }

}
