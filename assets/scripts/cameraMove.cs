using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {

    private float moveSpeed = 10f;

    private float leftRestriction = -7f;
    private float rightRestriction = 20f;
    private float upRestriction = 15f;
    private float downRestriction = -17.5f;
	
	
	
	void Update () {
		//left
        if((transform.position.x >=leftRestriction) && Input.mousePosition.x < 2)
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
        //right
        if((transform.position.x <= rightRestriction) && Input.mousePosition.x > Screen.width - 2)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        //up
        if ((transform.position.z <= upRestriction) && Input.mousePosition.y >= Screen.height - 2)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        //down
        if((transform.position.z >= downRestriction)&& Input.mousePosition.y < 2)
        {
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
        }
        
    }
}
