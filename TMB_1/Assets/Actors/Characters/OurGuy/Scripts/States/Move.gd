extends State

func process(delta):
	if !self.checkMovementInput():
		_Machine.changeState("Idle")
		return
		
	if Input.is_action_just_pressed("ui_accept") && _Actor.is_on_floor():
		_Machine.changeState("Jump")
		return
	if Input.is_action_just_pressed("attack") && _Actor.is_on_floor():
		_Machine.changeState("Attack")
		return
	
	self.getFinalXSpeed()

func getFinalXSpeed() -> void:
	var inputVelocity : float = _Actor._HorizontalVelocity
  
	if Input.is_action_pressed("sprint"):
		inputVelocity = _Actor._SprintVelocity
  
	var inputStrength = (
		Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	)
	_Actor._CurrSpeed.x = lerp(_Actor._CurrSpeed.x, inputVelocity * inputStrength, _Actor._Accel)

func checkMovementInput() -> bool:
	if (
		Input.is_action_pressed("ui_left")  ||
		Input.is_action_pressed("ui_right") ||
		_Actor._IsJumping
	):
		return true
	else:
		return false
