extends KinematicBody

var camera_angle = 0
var mouse_sensitivity = 0.3
var camera_change = Vector2()

var velocity = Vector3()
var direction = Vector3()

#fly variables
const FLY_SPEED = 5
const FLY_ACCEL = 20

signal run_test;

#have we frozen the camera?
var frozen = false

func _ready():
	# Called every time the node is added to the scene.
	# Initialization here
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED);
	pass

func _physics_process(delta):
	aim()
	fly(delta)

func _input(event):
	if event is InputEventMouseMotion and !frozen:
		camera_change = event.relative
			
func fly(delta):
	# reset the direction of the player
	direction = Vector3()
	
	# get the rotation of the camera
	var aim = $Head/Camera.get_global_transform().basis
	
	# check input and change direction
	if Input.is_action_pressed("move_forward"):
		direction -= aim.z
	if Input.is_action_pressed("move_backward"):
		direction += aim.z
	if Input.is_action_pressed("move_left"):
		direction -= aim.x
	if Input.is_action_pressed("move_right"):
		direction += aim.x
	if Input.is_action_pressed("move_down"):
		direction -= aim.y
	if Input.is_action_pressed("move_up"):
		direction += aim.y
	if Input.is_action_pressed("test"):
		emit_signal("run_test")
	if Input.is_action_just_pressed("ui_cancel"):
		if (frozen):
			Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
			frozen = false
		else:
			Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
			frozen = true
	if Input.is_action_pressed("restart"):
		get_tree().reload_current_scene()
	
	direction = direction.normalized()
	
	# where would the player go at max speed
	var target = direction * FLY_SPEED
	
	# calculate a portion of the distance to go
	velocity = velocity.linear_interpolate(target, FLY_ACCEL * delta)
	
	# move
	move_and_slide(velocity)
	
func aim():
	if camera_change.length() > 0:
		$Head.rotate_y(deg2rad(-camera_change.x * mouse_sensitivity))

		var change = -camera_change.y * mouse_sensitivity
		if change + camera_angle < 90 and change + camera_angle > -90:
			$Head/Camera.rotate_x(deg2rad(change))
			camera_angle += change
		camera_change = Vector2()
