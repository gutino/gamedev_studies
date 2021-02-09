using Godot;

namespace TowerDefense {
  public class DefenseGridMap : GridMap {

	public Vector3Int SpawnPoint {get;set;}

	private float GetRotationFromOrientation (int OrthogonalAngle){

	  switch (OrthogonalAngle) {
		
		case 0:
		default:
		  return 0;
		case 10:
		  return Mathf.Pi;
		case 16:
		  return Mathf.Pi / 2;
		case 22:
		  return -Mathf.Pi / 2;
	  }
	}

	public override void _Ready(){

	  (GetNode("/root/EnemySpawner") as EnemySpawner).SetOwnerMap(this);

	  foreach (Vector3 i in this.GetUsedCells()){
		
		var cellPos = new Vector3Int(i);

		var tileIndex = this.GetCellItem( cellPos.X, cellPos.Y, cellPos.Z);
		var tileName = this.MeshLibrary.GetItemName(tileIndex);


		MapTile info = TileDict.GetTile(tileName);

		if (info.Type == TileType.SPAWN) this.SpawnPoint = new Vector3Int(i);

		// if ( info.Type == TileType.PATH ){
		//   this.SetCellItem(
		//     cellPos.x, 1,  cellPos.z,
		//     0,
		//     GetOrientation( info.Exits, this.GetCellItemOrientation( cellPos.x, cellPos.y, cellPos.z ))
		//   );
		// }
	  }

	}

	private int GetOrientation(Exit[] exits, int orientation){
	  
	  Exit direction = exits[0];
	  
	  Basis basis;

	  switch (direction){

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
	  
	  basis = basis.Rotated(Vector3.Up, GetRotationFromOrientation(orientation));
	  
	  return basis.GetOrthogonalIndex();
	}
  }
}
