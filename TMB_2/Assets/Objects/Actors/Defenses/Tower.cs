using Godot;
using System;
using System.Collections.Generic;

namespace TowerDefense{
    public class Tower : Spatial{

        [Export]
        public int WeaponDmg {get; set;} = 1;

        [Export]
        public float AttackDelay {get; set;} = 1.0f;
        private Timer AttackTimer {get;} = new Timer();
        private bool Attacking {get; set;} = false;
        private List<Enemy> CloseEnemies {get;} = new List<Enemy>();
        private readonly LineDrawer3d LineDrawer = new LineDrawer3d();
        private Area RangeArea {get{return this.GetNode<Area>("RangeArea");}}

        public override void _Ready(){
            this.AddChild(this.LineDrawer);
            RangeArea.Connect("body_entered", this, nameof(On_Area_Entered));
            RangeArea.Connect("body_exited", this, nameof(On_Area_Exited));

            this.AddChild(AttackTimer);
            AttackTimer.OneShot = false;
            AttackTimer.Connect("timeout", this, nameof(On_Attack_Timeout));
        }

        public void On_Area_Entered(Node body){
            if (body.IsInGroup("Enemies")){
                Enemy enemy = body.GetOwner<Enemy>();
                CloseEnemies.Add(enemy);
                enemy.Connect("EnemyDied", this, nameof(On_Enemy_Death));

                if (CloseEnemies.Count > 0 && !Attacking){
                    Attacking = true;
                    this.Attack();
                    AttackTimer.Start(AttackDelay);
                }   
            }
        }

        public void On_Area_Exited(Node body){
            if (body.IsInGroup("Enemies")){
                Enemy enemy = body.GetOwner<Enemy>();
                if (enemy != null){
                    RemoveEnemy(enemy);
                }
            }
        }

        public void On_Enemy_Death(Enemy enemy){
            RemoveEnemy(enemy);
            // TODO: Colocar lógica de morte apenas dentro do enemy.
            enemy.QueueFree();
        }

        public void On_Attack_Timeout(){
            if (CloseEnemies.Count > 0){
                this.Attack();
                this.AttackTimer.Start(AttackDelay);
            } else {
                Attacking = false;
                AttackTimer.Stop();
            }
            
        }
        
        private void Attack(){
            foreach (var enemy in CloseEnemies.ToArray()){
                if (enemy != null){
                    this.FireProjectile(enemy);
                    // LineDrawer.AddLine(this.Transform.origin, enemy.Transform.origin);
                    enemy.TakeDmg(WeaponDmg);
                }
            }
        }

        // TODO: jogar para interface.
        public virtual void FireProjectile(Enemy target){}

        public void RemoveEnemy(Enemy enemy){
            enemy.Disconnect("EnemyDied", this, nameof(On_Enemy_Death));
            CloseEnemies.Remove(enemy);
        }
        
    }

    public class LineDrawer3d : ImmediateGeometry{
        List<Vector3> points = new List<Vector3>();

        public void AddLine(Vector3 p1, Vector3 p2){
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