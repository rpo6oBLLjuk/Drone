using CustomInspector;
using UnityEngine;
using Zenject;

public class CameraServiceInstaller : MonoInstaller
{
    [SerializeField, SelfFill(true)] private CameraService cameraService;
    public override void InstallBindings()
    {
        Container.Bind<CameraService>().FromInstance(cameraService).AsSingle();
    }
}