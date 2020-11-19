extends KinematicBody2D

func OnBodyEntered(body):
	if body.has_method("OnDamage"):
		body.OnDamage()
	pass
	
func _physics_process(delta):
	self.move_and_slide(Vector2(5,5))
	
func _ready():
	pass
