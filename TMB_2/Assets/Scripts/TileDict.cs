using System.Collections.Generic;

namespace TowerDefense {
  static public class TileDict {
    private static readonly Dictionary<string, MapTile> Dict = new Dictionary<string, MapTile>(){

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
    
    public static MapTile GetTile(string tileName) => Dict[tileName];

  }
}