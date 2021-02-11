using Godot;

namespace TowerDefense{
    public class EnemySpawner : Node{

        public DefenseGridMap OwnerMap { get; set; }
        private Timer SpawnDelay { get; set; } = new Timer();

        public void _on_Timer_Timeout(){
            this.SpawnEnemy();
        }

        private void SpawnEnemy(){

            Enemy newEnemy = GD.Load<PackedScene>("res://Assets/Objects/Actors/Enemies/Enemy1/Enemy1.tscn").Instance() as Enemy;
            newEnemy.Init(this.OwnerMap);
            this.GetTree().Root.AddChild(newEnemy);
            newEnemy.MoveToNextTile();
        }

        public void MapReadyHandler(){
            this.GetTree().Root.AddChild(SpawnDelay);
            SpawnDelay.OneShot = false;
            SpawnDelay.Connect("timeout", this, nameof(_on_Timer_Timeout));
            this.SpawnEnemy();
            this.SpawnDelay.Start(1.0f);
        }

        public void SetOwnerMap(DefenseGridMap value){
            this.OwnerMap = value;
            this.GetTree().Root.Connect("ready", this, nameof(this.MapReadyHandler));
        }
    }
}