using System.Collections.Generic;

namespace TowerDefense {
  public static class TileDict {
    private static readonly Dictionary<string, MapTile> Dict = new Dictionary<string, MapTile>() {

      //Spawn
      { "tile_spawnRound" , new SpawnTile() },

      //End
      { "tile_endRound" , new GoalTile() },

      //Buildables
      { "tile" , new BuildableTile() },

      //Ornaments
      { "tile_treeQuad" , new OrnamentTile() },
      { "tile_rock"     , new OrnamentTile() },

      //Paths
      { "tile_straight"         , new PathTile( new Exit[] { Exit.FORWARD } ) },
      { "tile_cornerRoundRight" , new PathTile( new Exit[] { Exit.RIGHT } ) },
      { "tile_cornerRoundLeft"  , new PathTile( new Exit[] { Exit.LEFT } ) },
      { "tile_TJoin"            , new PathTile( new Exit[] { Exit.BACK } ) },
      { "tile_TSplit"           , new PathTile( new Exit[] { Exit.RIGHT, Exit.LEFT } ) }
    };
    
    public static MapTile GetTile(string tileName){ return Dict[tileName]; }

  }
}