using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    public float followDistance = 2f;
    public GameObject blackMask;

    private Transform _Player;
    private float smoothTime = 0.3f;
    private float xVelocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _Player = player.transform;
        transform.position = new Vector3(_Player.position.x, 0, -10);
    }

    private void FixedUpdate()
    {
        float distance = transform.position.x - _Player.position.x;
        if (Mathf.Abs(distance) > followDistance)
        {
            float newPositionX;
            if (distance < 0) newPositionX = Mathf.SmoothDamp(transform.position.x, _Player.position.x - followDistance, ref xVelocity, smoothTime);
            else newPositionX = Mathf.SmoothDamp(transform.position.x, _Player.position.x + followDistance, ref xVelocity, smoothTime);
            
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }
    }
}
