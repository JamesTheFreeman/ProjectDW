using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomPropLink : MonoBehaviour
{
    public GameObject roomProps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unloadProps()
    {
        roomProps.SetActive(false);
    }

    public void loadProps()
    {
        roomProps.SetActive(true);
    }
}
