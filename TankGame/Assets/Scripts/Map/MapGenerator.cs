using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MapGenerator : MonoBehaviour
{


    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50;
    public float roomHeight = 50;
    public Room[,] grid;
    public int mapSeed;
    public bool mapOfTheDay;
    public bool randomLevel;



    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        
        GenerateMap();
        rows = GameManager.instance.rows;
        cols = GameManager.instance.cols;
        mapSeed = GameManager.instance.mapSeed;
        mapOfTheDay = GameManager.instance.mapOfTheDay;
        randomLevel = GameManager.instance.randomLevel;
        StartCoroutine(GameManager.instance.SpawnPlayersCoroutine());
        
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Return a random room from array of prefabs
    /// </summary>
    /// <returns></returns>
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];

    }

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    [Obsolete]
    public void GenerateMap()
    {
        //clear out the grid
        grid = new Room[cols, rows];
        if(!mapOfTheDay)
        {
            UnityEngine.Random.seed = mapSeed;
        }
        else
        {
            UnityEngine.Random.seed = DateToInt(DateTime.Today);
        }
        if(randomLevel)
        {
            UnityEngine.Random.seed = DateToInt(DateTime.Now);

        }


        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for(int currentCol = 0; currentCol < cols; currentCol++)
            {

                #region Generation
                //figure out location

                float xPos = roomWidth * currentCol;
                float zPos = roomHeight * currentRow;
                Vector3 newPos = new Vector3(xPos, 0, zPos);

                //create map tile
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPos, Quaternion.identity) as GameObject;

                //set map tile parent
                tempRoomObj.transform.parent = this.transform;

                //give it a name
                tempRoomObj.name = "Room_" + currentCol + ", " + currentRow;

                // get room obj
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //save it to the grid array
                grid[currentCol, currentRow] = tempRoom;
                #endregion Generation

                #region Doors
                // open doors
                // if bottom row open north door
                if(currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);

                }

                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);

                }

                else if (currentCol == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                #endregion Doors







            }
        }
    }
}
