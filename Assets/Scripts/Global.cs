using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static bool isGameStarted = false;
    public static int score = 0;
    public enum GameDirection
    {
        toLeft,
        toRight
    }

    public enum Sfx
    {
        Score,
        Basketboard,
        Ground,
    }

    public static GameDirection gameDir = GameDirection.toLeft;
}
