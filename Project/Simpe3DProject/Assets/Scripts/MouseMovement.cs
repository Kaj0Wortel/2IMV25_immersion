using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour {

     public GameObject player;
     public float sensitivity = 1;
 
     void FixedUpdate()
     {
         //Debug.Log(Input.GetAxis("Mouse X"));
         float rotateHorizontal = Input.GetAxis("Mouse X");
         float rotateVertical = Input.GetAxis("Mouse Y");
         // Use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead
         // if you dont want the camera to rotate around the player
         transform.RotateAround(player.transform.position, -Vector3.up, rotateHorizontal * sensitivity);
         // Again, use transform.Rotate(transform.right * rotateVertical * sensitivity)
         // if you don't want the camera to rotate around the player
         transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity);
     }
}
