extends Node

class_name StateMachine

onready var _Actor : KinematicBody2D = self.get_parent()
onready var _CurrState := $Idle

func _ready():
	for state in self.get_children():
		state._Actor = self.get_parent()

func _process(delta):
	_CurrState.process(delta)
	
func _physics_process(delta):
	_CurrState.physics_process(delta)

func changeState(nextState:String) -> void:
	var nextStateNode : State = self.get_node_or_null(nextState)
	
	#print("----------------------\nChanging state\nCurrent: "+self._CurrState.name+"\nNext:"+nextState)
	
	if (_CurrState == nextStateNode):
		#print("Same state, change canceled")
		return
	
	_CurrState.exit()
	
	if nextStateNode == null:
		nextStateNode = State.new()
	else:
		nextStateNode.enter()
	
	self._CurrState = nextStateNode
