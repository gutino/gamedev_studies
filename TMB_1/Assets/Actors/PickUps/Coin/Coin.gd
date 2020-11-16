extends PickUpItem

func _PickedUp(body):
	if body.has_method("_on_Coin_pickedUp"):
		body._on_Coin_pickedUp() 
	self.queue_free()

func DisableCollision():
	$CollisionShape2D.disabled = true
