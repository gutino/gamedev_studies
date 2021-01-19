using System;

namespace TowerDefense {
  public abstract class MapTile {
    public TileType Type { get; set; }
    public Exit[] Exits { get; set; }
  }

  public class BuildableTile : MapTile {
    public BuildableTile(){
      this.Type = TileType.BUILDABLE;
      Exits = new Exit[] {};
    }
  }

  public class PathTile : MapTile {
    public PathTile(Exit[] exits){
      this.Type = TileType.PATH;
      Exits = exits;
    }
  }

  public class OrnamentTile : MapTile {
    public OrnamentTile(){
      this.Type = TileType.ORNAMENT;
      Exits = new Exit[] {};
    }
  }

}