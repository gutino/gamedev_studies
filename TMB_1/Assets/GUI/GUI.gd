extends CanvasLayer

func UpdateCoinCounter():
	$Container/Icons/CoinCounter/Counter.text = "*" + str(OurGuyVars.Coins)

func _ready():
	UpdateCoinCounter()

func _onCoinPickedUp():
	UpdateCoinCounter()
