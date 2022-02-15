PerlinNoise class is used to use the PerlinNoise algorithm to generate the noise map sample, make the map spawning will not become linear, it is very flexible.

MapController class is used to control and manager the tile prefab, Instantiate the gameobjects, and generate the map and combine them into one big map.

NewMeshGenerator class is used to manager the meshes spawner in the world, also calculate the vertice and store them.

PlayerController class is used to get the camera input, and use wasd to move around, and use the mouse to rotate.

TextureController class is used to build the texture in the maps

TileGenerator class is used generated noise map to the plane, and then set up the different types of the area and terrain into the map, also set up the color
wave, seed... by the user.


No, I don't use flyweight pattern elsewhere, because I used unity for my engine, so Unity has prefab

My importation is input the density of the spawning objects, I did not use prototype pattern because of for one catergory I only spawn 2-3 different objects, that means it is 
very small size of the groups. I think prototype will be useful when you have many different types objects, and they have many same attributes. If I have to do it 
differently, I will make my map has more details, polish the UI stuff, when the player goes to the empty space, the map will generate new maps for him.
  