using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[Serializable]
public class GamepadVibrationController
{
    public GamepadService gamepadService;

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
        gamepadService.Gamepad.SetMotorSpeeds(strength, strength);

        VibrationOffDelay(duration, vibrationTokenSource).RunWithCancellation(vibrationTokenSource);
    }

    private async UniTask VibrationOffDelay(float duration, CancellationTokenSource source)
    {
        await UniTask.WaitForSeconds(duration, cancellationToken: source.Token);

        gamepadService.Gamepad.PauseHaptics();
        gamepadService.Gamepad.ResetHaptics();
        
        Debug.Log("Вибрация завершена");
    }
}
