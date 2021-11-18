using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Threading;

public class playerballcontroller : MonoBehaviour
{
    public float speed;

    public float time;
    private Rigidbody rb;

    private Transform position;


    public Text TimerPhone;


    private Vector3 prevRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = GetComponent<Transform>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (position.position.y < -20)
        {
            TimerPhone.text = "You won";
        }

        if( position.position.y < -50)
        {
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