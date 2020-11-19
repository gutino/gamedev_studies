extends State

class_name OurGuyState

func process(delta):
	if Input.is_action_just_pressed("attack") && !_Actor._Attacking :
		_Actor.EngageAttack()
