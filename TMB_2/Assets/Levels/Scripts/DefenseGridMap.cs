using Godot;

namespace TowerDefense {
    public class DefenseGridMap : GridMap {

        public Vector3Int SpawnPoint { get; set; }

        private float GetRotationFromOrientation(int OrthogonalAngle) {

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

        public override void _Ready() {

            (GetNode("/root/EnemySpawner") as EnemySpawner)?.SetOwnerMap(this);

            foreach (Vector3 i in this.GetUsedCells()) {

                var cellPos = new Vector3Int(i);


                MapTile info = this.GetMapTile(cellPos);

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

        public MapTile GetMapTile(Vector3Int tileLocation){
            return this.GetMapTile(tileLocation.X,tileLocation.Y,tileLocation.Z);
        }

        public MapTile GetMapTile(int x, int y, int z){

            var tileIndex = this.GetCellItem(x,y,z);

            if (tileIndex == -1){
                return null;
            }

            return TileDict.GetTile( this.MeshLibrary.GetItemName(tileIndex) );
        }
    }
}
