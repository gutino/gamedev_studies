extends Node2D

signal AttackEnded

func on_attack_ended():
	self.emit_signal("AttackEnded")
	self.queue_free()
