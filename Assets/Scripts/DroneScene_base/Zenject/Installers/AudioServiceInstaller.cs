using CustomInspector;
using UnityEngine;
using Zenject;

public class AudioServiceInstaller : MonoInstaller
{
    [SerializeField, SelfFill(true)] private AudioService audioService;
    public override void InstallBindings()
    {
        Container.Bind<AudioService>().FromInstance(audioService).AsSingle();
    }
}