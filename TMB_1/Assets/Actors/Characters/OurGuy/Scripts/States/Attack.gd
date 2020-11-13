extends State

const _Sword = preload("../../OurSword.tscn")

func enter():
	var sword_obj = _Sword.instance()
	sword_obj.connect("AttackEnded", self, "on_attack_ended")
	sword_obj.scale.x = _Actor.get_node("Sprite").scale.x
	_Actor.get_node("SwordSpawn").add_child(sword_obj)
	
func on_attack_ended():
	_Machine.changeState("Idle")
