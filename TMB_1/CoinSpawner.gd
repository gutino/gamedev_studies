tool
extends Node2D 

enum eFormation { BigSquare, SmlSquare, HorLine, VertLine, Triangle1 }

export(eFormation) var _Formation := eFormation.BigSquare setget set_Formation

func UpdateFormation()->void:
	if (_Formation == eFormation.BigSquare):
		$Coin2.transform.origin = Vector2(0,-24)
		$Coin3.transform.origin = Vector2(24,-24)
		$Coin4.transform.origin = Vector2(24,0)
  
	elif (_Formation == eFormation.SmlSquare):
		$Coin2.transform.origin = Vector2(0,-16)
		$Coin3.transform.origin = Vector2(16,-16)
		$Coin4.transform.origin = Vector2(16,0)
	  
	elif (_Formation == eFormation.VertLine):
		$Coin2.transform.origin = Vector2(0,16)
		$Coin3.transform.origin = Vector2(0,32)
		$Coin4.transform.origin = Vector2(0,48)
		
	elif (_Formation == eFormation.Triangle1):
		$Coin2.transform.origin = Vector2(16,0)
		$Coin3.transform.origin = Vector2(8,-8)
		$Coin4.transform.origin = Vector2(8,-24)
	  
	else:
		$Coin2.transform.origin = Vector2(16,0)
		$Coin3.transform.origin = Vector2(32,0)
		$Coin4.transform.origin = Vector2(48,0)

func set_Formation(new_formation:int):
	_Formation = new_formation
	if(Engine.editor_hint):
		self.UpdateFormation()

func _ready():
	UpdateFormation()
