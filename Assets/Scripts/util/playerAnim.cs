using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    // private bool facingLeft;     // Boolean for facing direction
    public bool pushing;           // Boolean to disable turning
    private Transform playerT;      // Player transform
    private Animator animCtrl;      // Animation controller
    private Rigidbody2D rb;         // Rigid body
    private float adjSpeed;         // Adjusted speed

    [Tooltip("Player max walking speed")]
    public float speed = 1;         // Player speed
    [Tooltip("Player running speed")]
    public float runSpeed = 2;     // Player run speed

    // Start is called before the first frame update
    void Start()
    {
        // facingLeft = true;
        animCtrl = GetComponent<Animator>();
        playerT = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        pushing = false;
        adjSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        bool idle1 = true;  // Idle key 1
        bool idle2 = true;  // Idle key 2

        /*
            L/R Movement while key is held
            Input determines rotation direction
        */
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            // facingLeft = false;
            idle1 = false;
            if (!pushing) transform.localRotation = Quaternion.Euler(0, 180, 0);

            animCtrl.SetBool("moving", true);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            // facingLeft = true;
            idle1 = false;
            if (!pushing) transform.localRotation = Quaternion.Euler(0, 0, 0);

            animCtrl.SetBool("moving", true);
        }

        /*
            Moves the player as needed
            Separate from above code, but still works with it
        */
        move();

        /*
            Return to idle if neither direction is being traveled
            Requires both keys to be 'true'
            Implemented to avoid 'if else if else' chain
        */
        if (idle1 && idle2) animCtrl.SetBool("moving", false);
    }
    
    // Movement method for player
    private void move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * adjSpeed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    // Modify adjusted speed
    // Max speed * modifyer
    public void modSpeed(float mod)
    {
        adjSpeed = speed - (speed * mod);
    }
}
