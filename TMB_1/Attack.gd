extends State

const _Sword = preload("res://OurSword.tscn")

func enter():
	var sword_obj = _Sword.instance()
	sword_obj.connect("AttackEnded", self, "on_attack_ended")
	_Actor.add_child(sword_obj)
	
func on_attack_ended():
	_Machine.changeState("Idle")
