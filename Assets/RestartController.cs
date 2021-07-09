using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RestartController : MonoBehaviour
{

    // Update is called once per frame
    public void RestartGame() {
        Debug.Log("Stöff");
        PhotonNetwork.LoadLevel("StartScene");
    }

}
