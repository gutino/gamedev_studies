using System;
using System.Collections.Generic;
using Godot;

namespace TowerDefense {
  public class GridMapTest : GridMap {

    private static readonly float[] orthogonal_angles = {
      0.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      Mathf.Pi,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      Mathf.Pi/2,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      1.0f,
      -Mathf.Pi/2,
      1
    };

    public override void _Ready(){

      foreach (Vector3 i in this.GetUsedCells()){
        var tileIndex = this.GetCellItem( (int)i.x, (int)i.y, (int)i.z);
        var tileName = this.MeshLibrary.GetItemName(tileIndex);

        MapTile info = TileDict.GetTile(tileName);

        if ( info.Type == TileType.PATH ){
          this.SetCellItem(
            (int) i.x, 1, (int) i.z,
            0,
            GetOrientation( info.Exits, this.GetCellItemOrientation( (int)i.x, (int)i.y, (int)i.z ))
          );
        }
      }
    }

    private int GetOrientation(Exit[] exits, int rotation){
      Exit direction = exits[0];
      Basis basis;

      switch (direction)
      {
          case Exit.FORWARD:
            basis = Basis.Identity; break;
          case Exit.RIGHT:
            basis = new Basis(Vector3.Back, Vector3.Up, Vector3.Left); break;
          case Exit.BACK:
            basis = new Basis(Vector3.Left, Vector3.Up, Vector3.Forward); break;
          case Exit.LEFT:
            basis = new Basis(Vector3.Forward, Vector3.Up, Vector3.Right); break;
          default:
            basis = Basis.Identity; break;
      }
      
      basis = basis.Rotated(Vector3.Up,orthogonal_angles[rotation]);
      
      return basis.GetOrthogonalIndex();
    }
  }
}
