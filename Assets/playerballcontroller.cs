using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Threading;

public class playerballcontroller : MonoBehaviour
{
    // Appears in the Inspector view from where you can set the speed
    public float speed;

    public float time;
    // Rigidbody variable to hold the player ball's rigidbody instance
    private Rigidbody rb;

    private Transform position;

    //public OVRCameraRig Camera;

    public Text TimerPhone;


    private Vector3 prevRotation;

    // Called before the first frame update
    void Start()
    {
        // Assigns the player ball's rigidbody instance to the variable
        rb = GetComponent<Rigidbody>();
        position = GetComponent<Transform>();
    }

    // Called once per frame
    private void Update()
    {
        // The float variables, moveHorizontal and moveVertical, holds the value of the virtual axes, X and Z.
        time += Time.deltaTime;

        if (position.position.y < -20)
        {
            TimerPhone.text = "You won";
            Thread.Sleep(3000);
            switchToStart();
        }

        else if(time == 117)
        {
            TimerPhone.fontSize = 48;
        }

        else if(time < 120)
        {

            TimerPhone.text = "Timer : " + String.Format("{0:0.00}", time);
        }
        
        else if(time > 120)
        {
            TimerPhone.fontSize = 24;
            TimerPhone.text = "You lost";
            switchToStart();
        }
    }

    public void switchToStart()
    {
        PhotonNetwork.LoadLevel("StartScene");
        SceneManager.LoadScene("StartScene");
    }
}