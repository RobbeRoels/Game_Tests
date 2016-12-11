using Completed;
using System;
using System.Collections;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.
    public enum TileType
    {
        Void, Wall, Floor, Entrance, Exit
    }


    public int columns = 100;                                 // The number of columns on the board (how wide it will be).
    public int rows = 100;                                    // The number of rows on the board (how tall it will be).
    public IntRange numRooms = new IntRange(15, 20);         // The range of the number of rooms there can be.
    public IntRange roomWidth = new IntRange(3, 10);         // The range of widths rooms can have.
    public IntRange roomHeight = new IntRange(3, 10);        // The range of heights rooms can have.
    public IntRange corridorLength = new IntRange(6, 10);    // The range of lengths corridors between rooms can have.
    public GameObject[] floorTiles;                           // An array of floor tile prefabs.
    public GameObject[] wallTiles;                            // An array of wall tile prefabs.
    public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.
	public GameObject entrance;								  // The entrance object
	public GameObject exit;									  // The exit object

    
	public GameObject player;

    private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
    private Room[] rooms;                                     // All the rooms that are created for this board.
    private Corridor[] corridors;                             // All the corridors that connect the rooms.
    private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.


    void awake() {


    }

    private void Start()
    {
        player = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        player.transform.position = entrance.transform.position;
        Camera.main.GetComponent<SmoothFollow>().target = player.transform;
    }

    public void SetUpLevel()
    {
        if(boardHolder != null)
        {
            //if the board holder already exists delete it and make a new one.
            Destroy(boardHolder);
        }
        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();


        SetTilesValuesForCorridors();

        InstantiateTiles();
        //InstantiateOuterWalls();
    }


    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        //Add space to create walls in.
        tiles = new TileType[columns + 4][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct height.
            tiles[i] = new TileType[rows + 4];
        }
    }


    void CreateRoomsAndCorridors()
    {
        // Create the rooms array with a random size.
        rooms = new Room[numRooms.Random];

        // There should be one less corridor than there is rooms.
        corridors = new Corridor[rooms.Length - 1];

        // Create the first room and corridor.
        rooms[0] = new Room();
        corridors[0] = new Corridor();

        // Setup the first room, there is no previous corridor so we do not use one.
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        // Setup the first corridor using the first room.
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            // Create a room.
            rooms[i] = new Room();

            // Setup the room based on the previous corridor.
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            // If we haven't reached the end of the corridors array...
            if (i < corridors.Length)
            {
                // ... create a corridor.
                corridors[i] = new Corridor();

                // Setup the corridor based on the room that was just created.
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            if (i == rooms.Length * .5f)
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                //Instantiate(player, playerPos, Quaternion.identity);
            }
        }

    }


    void SetTilesValuesForRooms()
    {
		bool entranceSet = false, exitSet = false;
        // Go through all the rooms...
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            // ... and for each room go through it's width.
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;
					if(i == 0 && j == currentRoom.roomWidth/2 && k == currentRoom.roomHeight/2 && !entranceSet){
						tiles[xCoord + 2][yCoord + 2] = TileType.Entrance;
						entranceSet = true;
					}else{
						if(i == rooms.Length-1 && j == currentRoom.roomWidth/2 && k == currentRoom.roomHeight/2 && !exitSet){
							tiles[xCoord + 2][yCoord + 2] = TileType.Exit;
							exitSet=true;
						}
					}
					if(!(tiles[xCoord + 2][yCoord + 2] == TileType.Entrance || tiles[xCoord + 2][yCoord + 2] == TileType.Exit)){
						tiles[xCoord + 2][yCoord + 2] = TileType.Floor;
					}
                    
                }
            }
        }
    }


    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];


            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                // Set the tile at these coordinates to Floor but check for Enterance or Exit
				if(!(tiles[xCoord + 2][yCoord + 2] == TileType.Entrance || tiles[xCoord + 2][yCoord + 2] == TileType.Exit)){
					tiles[xCoord + 2][yCoord + 2] = TileType.Floor;
				}
            }
        }
    }


    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                // ... and instantiate a floor tile for it.
                if (tiles[i][j] == TileType.Floor)
                {
                    InstantiateFromArray(floorTiles, i, j);
                    checkForWallsAround(i,j);
                }
				if(tiles[i][j] == TileType.Exit){
					InstantiateFromSingleObject(exit, i, j);
				}
				if(tiles[i][j] == TileType.Entrance){
                    entrance = InstantiateFromSingleObject(entrance, i, j);
                    player.transform.position = entrance.transform.position;
				}
            }
        }
    }

    void checkForWallsAround(int i, int j) {
        try {
            if (tiles[i - 1][j] == TileType.Void)
            {
                tiles[i - 1][j] = TileType.Wall;
                InstantiateFromArray(wallTiles, i - 1, j);
            }
            if (tiles[i - 1][j - 1] == TileType.Void)
            {
                tiles[i - 1][j - 1] = TileType.Wall;
                InstantiateFromArray(wallTiles, i - 1, j - 1);
            }
            if (tiles[i][j - 1] == TileType.Void)
            {
                tiles[i][j - 1] = TileType.Wall;
                InstantiateFromArray(wallTiles, i, j - 1);
            }
            if (tiles[i + 1][j] == TileType.Void)
            {
                tiles[i + 1][j] = TileType.Wall;
                InstantiateFromArray(wallTiles, i + 1, j);
            }
            if (tiles[i + 1][j + 1] == TileType.Void)
            {
                tiles[i + 1][j + 1] = TileType.Wall;
                InstantiateFromArray(wallTiles, i + 1, j + 1);
            }
            if (tiles[i][j + 1] == TileType.Void)
            {
                tiles[i][j + 1] = TileType.Wall;
                InstantiateFromArray(wallTiles, i, j + 1);
            }
            if (tiles[i - 1][j + 1] == TileType.Void)
            {
                tiles[i - 1][j + 1] = TileType.Wall;
                InstantiateFromArray(wallTiles, i - 1, j + 1);
            }
            if (tiles[i + 1][j - 1] == TileType.Void)
            {
                tiles[i + 1][j - 1] = TileType.Wall;
                InstantiateFromArray(wallTiles, i + 1, j - 1);
            }
        }
        catch (IndexOutOfRangeException) { }
    }

    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
                    InstantiateFromArray(wallTiles, xCoord, currentY);

        currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {

            InstantiateFromArray(outerWallTiles, currentX, yCoord);
            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = boardHolder.transform;
    }


    GameObject InstantiateFromSingleObject(GameObject prefab, float xCoord, float yCoord)
	{
		// The position to be instantiated at is based on the coordinates.
		Vector3 position = new Vector3(xCoord, yCoord, 0f);
		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = boardHolder.transform;
        return tileInstance;

	}
}