extends "pawn.gd"

onready var Grid = get_parent()

var _timer = null
var input_direction = null

func _ready():
	_timer = Timer.new()
	
	add_child(_timer)
	_timer.connect("timeout", self, "drop_down")
	_timer.set_wait_time(1.0)
	_timer.set_one_shot(false)
	_timer.start()
	
	update_look_direction(Vector2(1, 0))


func _process(delta):
	rotate()
	
	input_direction = get_input_direction()
	if not input_direction:
		return
	input_direction = Vector2(input_direction.x, 0)
	update_look_direction(input_direction)
	
	var target_position = Grid.request_move(self, input_direction)
	if target_position:
		move_to(target_position)
	else:
		bump()

func rotate():
	var rotation_direction = get_rotation_direction()
	if rotation_direction == null: return
	print("rot ", rotation_direction)

func drop_down():
	var target_position = Grid.request_move(self, Vector2(0, 1))
	if target_position:
		move_to(target_position)
	else:
		bump()

func get_input_direction():
	return Vector2(
		int(Input.is_action_pressed("ui_right")) - int(Input.is_action_pressed("ui_left")),
		int(Input.is_action_pressed("ui_down")) - int(Input.is_action_pressed("ui_up"))
	)

func get_rotation_direction():
	if (Input.is_action_just_pressed("rotate_left")): return(0)
	elif (Input.is_action_just_pressed("rotate_right")): return(1)

func update_look_direction(direction):
	$Pivot/Sprite.rotation = 0#direction.angle()


func move_to(target_position):
	set_process(false)
	#$AnimationPlayer.play("walk")

	# Move the node to the target cell instantly,
	# and animate the sprite moving from the start to the target cell
	#var move_direction = (target_position - position).normalized()
	#$Tween.interpolate_property($Pivot, "position", - move_direction * 32, Vector2(), $AnimationPlayer.current_animation_length, Tween.TRANS_LINEAR, Tween.EASE_IN)
	position = target_position

	#$Tween.start()

	# Stop the function execution until the animation finished
	#yield($AnimationPlayer, "animation_finished")
	
	set_process(true)


func bump():
	set_process(false)
	#$AnimationPlayer.play("bump")
	#yield($AnimationPlayer, "animation_finished")
	set_process(true)
