using System;
using UnityEngine;

public class GameStateController
{
    public static event Action GameStart;

    /// <summary>
    /// ������� ���������� ����, �������� �������� ��  ������  (true) � ��������� (false) ������
    /// </summary>
    public static event Action<bool> GameEnd;

    public static bool GameStarted { get; private set; }
    public static bool GameEnded { get; private set; }

    public static void Start()
    {
        {
            Debug.Log("������� GameStarted ��������");

            GameStarted = true;
            GameStart?.Invoke();

            GameEnded = false;

        }
    }

    public static void End(bool playerWin)
    {
        if (!GameEnded)
        {
            Debug.Log("������� GameEnded ��������");

            GameEnded = true;
            GameEnd?.Invoke(playerWin);

            GameStarted = false;
        }
    }
}
