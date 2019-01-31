extends MeshInstance

var satObj

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	satObj = get_parent()
	pass

#func _draw():
#	for n in range(0,satObj.numOfLinks):
#		link = satObj.links[n]
#		link.begin(1)
#		link.add_vertex(translation)
#		link.add_vertex(to_local(satObj.linkSats[n].translation));
#		link.end();