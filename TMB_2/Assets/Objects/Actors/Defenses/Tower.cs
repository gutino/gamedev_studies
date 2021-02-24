using Godot;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TowerDefense{
    public abstract class Tower : Spatial{

        [Export]
        public int WeaponDmg {get; set;} = 1;

        [Export]
        public float AttackDelay {get; set;} = 1.0f;
        private Timer AttackTimer {get;} = new Timer();
        private bool Attacking {get; set;} = false;
        private List<WeakRef> CloseEnemies {get;} = new List<WeakRef>();
        private readonly LineDrawer3d LineDrawer = new LineDrawer3d();
        private Area RangeArea {get{return this.GetNode<Area>("RangeArea");}}
        protected Spatial ProjectileSpawner {get{return this.GetNode<Spatial>("ProjectileSpawner");}}

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
                CloseEnemies.Add( WeakRef(enemy) );

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
                    RemoveEnemy( WeakRef(enemy) );
                }
            }
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
                }
            }
        }

        // TODO: jogar para interface.
        public abstract void FireProjectile(WeakRef target);

        public void RemoveEnemy(WeakRef enemy){
            CloseEnemies.RemoveAll( e => e.GetRef() == enemy.GetRef() );
        }
        
    }

    public class LineDrawer3d : ImmediateGeometry{
        readonly List<Vector3> points = new List<Vector3>();

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