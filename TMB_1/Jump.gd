extends "res://Move.gd"

func enter():
	_Actor.get_node("JumpTimer").start(_Actor._JumpingDuration)
	_Actor._IsJumping = true
	_Actor._CurrSpeed.y = -_Actor._JumpForce

func process(delta):
	
	if (Input.is_action_just_released("ui_accept") || !_Actor._IsJumping):
		self._Machine.changeState("Idle")
		
	.process(delta)

func exit():
	_Actor._IsJumping = false
