extends Node

var Coins := 0
var Health := 3
var MaxHealth := 3
var Shield := 1

func Death():
	self.get_tree().reload_current_scene()
	Health = 3
	Coins = 0
