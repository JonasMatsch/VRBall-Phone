using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class playerballcontroller : MonoBehaviour
{
    // Appears in the Inspector view from where you can set the speed
    public float speed;

    public float time;
    // Rigidbody variable to hold the player ball's rigidbody instance
    private Rigidbody rb;


    //public OVRCameraRig Camera;

    public Text TimerPhone;


    private Vector3 prevRotation;

    // Called before the first frame update
    void Start()
    {
        // Assigns the player ball's rigidbody instance to the variable
        rb = GetComponent<Rigidbody>();
    }

    // Called once per frame
    private void Update()
    {
        // The float variables, moveHorizontal and moveVertical, holds the value of the virtual axes, X and Z.


            time += Time.deltaTime;

            
            TimerPhone.text = "Timer : " + String.Format("{0:0.00}", time);



    }

}