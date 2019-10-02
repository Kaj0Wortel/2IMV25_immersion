using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        /* Oculus Rift */
        // A/B/X/Y buttons
        if (Input.GetButtonDown("Oculus 0 X button")) {
            Debug.Log("X 0 button");
        }
        if (Input.GetButtonDown("Oculus 1 A button")) {
            Debug.Log("A 1 button");
        }
        if (Input.GetButtonDown("Oculus 0 Y button")) {
            Debug.Log("Y 0 button");
        }
        if (Input.GetButtonDown("Oculus 1 B button")) {
            Debug.Log("B 1 button");
        }
        
        // Thumbstick buttons
        if (Input.GetButtonDown("Oculus 0 thumbstick button")) {
            Debug.Log("thumbstick 0 button");
        }
        if (Input.GetButtonDown("Oculus 1 thumbstick button")) {
            Debug.Log("thumbstick 1 button");
        }
        if (Input.GetButtonDown("Oculus 0 thumbstick touch")) {
            Debug.Log("thumbstick 0 touch");
        }
        if (Input.GetButtonDown("Oculus 1 thumbstick touch")) {
            Debug.Log("thumbstick 1 touch");
        }

        // Horizontal thumbsticks
        if (Input.GetAxis("Oculus 0 thumbstick horizontal") > 0.5f) {
            Debug.Log("thumbstick 0 right (" + Input.GetAxis("Oculus 0 thumbstick horizontal") + ")");
        } else if (Input.GetAxis("Oculus 0 thumbstick horizontal") < -0.5f) {
            Debug.Log("thumbstick 0 left (" + Input.GetAxis("Oculus 0 thumbstick horizontal") + ")");
        }
        if (Input.GetAxis("Oculus 1 thumbstick horizontal") > 0.5f) {
            Debug.Log("thumbstick 1 right (" + Input.GetAxis("Oculus 1 thumbstick horizontal") + ")");
        } else if (Input.GetAxis("Oculus 1 thumbstick horizontal") < -0.5f) {
            Debug.Log("thumbstick 1 left (" + Input.GetAxis("Oculus 1 thumbstick horizontal") + ")");
        }

        // Vertical thumbsticks
        if (Input.GetAxis("Oculus 0 thumbstick vertical") > 0.5f) {
            Debug.Log("thumbstick 0 up (" + Input.GetAxis("Oculus 0 thumbstick vertical") + ")");
        } else if (Input.GetAxis("Oculus 0 thumbstick vertical") < -0.5f) {
            Debug.Log("thumbstick 0 down (" + Input.GetAxis("Oculus 0 thumbstick vertical") + ")");
        }
        if (Input.GetAxis("Oculus 1 thumbstick vertical") > 0.5f) {
            Debug.Log("thumbstick 1 up (" + Input.GetAxis("Oculus 1 thumbstick vertical") + ")");
        } else if (Input.GetAxis("Oculus 1 thumbstick vertical") < -0.5f) {
            Debug.Log("thumbstick 1 down (" + Input.GetAxis("Oculus 1 thumbstick vertical") + ")");
        }

        // Trigger touch
        if (Input.GetButtonDown("Oculus 0 trigger touch")) {
            Debug.Log("trigger 0 touch");
        }
        if (Input.GetButtonDown("Oculus 1 trigger touch")) {
            Debug.Log("trigger 1 touch");
        }

        // Trigger squeeze
        if (Input.GetAxis("Oculus 0 trigger squeeze") > 0.5f) {
            Debug.Log("trigger 0 (" + Input.GetAxis("Oculus 0 trigger squeeze") + ")");
        }
        if (Input.GetAxis("Oculus 1 trigger squeeze") > 0.5f) {
            Debug.Log("trigger 1 (" + Input.GetAxis("Oculus 1 trigger squeeze") + ")");
        }

        // Grip squeeze
        if (Input.GetAxis("Oculus 0 grip squeeze") > 0.5f) {
            Debug.Log("grip 0 (" + Input.GetAxis("Oculus 0 grip squeeze") + ")");
        }
        if (Input.GetAxis("Oculus 1 grip squeeze") > 0.5f) {
            Debug.Log("grip 1 (" + Input.GetAxis("Oculus 1 grip squeeze") + ")");
        }

        // Thumb rest
        if (Input.GetButtonDown("Oculus 0 thumb rest touch")) {
            Debug.Log("thumb rest 0 touch");
        }
        if (Input.GetButtonDown("Oculus 1 thumb rest touch")) {
            Debug.Log("thumb rest 1 touch");
        }

        // Grip button
        if (Input.GetButtonDown("Oculus 0 grip button")) {
            Debug.Log("grip 0 button");
        }
        if (Input.GetButtonDown("Oculus 1 grip button")) {
            Debug.Log("grip 1 button");
        }
        
        // Menu button
        if (Input.GetButtonDown("Oculus 0 menu button")) {
            Debug.Log("menu 0 button");
        }

        // A/B/X/Y touch
        if (Input.GetButtonDown("Oculus 0 X touch")) {
            Debug.Log("X 0 touch");
        }
        if (Input.GetButtonDown("Oculus 1 A touch")) {
            Debug.Log("A 1 touch");
        }
        if (Input.GetButtonDown("Oculus 0 Y touch")) {
            Debug.Log("Y 0 touch");
        }
        if (Input.GetButtonDown("Oculus 1 B touch")) {
            Debug.Log("B 1 touch");
        }
        /*
        // Index finger grab (either 0 or 1)
        if (Input.GetAxis("Oculus 0 index finger grab") > 0.5f) {
            Debug.Log("Index finger 0 grab (" + Input.GetAxis("Oculus 0 index finger grab") + ")");
        }
        if (Input.GetAxis("Oculus 1 index finger grab") > 0.5f) {
            Debug.Log("Index finger 1 grab (" + Input.GetAxis("Oculus 1 index finger grab") + ")");
        }
        // Thumb grab (either 0 or 1)
        if (Input.GetAxis("Oculus 0 thumb grab") > 0.5f) {
            Debug.Log("Thumb 0 grab (" + Input.GetAxis("Oculus 0 thumb grab") + ")");
        }
        if (Input.GetAxis("Oculus 1 thumb grab") > 0.5f) {
            Debug.Log("Thumb 1 grab (" + Input.GetAxis("Oculus 1 thumb grab") + ")");
        }

        /**/
        /* VIVE *//*
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

        // Grip squeeze
        if (Input.GetAxis("VIVE 0 grip squeeze") > 0f) {
            Debug.Log("grip squeeze 0 (" + Input.GetAxis("VIVE 0 grip squeeze") + ")");
        }
        if (Input.GetAxis("VIVE 1 grip squeeze") > 0f) {
            Debug.Log("grip squeeze 1 (" + Input.GetAxis("VIVE 1 grip squeeze") + ")");
        }
        /**/
    }


}
