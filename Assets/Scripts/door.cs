using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class door : MonoBehaviour
{
    /*
        DESIGN
    */
    public GameObject arrow;    // Arrow indicator for player

    /*
        FUNCTIONALITY
    */
    public GameObject currRoom; // Room this door is located in
    public GameObject toDoor;   // Door to go to
    private GameObject toRoom;  // Room to go to
    private bool blocked;       // Whether the door is blocked
    private bool canMove;       // Whether the player can transition rooms
    Collider2D playerC;         // Players collider
    private int numObstr;       // Number of obstructions
    private SpriteRenderer cb;  // Camera behavior script attached to camera
    private Camera cam;         // Camera

    // Start is called before the first frame update
    void Start()
    {
        arrow.SetActive(false);
        blocked = false;
        playerC = null;

        toRoom = toDoor.GetComponent<door>().currRoom;                                              // Room to load derived from door
        cam = FindObjectOfType<Camera>();                                                           // Finds camera in scene, since there's only one
        cb = cam.GetComponent<CameraBehavior>().blackMask.GetComponent<SpriteRenderer>();           // Yikes
    }

    void Update() 
    {
        if (canMove && !blocked && playerC != null)
        {
            Traverse(playerC);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "obstruction":
                numObstr++;                                                                         // Add obstruction to counter
                blocked = true;                                                                     // Set door to blocked
                arrow.SetActive(false);                                                             // Disable arrow completely
                break;
            case "Player":
                playerC = other;
                canMove = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Player":
                arrow.SetActive(false);
                canMove = false;                                                                    // Disable arrow when player exits collider
                break;
            case "obstruction":
                numObstr--;                                                                         // Remove obstruction from counter
                if (numObstr == 0) blocked = false;                                                 // Unblock if number of obstructions = 0
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !blocked)
        {
            arrow.SetActive(true);    
        }
    }

    private void Traverse(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))                       // If the player pushes up
        {
            float playerY = -0.53f;                                                                 // Standard player y-coord
            GameObject player = other.gameObject;                                                   // Player gameObject

            /*
                ROOM TRANSITION BLOCK
                Surprisingly fast lol, thanks Unity
            */
            player.SetActive(false);                                                                // Disable player
            try{currRoom.GetComponent<roomPropLink>().unloadProps();}                               // Unload room props
            catch(NullReferenceException e){ var x = e; }                                           
            currRoom.SetActive(false);                                                              // Unload room

            toRoom.SetActive(true);                                                                 // Load new room
            try{toRoom.GetComponent<roomPropLink>().loadProps();}                                   // Load new room props
            catch(NullReferenceException e){ var x = e; } 

            player.transform.position =                                                             // Move player
                new Vector2(toDoor.transform.localPosition.x, playerY);                         
            cam.transform.position =                                                                // Correct camera position
                new Vector3(player.transform.position.x, cam.transform.position.y, -10);        
            player.GetComponent<playerAnim>().pushing = false;                                      // Correct potential anim. bug
            player.SetActive(true);                                                                 // Enable player
        }
    }
}
