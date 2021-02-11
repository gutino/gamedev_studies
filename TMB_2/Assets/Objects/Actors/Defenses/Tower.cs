using Godot;
using System;
using System.Collections.Generic;

namespace TowerDefense{
    public class Tower : Spatial{
        private List<Enemy> CloseEnemies {get;} = new List<Enemy>();
        private LineDrawer3d LineDrawer = new LineDrawer3d();
        private Area RangeArea {get{return this.GetNode<Area>("RangeArea");}}

        public override void _Ready(){
            this.AddChild(this.LineDrawer);
            RangeArea.Connect("body_entered", this, nameof(_on_Area_Entered));
            RangeArea.Connect("body_exited", this, nameof(_on_Area_Exited));
        }
        
        public void _on_Area_Entered(Node body){
            if (body.IsInGroup("Enemies")){
                CloseEnemies.Add(body.GetOwner<Enemy>());
            }
        }
        public void _on_Area_Exited(Node body){
            if (body.IsInGroup("Enemies")){
                CloseEnemies.Remove(body.GetOwner<Enemy>());
            }
        }
        public override void _Process(float delta){
            foreach (var enemy in CloseEnemies){
                LineDrawer.addLine(this.Transform.origin, enemy.Transform.origin);
            }
            GD.Print(CloseEnemies.Count);
        }
        
    }

    public class LineDrawer3d : ImmediateGeometry{
        List<Vector3> points = new List<Vector3>();

        public void addLine(Vector3 p1, Vector3 p2){
            points.Add(p1 - this.GlobalTransform.origin + Vector3.Up);
            points.Add(p2 - this.GlobalTransform.origin + Vector3.Up);
        }
        public override void _Process(float delta){
            Clear();
            Begin(Mesh.PrimitiveType.Lines);

            for (int i = 0; i < points.Count; ++i){
                AddVertex(points[i]);
            }
            points.Clear();
            End();
        }
    }
}