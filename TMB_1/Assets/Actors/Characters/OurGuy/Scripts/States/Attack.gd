extends State

const _Sword = preload("../../OurSword.tscn")

func enter():
	var sword_obj = _Sword.instance()
	var anim = sword_obj.get_node("AnimationPlayer")
	
	sword_obj.connect("AttackEnded", self, "on_attack_ended")
	sword_obj.scale.x = _Actor.get_node("Sprite").scale.x
	
	if _Actor._GoingLeft:
		anim.play("AttackL")
	else:
		anim.play("AttackR")
	
	#_Actor._CurrSpeed.x = 0
	
	_Actor.get_node("SwordSpawn").add_child(sword_obj)
	
func on_attack_ended():
	#Implementar Lista
	_Machine.changeState("Idle")
