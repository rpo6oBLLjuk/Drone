using CustomInspector;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[Serializable]
public class GamepadVibrationController
{
    [HideField] public GamepadService gamepadService;

    [SerializeField] private float maxVibration;

    [SerializeField] private float defaultStrength;
    [SerializeField] private float defaultDuration;

    private CancellationTokenSource vibrationTokenSource = new();


    public void SetVibration()
    {
        SetGamepadMotorSpeeds(defaultStrength, defaultDuration);
    }

    public void SetVibration(float strength, float duration)
    {
        SetGamepadMotorSpeeds(strength, duration);
    }

    private void SetGamepadMotorSpeeds(float strength, float duration)
    {
        if (strength <= 0 || duration <= 0)
            return;

        gamepadService.Gamepad?.SetMotorSpeeds(strength, strength / 2);

        VibrationOffDelay(duration, vibrationTokenSource).RunWithCancellation(vibrationTokenSource);
    }

    private async UniTask VibrationOffDelay(float duration, CancellationTokenSource source)
    {
        await UniTask.WaitForSeconds(duration, cancellationToken: source.Token, ignoreTimeScale: true);

        if (gamepadService.Gamepad != null)
        {
            gamepadService.Gamepad.PauseHaptics();
            gamepadService.Gamepad.ResetHaptics();
        }

        Debug.Log("Вибрация завершена");
    }
}
