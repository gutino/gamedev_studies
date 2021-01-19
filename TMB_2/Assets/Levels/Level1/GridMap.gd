extends GridMap

const orthogonal_angles:Array = [
	Vector3(0, 0, 0),
	Vector3(0, 0, PI/2),
	Vector3(0, 0, PI),
	Vector3(0, 0, -PI/2),
	Vector3(PI/2, 0, 0),
	Vector3(PI, -PI/2, -PI/2),
	Vector3(-PI/2, PI, 0),
	Vector3(0, -PI/2, -PI/2),
	Vector3(-PI, 0, 0),
	Vector3(PI, 0, -PI/2),
	Vector3(0, PI, 0),
	Vector3(0, PI, -PI/2),
	Vector3(-PI/2, 0, 0),
	Vector3(0, -PI/2, PI/2),
	Vector3(PI/2, 0, PI),
	Vector3(0, PI/2, -PI/2),
	Vector3(0, PI/2, 0),
	Vector3(-PI/2, PI/2, 0),
	Vector3(PI, PI/2, 0),
	Vector3(PI/2, PI/2, 0),
	Vector3(PI, -PI/2, 0),
	Vector3(-PI/2, -PI/2, 0),
	Vector3(0, -PI/2, 0),
	Vector3(PI/2, -PI/2, 0)
]

enum EXIT { FORWARD, RIGHT, BACK, LEFT }

var TileInfo:Dictionary = {
	"tile" : { "walkable" : false }, 
	"tile_straight" : {
		"walkable" : true,
		"exits" : [EXIT.FORWARD]
	},
	"tile_cornerRoundRight" : {
		"walkable" : true,
		"exits" : [EXIT.RIGHT]
	},
	"tile_cornerRoundLeft" : {
		"walkable" : true,
		"exits" : [EXIT.LEFT]
	},
	"tile_TJoin" : {
		"walkable" : true,
		"exits" : [EXIT.BACK]
	},
	"tile_TSplit" : {
		"walkable" : true,
		"exits" : [EXIT.RIGHT,EXIT.LEFT]
	}
}

func _ready():
	
	for i in self.get_used_cells():
		
		var tileIndex = self.get_cell_item(i.x,i.y,i.z);
		var tileName = self.mesh_library.get_item_name(tileIndex);
		
		var basis;
		
		if ( TileInfo[tileName]["walkable"] ):
			self.set_cell_item(
				i.x, 1, i.z,
				2,
				GetOrientation(TileInfo[tileName]["exits"],
				self.get_cell_item_orientation(i.x,i.y,i.z))
			);


func GetOrientation(exits:Array, rotation:int)->int:
	pass
	var direction = exits[0];
	var basis:Basis;
	match (direction):
		
		EXIT.FORWARD:
			basis = Basis(Vector3(-1,0,0), Vector3(0,1,0), Vector3(0,0,-1));
		EXIT.RIGHT:
			basis = Basis(Vector3(0,0,1), Vector3(0,1,0), Vector3(1,0,0));
		EXIT.BACK:
			basis = Basis(Vector3(1,0,0), Vector3(0,1,0), Vector3(0,0,1));
		EXIT.LEFT:
			basis = Basis(Vector3(0,0,-1), Vector3(0,1,0), Vector3(-1,0,0));
	
	basis = basis.rotated(
		Vector3.UP,
		orthogonal_angles[rotation].angle_to(Vector3.FORWARD)
	);
	
	print("Direction:"+str(direction)+" ; Orthogonal:"+ str(basis));
	return basis.get_orthogonal_index();
	
