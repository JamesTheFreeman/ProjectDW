using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bob : MonoBehaviour
{
    [Range(0.05f, 1.0f)]
    public float smoothTime = 0.3f;    
    [Range(0.005f, 0.5f)]
    public float dist = 0.2f;

    private float speed = 0.0f;
    private float startPos;
    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newPos;
        float rounded;
        if (up) 
        {
            newPos = Mathf.SmoothDamp(transform.localPosition.y, startPos + dist, ref speed, smoothTime);

            rounded = Mathf.Ceil(transform.localPosition.y * 100) / 100;
            if (rounded >= startPos + dist) up = false;
        }
        else
        {
            newPos = Mathf.SmoothDamp(transform.localPosition.y, startPos - dist, ref speed, smoothTime);

            rounded = Mathf.Floor(transform.localPosition.y * 100) / 100;
            if (rounded <= startPos - dist) up = true;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
    }
}
