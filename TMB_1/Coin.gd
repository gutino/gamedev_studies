extends Area2D

signal pickedUp

func _ready():
	pass


func _on_Coin_body_entered(_body):
	emit_signal("pickedUp")
	self.queue_free()
