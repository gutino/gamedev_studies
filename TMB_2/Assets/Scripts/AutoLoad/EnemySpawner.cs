using Godot;

namespace TowerDefense {
  public class EnemySpawner : Node {

    public DefenseGridMap OwnerMap { get;set;}

    private void SpawnEnemy(){

      Enemy newEnemy = GD.Load<PackedScene>("res://Assets/Objects/Actors/Enemies/Enemy1/Enemy1.tscn").Instance() as Enemy;

      newEnemy.Init(this.OwnerMap);

      this.GetTree().Root.AddChild( newEnemy );

      newEnemy.MoveToNextTile();
    }

    public void MapReadyHandler(){
      this.SpawnEnemy();
    }

    public void SetOwnerMap(DefenseGridMap value){
      this.OwnerMap = value;
      this.GetTree().Root.Connect( "ready", this, nameof(this.MapReadyHandler) );
    }

  }

}