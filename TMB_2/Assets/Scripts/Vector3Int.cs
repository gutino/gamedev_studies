using Godot;

namespace TowerDefense {

  public class Vector3Int{
    
    public int x {get;set;}
    public int y {get;set;}
    public int z {get;set;}

    public Vector3Int(Vector3 vector3){
      this.x = (int)vector3.x;
      this.y = (int)vector3.y;
      this.z = (int)vector3.z;
    }
  }

}