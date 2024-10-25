using CustomInspector;
using UnityEngine;
using Zenject;

public class CheckpointServiceInstaller : MonoInstaller
{
    [SerializeField, SelfFill(true)] private CheckpointService checkpointService;
    public override void InstallBindings()
    {
        Container.Bind<CheckpointService>().FromInstance(checkpointService).AsSingle();
    }
}