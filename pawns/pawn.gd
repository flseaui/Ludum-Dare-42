extends Node2D

enum CELL_TYPES { EMPTY = -1, PERSON_TOP, PERSON_BOTTOM, OBSTACLE, OBJECT }
export(CELL_TYPES) var type = PERSON_BOTTOM
