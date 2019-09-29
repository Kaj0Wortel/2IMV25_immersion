using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // Horizontal trackpad
        if (Input.GetAxis("VIVE 0 trackpad horizontal") > 0.5f) {
            Debug.Log("trackpad 0 right (" + Input.GetAxis("VIVE 0 trackpad horizontal") + ")");
        } else if (Input.GetAxis("VIVE 0 trackpad horizontal") < -0.5f) {
            Debug.Log("trackpad 0 left (" + Input.GetAxis("VIVE 0 trackpad horizontal") + ")");
        }
        if (Input.GetAxis("VIVE 1 trackpad horizontal") > 0.5f) {
            Debug.Log("trackpad 1 right (" + Input.GetAxis("VIVE 1 trackpad horizontal") + ")");
        } else if (Input.GetAxis("VIVE 1 trackpad horizontal") < -0.5f) {
            Debug.Log("trackpad 1 left (" + Input.GetAxis("VIVE 1 trackpad horizontal") + ")");
        }

        // Vertical trackpad
        if (Input.GetAxis("VIVE 0 trackpad vertical") > 0.5f) {
            Debug.Log("trackpad 0 up (" + Input.GetAxis("VIVE 0 trackpad vertical") + ")");
        } else if (Input.GetAxis("VIVE 0 trackpad vertical") < -0.5f) {
            Debug.Log("trackpad 0 down (" + Input.GetAxis("VIVE 0 trackpad vertical") + ")");
        }
        if (Input.GetAxis("VIVE 1 trackpad vertical") > 0.5f) {
            Debug.Log("trackpad 1 up (" + Input.GetAxis("VIVE 1 trackpad vertical") + ")");
        } else if (Input.GetAxis("VIVE 1 trackpad vertical") < -0.5f) {
            Debug.Log("trackpad 1 down (" + Input.GetAxis("VIVE 1 trackpad vertical") + ")");
        }

        // Menu button
        if (Input.GetButtonDown("VIVE 0 menu button")) {
            Debug.Log("menu button 0");
        }
        if (Input.GetButtonDown("VIVE 1 menu button")) {
            Debug.Log("menu button 1");
        }

        // Trackpad button
        if (Input.GetButtonDown("VIVE 0 trackpad button")) {
            Debug.Log("trackpad button 0");
        }
        if (Input.GetButtonDown("VIVE 1 trackpad button")) {
            Debug.Log("trackpad button 1");
        }

        // Trackpad touch
        if (Input.GetButtonDown("VIVE 0 trackpad touch")) {
            Debug.Log("trackpad touch 0");
        }
        if (Input.GetButtonDown("VIVE 1 trackpad touch")) {
            Debug.Log("trackpad touch 1");
        }

        // Trigger touch
        if (Input.GetButtonDown("VIVE 0 trigger touch")) {
            Debug.Log("trigger touch 0");
        }
        if (Input.GetButtonDown("VIVE 1 trigger touch")) {
            Debug.Log("trigger touch 1");
        }

        // Trigger squeeze
        if (Input.GetAxis("VIVE 0 trigger squeeze") > 0f) {
            Debug.Log("trigger squeeze 0 (" + Input.GetAxis("VIVE 0 trigger squeeze") + ")");
        }
        if (Input.GetAxis("VIVE 1 trigger squeeze") > 0f) {
            Debug.Log("trigger squeeze 1 (" + Input.GetAxis("VIVE 1 trigger squeeze") + ")");
        }

        // Grip button
        if (Input.GetAxis("VIVE 0 grip button") > 0f) {
            Debug.Log("grip squeeze 0 (" + Input.GetAxis("VIVE 0 grip button") + ")");
        }
        if (Input.GetAxis("VIVE 1 grip button") > 0f) {
            Debug.Log("grip squeeze 1 (" + Input.GetAxis("VIVE 1 grip button") + ")");
        }
    }


}
