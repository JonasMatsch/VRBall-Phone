using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    private static int time;
    public static int Time
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
        }
    }
}
