using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToMe : MonoBehaviour
{
    public GameObject target = null;
    public GameObject parent = null;
    public GameObject col;
    public float snapDistance = 0.1f;
    public float weight;
    [Tooltip("Weight / Weight Modifyer calculates % speed reduction")]
    [Range(100, 1000)]
    public float weightModifyer = 600f;

    private float xOffset;
    private float yOffset;
    private bool snapped;
    private Animator animCtrl;
    private Transform pTrans;
    private playerAnim pa;

    void Start() 
    {
        xOffset = transform.localPosition.x;
        yOffset = transform.localPosition.y;
        snapped = false;
        if (target != null && parent != null)
        {
            animCtrl = target.GetComponent<Animator>();
            pa = target.GetComponent<playerAnim>();
            pTrans = parent.transform;
        }
        else Debug.Log("Fix your damn code!");
    }

    void Update()
    {   
        if (target == null || parent == null) return;
        if (pa.pushing && !snapped) return; // Hopefully prevent other scripts from interfering

        if (Input.GetKey(KeyCode.Space))
        {
            snap();
        }
        else
        {
            snapped = false;
            pa.pushing = false;

            animCtrl.SetBool("pushing", false);
            pa.modSpeed(0);
            col.transform.parent = parent.transform;
        }
    }

    void snap()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= snapDistance) 
        {
            animCtrl.SetBool("pushing", true);

            // Snap player to object so object doesn't snap to player
            if (!snapped) target.transform.position = new Vector2(transform.position.x, target.transform.position.y);

            snapped = true;
            pa.pushing = true;
            pa.modSpeed(weight / weightModifyer);
            
            // Set player rotation to snap point rotation
            target.transform.localRotation = transform.localRotation;

            // Make snap point track player
            transform.position = new Vector2(target.transform.localPosition.x, 
                                             transform.position.y);

            // Make parent follow snap point
            pTrans.position = new Vector2(transform.position.x - xOffset,
                                          transform.position.y - yOffset);

            // Child the collider to the player
            col.transform.parent = target.transform;
        }
    }
}
