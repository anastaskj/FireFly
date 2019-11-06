using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    /*Rooms are based on an index
     Each index has a unique prefab attached to it
     Index 0 - LR
     Index 1 - LRD
     Index 2 - LRU
     Index 3 - LRDU*/
     
    public Transform startingPosition; //set to always start at the top middle of the map
    public GameObject startingRoom; //set to be a 4 exit room
    public GameObject[] rooms;  //array that contains all room template prefabs, which contain all the rooms for their type
    public GameObject[] endRooms; //contains the 3 possible end room prefabs with their special characteristics
    public float moveAmount; //the amount of spaces that will be moved when creating a new room (a constant 10 throughout)
    public float startTimeBtwRoom = 0.25f; //the time between creating a new room 
    public GameObject borders; //the object that controls the borders that confine the random generation
    public LayerMask room; //the layerMask that will be used to check the type of the room to accomodate for the creation algorithm
    public GameObject player; //the player character 

    private BorderController borderControls; //the script that contains the border numbers
    public GameObject lastRoom; //the last room that was created
    private GameObject secondLastRoom; //the second last room that was created
    /*These 2 game objects aim to work similar to a linked list, however the implementation is not final.
     They will be used later in order to determine what the last room type should be.
     Measuring the difference in positions will result in that.*/
    private float timeBtwRoom; //the Time.DeltaTime function that will be used with the startTimeBtwRoom variable
    private int direction; /*the direction that the next room will spawn in. There is a 1:5 chance of going down 
    unless there are no more spaces and 2:5 chance for either left or right spawns. The critical path will not go up.*/ 
    private int downCounter; //will be used to determine the vertical position of the generation

    // Start is called before the first frame update
    void Start()
    {
        borderControls = borders.GetComponent<BorderController>();
        transform.position = startingPosition.position;
        //first room is instantiated at start
        lastRoom = Instantiate(startingRoom, transform.position, Quaternion.identity);
        player.transform.position = this.transform.position + new Vector3(0f, 0f, 10f);
        //the direction variable is set to a range that covers all possibilities
        direction = Random.Range(1, 6);
    }

    private void Move()
    {
        if (direction== 1 || direction == 2) // Generate room on the right
        {
            if (transform.position.x < borderControls.maxX)
            {
                downCounter = 0;
                //generate the position of the new room by adding 10 spaces to the x position vector
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); //generate a random room type
                secondLastRoom = lastRoom; //save the room created last
                lastRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity); //make new room

                /*This will not allow the overlapping of rooms.
                  Example: When a room is generated, the direction that 
                  the new room will be at cannot go back.
                  Because we know that the room generated is to the right (1/2), 
                  the next room should not generate to the left (3/4)*/
                direction = Random.Range(1, 6); 
                if (direction == 3)
                    direction = 2;
                else if (direction == 4)
                    direction = 5;
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) // Generate room on the left
        {
            if (transform.position.x > borderControls.minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                secondLastRoom = lastRoom;
                lastRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5) // Generate room below
        {
            downCounter++;

            if (transform.position.y >borderControls.minY)
            {
                //this detects the type of room that needs to be created when going down
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter>=2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        secondLastRoom = lastRoom;
                        lastRoom = Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else //you can't make a room below if you've reached the border
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottom = Random.Range(1, 4);
                        if (randBottom == 2)
                            randBottom = 1;
                        secondLastRoom = lastRoom;
                        lastRoom = Instantiate(rooms[randBottom], transform.position, Quaternion.identity);
                    }
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 3);
                secondLastRoom = lastRoom;
                lastRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                //End level generation
                if (lastRoom != null)
                {
                    if (lastRoom.transform.position.x - secondLastRoom.transform.position.x == 10)
                    {   //last room came from the left
                        Destroy(lastRoom);
                        lastRoom = Instantiate(endRooms[0], transform.position, Quaternion.identity);
                    }
                    else if (lastRoom.transform.position.x - secondLastRoom.transform.position.x == -10)
                    {   //last room came from the right
                        Destroy(lastRoom);
                        lastRoom = Instantiate(endRooms[1], transform.position, Quaternion.identity);
                    }
                    else if (lastRoom.transform.position.y - secondLastRoom.transform.position.y == -10)
                    {   //last room came from above
                        Destroy(lastRoom);
                        lastRoom = Instantiate(endRooms[2], transform.position, Quaternion.identity);
                    }
                  
                }
                borderControls.genеrаtionCompletion = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && !borderControls.genеrаtionCompletion)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    
}
