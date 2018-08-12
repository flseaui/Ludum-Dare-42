extends Node2D

onready var Grid = get_parent()
onready var person_top = get_child(0)
onready var person_bottom = get_child(1)

var timer = null
var input_direction = null 

func _ready():
	timer = Timer.new()
	
	add_child(timer)
	timer.connect("timeout", self, "drop_down")
	timer.set_wait_time(1.0)
	timer.set_one_shot(false)
	timer.start()
	
	update_look_direction(Vector2(1, 0))
	
	
func _process(delta):
	drop_person()
	rotate_person()
	move_person()
	

func spawn_person():
	person_top.position = Vector2(70, 30)
	person_bottom.position = Vector2(70, 38)
	Grid.add_person(person_bottom.position)

	
func drop_person():
	var drop_input = Input.is_action_just_pressed("drop_person")
	var full_drop_input = Input.is_action_just_pressed("full_drop_person")
	if full_drop_input:
		# full drop
		timer.set_paused(true)
		for i in range(13):
			drop_down()
		return
	if drop_input:	
		# single drop
		timer.set_paused(true)
		drop_down()
		
	timer.set_paused(false)
	
	
func move_person():
	input_direction = getinput_direction()
	if not input_direction:
		return
		
	timer.set_paused(true)
	
	input_direction = Vector2(input_direction.x, 0)
	update_look_direction(input_direction)
	
	var orientation = Grid.get_person_orientation(person_top, person_bottom)
	if (orientation == 2 && input_direction.x <= -1) || (orientation == 3 && input_direction.x >= 1):
		var bottom_target_position = Grid.request_move(person_bottom, input_direction)
		if bottom_target_position:
			move_pawn(person_bottom, bottom_target_position)
		var top_target_position = Grid.request_move(person_top, input_direction)
		if top_target_position:
			move_pawn(person_top, top_target_position)
	else:
		var top_target_position = Grid.request_move(person_top, input_direction)
		if top_target_position:
			move_pawn(person_top, top_target_position)
		var bottom_target_position = Grid.request_move(person_bottom, input_direction)
		if bottom_target_position:
			move_pawn(person_bottom, bottom_target_position)
			
	timer.set_paused(false)
	
	
func move_pawn(pawn, target_position):
	pawn.position = target_position
	
	
func rotate_person():
	var rotation_direction = get_rotation_direction()
	if rotation_direction == null: 
		return
		
	timer.set_paused(true)
	
	var top_target_position = Grid.request_move(person_top, 
			rotation_from_orientation(Grid.get_person_orientation(person_top, person_bottom), rotation_direction))
	if top_target_position:
		move_pawn(person_top, top_target_position)
	
	timer.set_paused(false)
	
	
func rotation_from_orientation(orientation, direction):
	match Grid.get_person_orientation(person_top, person_bottom):
		0:
			# left from up
			if direction == 0:
				return(Vector2(-1, 1))
			# right from up
			else:
				return(Vector2(1, 1))
		1:
			# left from down
			if direction == 0:
				return(Vector2(1, -1))
			# right from down
			else:
				return(Vector2(-1, -1))
		2:
			# left from right
			if direction == 0:
				return(Vector2(-1, -1))
			# right from right
			else:
				return(Vector2(-1, 1))
		3:
			# left from left
			if direction == 0:
				return(Vector2(1, 1))
			# right from left
			else:
				return(Vector2(1, -1))
			
			
func drop_down():
	var orientation = Grid.get_person_orientation(person_top, person_bottom)
	var drop_distance = 1
		
	
	if orientation == 1:
		var top_target_position = Grid.request_move(person_top, Vector2(0, drop_distance))
		if top_target_position:
			move_pawn(person_top, top_target_position)
		else:
			spawn_person()
		var bottom_target_position = Grid.request_move(person_bottom, Vector2(0, drop_distance))
		if bottom_target_position:
			move_pawn(person_bottom, bottom_target_position)
		else:
			spawn_person()
	else:
		var bottom_target_position = Grid.request_move(person_bottom, Vector2(0, drop_distance))
		if bottom_target_position:
			move_pawn(person_bottom, bottom_target_position)
		else:
			spawn_person()
		var top_target_position = Grid.request_move(person_top, Vector2(0, drop_distance))
		if top_target_position:
			move_pawn(person_top, top_target_position)
		else:
			spawn_person()
		
		
func getinput_direction():
	return Vector2(
		int(Input.is_action_pressed("ui_right")) - int(Input.is_action_pressed("ui_left")),
		int(Input.is_action_pressed("ui_down")) - int(Input.is_action_pressed("ui_up"))
	)
	
	
func get_rotation_direction():
	if Input.is_action_just_pressed("rotate_left"): 
		return(0)
	elif Input.is_action_just_pressed("rotate_right"): 
		return(1)
		
		
func update_look_direction(direction):
	print("dir")
	#$Pivot/Sprite.rotation = 0#direction.angle()