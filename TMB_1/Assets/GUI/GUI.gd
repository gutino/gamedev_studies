extends CanvasLayer

var HeartScn = preload("res://Assets/GUI/Health/Heart.tscn")
var CurrHearts := 1

func UpdateCoinCounter():
	$Container/Icons/CoinCounter/Counter.text = "*" + str(OurGuyVars.Coins)

func _ready():
	UpdateHealth()
	UpdateCoinCounter()

func UpdateHealth():
	var acc = 0
	
	if OurGuyVars.Health > CurrHearts:
		AddHearts(OurGuyVars.Health - CurrHearts)

	print ($Container/Icons/Health.get_children())
	
	for child in $Container/Icons/Health.get_children():
		if child.has_node("AnimatedSprite"):
			if OurGuyVars.Health > acc:
				child.get_node("AnimatedSprite").frame = 0
				acc += 1
			else:
				child.get_node("AnimatedSprite").frame = 1
		else:
			print("Failed to Update Health")
	
func _onCoinPickedUp():
	UpdateCoinCounter()

func _on_OurGuy_OnDamaged():
	UpdateHealth()

func AddHearts(var qty : int):
	for i in range(qty):
		var NewHeart = HeartScn.instance()
		$Container/Icons/Health.add_child(NewHeart)
		CurrHearts += 1
