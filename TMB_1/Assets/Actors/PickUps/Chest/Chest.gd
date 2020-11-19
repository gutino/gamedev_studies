extends PickUpItem

export(PackedScene) var Item:PackedScene

var CollidedBody
var ItemInstancePath:NodePath

func _ready():
	$AnimatedSprite.frame = 0

func _PickedUp(body):
	
	$AnimationPlayer.play("Opened")
	var pickedUpItem = Item.instance()
	
	pickedUpItem.DisableCollision()
	pickedUpItem.z_index = 10
	
	$ItemSpawner.add_child(pickedUpItem)
	
	self.ItemInstancePath = pickedUpItem.get_path()
	
	self.CollidedBody = body

func _PickUpChild():
	
	var item := self.get_node_or_null(ItemInstancePath)
	
	if (item != null && item.has_method("_PickedUp")):
		item._PickedUp(self.CollidedBody)
	self.queue_free()
