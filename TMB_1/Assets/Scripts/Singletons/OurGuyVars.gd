extends Node

var Coins := 0
var Health := 3

func Death():
	self.get_tree().reload_current_scene()
	OurGuyVars.Coins = 0
