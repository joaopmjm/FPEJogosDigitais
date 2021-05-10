using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static bool won = false;
    private static bool paused = false;
    public static bool Won
    {
        get
        {
            return won;
        }
        set
        {
            won = value;
        }
    }
    public static bool Paused
    {
        get
        {
            return paused;
        }
        set
        {
            paused = value;
        }
    }
}
