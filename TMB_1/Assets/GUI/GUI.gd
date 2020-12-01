extends CanvasLayer

func UpdateCoinCounter():
	$Container/Icons/CoinCounter/Counter.text = "*" + str(OurGuyVars.Coins)

func _ready():
	UpdateCoinCounter()

func _process(delta):
#	if OurGuyVars.Health == 0:
#		$Container/Icons/Health/Heart1/AnimatedSprite.frame = 1
#		$Container/Icons/Health/Heart2/AnimatedSprite.frame = 1
#		$Container/Icons/Health/Heart3/AnimatedSprite.frame = 1
#	elif OurGuyVars.Health == 1:
#		$Container/Icons/Health/Heart1/AnimatedSprite.frame = 0
#		$Container/Icons/Health/Heart2/AnimatedSprite.frame = 1
#		$Container/Icons/Health/Heart3/AnimatedSprite.frame = 1
#	elif OurGuyVars.Health == 2:
#		$Container/Icons/Health/Heart1/AnimatedSprite.frame = 0
#		$Container/Icons/Health/Heart2/AnimatedSprite.frame = 0
#		$Container/Icons/Health/Heart3/AnimatedSprite.frame = 1
#	elif OurGuyVars.Health == 3:
#		$Container/Icons/Health/Heart1/AnimatedSprite.frame = 0
#		$Container/Icons/Health/Heart2/AnimatedSprite.frame = 0
#		$Container/Icons/Health/Heart3/AnimatedSprite.frame = 0
	
	if OurGuyVars.Health >2:
		$Container/Icons/Health/Heart3/AnimatedSprite.frame = 0
	else : $Container/Icons/Health/Heart3/AnimatedSprite.frame = 1
	
	if OurGuyVars.Health >1:
		$Container/Icons/Health/Heart2/AnimatedSprite.frame = 0
	else : $Container/Icons/Health/Heart2/AnimatedSprite.frame = 1
	
	if OurGuyVars.Health >0:
		$Container/Icons/Health/Heart1/AnimatedSprite.frame = 0
	else : $Container/Icons/Health/Heart1/AnimatedSprite.frame = 1

func _onCoinPickedUp():
	UpdateCoinCounter()
