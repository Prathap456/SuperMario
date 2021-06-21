using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform playerTransform;
    public float offset;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //assigning cameras position to temp
        Vector3 temp = transform.position;
        
        //assigning player position x to temp x
        temp.x = playerTransform.position.x;
        

        //offset of camera
        temp.x += offset;
        

        //assigning changed temp value to original camera position to change the original cam position
        transform.position = temp;
    }
}
