extends Spatial

# these variables determine the orbit of the sattelite.
# The earth here has a radius of 1

# distance is measured in thousands of kilometers, or
# 10^6m
var distance_above_surface = 1.320

# Very few sources document distance as distancefromthe core
# of the earth, so we compute
var distance_above_core

#this is the angle to the equatorial plane
var inclination = 0

# this is longditite of the points at which the orbit
# crosses the equatorial plane
var longditudanal_offset = 0

# are we orbiting the earth in the direction of it's rotation, or against it?
var retrograde = false

# this is the current angle our satellite is at.
# we're going to use this value, and constantly update
# our position based off of it.
var true_anomaly = 0

# LEAVE EMPTY we calculate this from the distance on
# ready
var velocity
var angular_velocity

# how much to multiply time by
var timefactor

# we store the Earth here
var the_earth
var the_universe

# Called when the node enters the scene tree for the first time.
func _ready():
	var the_earth = get_node("/root/The Earth")
	var the_universe = get_node("/root/Universe")
	
	timefactor = the_universe.time_factor
	
	# we set our actual distance from (0,0,0)
	distance_above_core = distance_above_surface + the_universe.RADIUS_OF_EARTH
	
	velocity = sqrt(the_universe.GM / distance_above_core)
	
	# due to the fact we are messing around with floats, we multiply
	# in timefactor when rdetermining angular velocity
	angular_velocity = (velocity * timefactor) / distance_above_core

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if (retrograde):
		true_anomaly -= angular_velocity * delta
	else:
		true_anomaly += angular_velocity * delta
	
	# now for a tricky bit, calculating our positon
	# from our current angle along our orbital path
	var longditude = longditudanal_offset + true_anomaly
	var latitude = inclination
	
	# we start with a point in the x direction
	var newPos = Vector3(distance_above_core, 0, 0)
	
	# first we treat our equatorial plane as our orbital plane
	newPos = newPos.rotated(Vector3(0,1,0),true_anomaly)
	
	# then we apply our inclination
	newPos = newPos.rotated(Vector3(1,0,0),inclination)
	
	# finally we apply our offset
	newPos = newPos.rotated(Vector3(0,1,0),longditudanal_offset)
	
	translate(to_local(newPos))
	pass

# we'll use this function to set all the starting parameters
# for our sattelite

func init(r,inc,long_off,init_anom,retro):
	distance_above_surface = r
	inclination = inc
	longditudanal_offset = long_off
	true_anomaly = init_anom
	retrograde = retro