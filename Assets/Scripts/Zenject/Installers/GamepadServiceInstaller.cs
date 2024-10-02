using CustomInspector;
using UnityEngine;
using Zenject;

public class GamepadServiceInstaller : MonoInstaller
{
    [SerializeField, SelfFill(true)] private GamepadService gamepadService;
    public override void InstallBindings()
    {
        Container.Bind<GamepadService>().FromInstance(gamepadService).AsSingle();
    }
}