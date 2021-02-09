using Godot;

namespace TowerDefense {

  public class Vector3Int{
    
    public int X {get;set;}
    public int Y {get;set;}
    public int Z {get;set;}

    public Vector3Int(Vector3 vector3){
      this.X = (int)vector3.x;
      this.Y = (int)vector3.y;
      this.Z = (int)vector3.z;
    }

    public Vector3Int(int x, int y, int z){
      this.X = x;
      this.Y = y;
      this.Z = z;
    }
  }

}