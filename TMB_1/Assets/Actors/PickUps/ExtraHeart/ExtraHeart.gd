extends PickUpItem

export(int) var HealthQty := 1

func _PickedUp(body):
	if body.has_method("_on_ExtraHeart_pickedUp"):
		body._on_ExtraHeart_pickedUp(HealthQty) 
	self.queue_free()

func DisableCollision():
	$CollisionShape2D.disabled = true
