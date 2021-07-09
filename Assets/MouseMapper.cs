using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMapper : MonoBehaviour
{

    public GameObject Controller;

    public void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Console.WriteLine("stöffff  " + mouse);
        Controller.transform.Translate(mouse);
    }
}
