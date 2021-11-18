using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BallSpawnController : MonoBehaviour
{

    public int weight;

    private float time;

    public int spawnrate;

    public GameObject Enemy;


    private GameObject copy;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        int seconds = Math.Max(1,(int)Math.Round(time));

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float positionX = touch.position.x;
                float positionY = touch.position.y;

                Vector3 spawnPosition = new Vector3((positionX-950)/33.3f, 10, (positionY-520)/33.3f);

                if(copy == null)
                {
                    copy = PhotonNetwork.Instantiate("Enemy", spawnPosition, Quaternion.identity, 0);
                }
        }

        if(copy != null)
            {
            if (copy.transform.position.y < -20)
            {
                PhotonNetwork.Destroy(copy);
            }
        }
}
}
