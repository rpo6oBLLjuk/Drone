using System;
using UnityEngine;

public class GameStateController
{
    public static event Action GameStart;

    /// <summary>
    /// Событие завершения игры, аргумент отвечает за  победу  (true) и поражение (false) игрока
    /// </summary>
    public static event Action<bool> GameEnd;

    public static bool GameStarted { get; private set; }
    public static bool GameEnded { get; private set; }

    public static void Start()
    {
        {
            Debug.Log("Условие GameStarted пройдено");

            GameStarted = true;
            GameStart?.Invoke();

            GameEnded = false;

        }
    }

    public static void End(bool playerWin)
    {
        if (!GameEnded)
        {
            Debug.Log("Условие GameEnded пройдено");

            GameEnded = true;
            GameEnd?.Invoke(playerWin);

            GameStarted = false;
        }
    }
}
