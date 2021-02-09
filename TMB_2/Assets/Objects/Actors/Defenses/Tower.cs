using Godot;
using System;

namespace TowerDefense{
    public class Tower : Spatial{

        private Area RangeArea {get{return this.GetNode<Area>("RangeArea");}}
        public override void _Process(float delta){
            
        }
    }
}