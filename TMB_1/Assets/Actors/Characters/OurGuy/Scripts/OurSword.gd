extends Node2D

signal AttackEnded

func on_attack_ended():
	self.emit_signal("AttackEnded")
	self.queue_free()

func on_Hit(body):
	if body.has_method("OnDamage"):
		body.OnDamage()
