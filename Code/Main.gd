extends Spatial

# declate here some "global" variables

const RADIUS_OF_EARTH = 6.371

# this is used in calcumating angular momentum, it is
# in 10^15m^3/s^2
const GM = 0.3986

#this is how much to multiply time by
var time_factor = 10

func _ready():
	create_random_sattelites_in_orbits(5,60)
	pass
	
func create_random_sattelites(n):
	var satteliteScene = preload("res://Sattelite.tscn")
	for i in range(0,n):
		var mySattl = satteliteScene.instance()
		mySattl.init(
			randf() * 1.7 + 1.15,
			randf() * PI,
			randf() * 2 * PI,
			randf() * 2 * PI,
			(rand_range(0,1) == 0)
		)
		add_child(mySattl)
		
func create_random_sattelites_in_orbits(n,k):
	var satteliteScene = preload("res://Sattelite.tscn")
	for i in range(0,n):
		var alt = randf() * 1.7 + 1.15
		var incl = randf() * PI
		var long_off = randf() * 2 * PI
		var retro = (rand_range(0,1) == 0)
		for j in range(0,k):
			var mySattl = satteliteScene.instance()
			mySattl.init(
				alt,
				incl,
				long_off,
				2 * PI * j / k,
				retro
			)
			add_child(mySattl)