extends KinematicBody2D

export(int) var RightLimit := 111
export(int) var LeftLimit := -31
export(float) var HorizontalAccel := 5.0
export(float) var VerticalAccel := 5.0

var CurrPos := Vector2()
var CurrVelocity := Vector2(5, 5)
var GoingRight := true
var PlayerPosition := Vector2()
var PlayerNode

func OnDamage():
	self.queue_free()

func CheckCollisions():
	for i in get_slide_count():
		var collision = get_slide_collision(i)
		if collision.collider.has_method("OnDamage"):
			collision.collider.OnDamage()
	
func ChangeDirection() -> void:
	GoingRight = !GoingRight
	$AnimatedSprite.scale.x *= -1
	
	if GoingRight:
		CurrVelocity = Vector2(HorizontalAccel, VerticalAccel)
	else:
		CurrVelocity = Vector2(-HorizontalAccel, VerticalAccel)
	
func _physics_process(delta):
	CurrPos = get_position()
	if CurrPos.x > RightLimit || CurrPos.x < LeftLimit:
		ChangeDirection()
	
	self.move_and_slide(CurrVelocity)
	CheckCollisions()
		
func PlayerOnArea() -> bool:
	PlayerPosition = PlayerNode.get_position()
	return PlayerPosition.x <= RightLimit && PlayerPosition.x >= LeftLimit

func _process(delta):
	if PlayerOnArea():
		if ((PlayerPosition.x > CurrPos.x && !GoingRight) 
			|| (PlayerPosition.x < CurrPos.x && GoingRight)):
			ChangeDirection()

func _ready():
	PlayerNode = get_parent().get_node("OurGuy")
