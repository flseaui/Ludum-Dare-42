extends TileMap

enum CELL_TYPES { EMPTY = -1, ACTOR, OBSTACLE, OBJECT, P0_TOP, P0_BOTTOM }

func _ready():
	for person in get_children():
		for person_segmant in person.get_children():
			if person_segmant.get_name() == "Top" || person_segmant.get_name() == "Bottom":
				set_cellv(world_to_map(person_segmant.position), person_segmant.type)
		
		 
func get_cell_pawn(coordinates):
	for node in get_children():
		if world_to_map(node.position) == coordinates:
			return(node)
			
			
func get_person_orientation(top, bottom):
	var top_cell = world_to_map(top.position)
	var bottom_cell = world_to_map(bottom.position)
	
	#print("tx: %s, ty: %s" % [top_cell.x, top_cell.y])
	#print("bx: %s, by: %s" % [bottom_cell.x, bottom_cell.y])
	
	if top_cell.y < bottom_cell.y:
		# oriented up
		return 0
	elif top_cell.y > bottom_cell.y:
		# oriented down
		return 1
		
	if top_cell.x > bottom_cell.x:
		# oriented right
		return 2
	elif top_cell.x < bottom_cell.x:
		# oriented left
		return 3
		
		
func add_person(bottom_coords):
	set_cellv(world_to_map(bottom_coords),  P0_BOTTOM)
	set_cellv(world_to_map(Vector2(bottom_coords.x, bottom_coords.y - 1)),  P0_TOP)
		
		
func request_move(pawn, direction):
	var cell_start = world_to_map(pawn.position)
	var cell_target = cell_start + direction
	
	var cell_target_type = get_cellv(cell_target)
	match cell_target_type:
		EMPTY:
			return update_pawn_position(pawn, cell_start, cell_target)
		OBJECT:
			var object_pawn = get_cell_pawn(cell_target)
			if object_pawn:
				object_pawn.queue_free()
				return update_pawn_position(pawn, cell_start, cell_target)
		[ACTOR, P0_BOTTOM, P0_TOP]:
			var cell_pawn = get_cell_pawn(cell_target)
			if cell_pawn:
				print("Cell %s contains %s" % [cell_target, cell_pawn.name])
			
			
func update_pawn_position(pawn, cell_start, cell_target):
	set_cellv(cell_target, pawn.type)
	set_cellv(cell_start, EMPTY)
	return map_to_world(cell_target) + cell_size / 2
	
	
func vertical_distance_to_tile(pawn):
	var cell = world_to_map(pawn.position)
	var hit = false
	var distance = 0
	
	while not hit:
		distance += 1
		if get_cell(cell.x, cell.y + distance) != EMPTY:
			hit = true
			
	return(distance)