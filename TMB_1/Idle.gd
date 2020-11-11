extends State

func process(delta):
	
	if Input.is_action_just_pressed("ui_accept") && _Actor.is_on_floor():
		_Actor._IsJumping = true
	
	if _Actor._IsJumping:
		self._Machine.changeState("Jump")
	elif (
		Input.is_action_pressed("ui_left") ||
		Input.is_action_pressed("ui_right")
	):
		self._Machine.changeState("Move")
	else:
		_Actor._CurrSpeed.x = lerp(_Actor._CurrSpeed.x, 0, _Actor._Deaccel)

