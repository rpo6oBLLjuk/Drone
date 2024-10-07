using System;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static event Action GameStart;

    /// <summary>
    /// Событие завершения игры, аргумент отвечает за  победу  (true) и поражение (false) игрока
    /// </summary>
    public static event Action<bool> GameEnd;

    private static bool GameStarted = false;
    private static bool GameEnded = false;

    public static void Start()
    {
        GameEnded = false;
        if (!GameStarted)
        {
            GameStarted = true;
            GameStart?.Invoke();
        }
    }

    public static void End(bool playerWin)
    {
        if (!GameEnded)
        {
            GameEnded = true;
            GameEnd?.Invoke(playerWin);
        }
    }
}
