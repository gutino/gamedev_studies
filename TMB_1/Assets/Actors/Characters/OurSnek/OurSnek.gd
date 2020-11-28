extends KinematicBody2D

export(float) var PatrolSpeed := 10.0
export(float) var ChasingFactor := 3.0

var IsChasing := false
var CurrVelocity := Vector2(5, 5)
var GoingRight := true
var ChasedNode

func OnDamage():
	self.queue_free()

func CheckCollisions():
	for i in self.get_slide_count():
		var collision = self.get_slide_collision(i)
		if collision.collider.has_method("OnDamage"):
			collision.collider.OnDamage()
	
func ChangeDirection() -> void:
	GoingRight = !GoingRight
	$AnimatedSprite.scale.x *= -1
	$EdgeChecker.position.x *= -1
	
func _physics_process(delta):
	CheckCollisions()
	
	if !$EdgeChecker.is_colliding():
		IsChasing = false
		ChangeDirection()
	
	if GoingRight:
		CurrVelocity = Vector2(PatrolSpeed, GlobalVars.GravitySpeed)
	else:
		CurrVelocity = Vector2(-PatrolSpeed, GlobalVars.GravitySpeed)
	
	if IsChasing:
		CurrVelocity.x *= ChasingFactor
		
	self.move_and_slide(CurrVelocity)
	
func _process(delta):
	if IsChasing:
		if ((ChasedNode.position.x > position.x && !GoingRight) 
			|| (ChasedNode.position.x < position.x && GoingRight)):
			ChangeDirection()

func startChasing(body):
	IsChasing = true
	ChasedNode = body

func stopChasing(body):
	IsChasing = false
