extends Area2D

signal pickedUp

func _ready():
	pass

func _on_Coin_body_entered(body):
	if body.has_method("_on_Coin_pickedUp"):
		body._on_Coin_pickedUp() 
	self.queue_free()
