using CustomInspector;
using UnityEngine;
using Zenject;

public class UIServiceInstaller : MonoInstaller
{
    [SerializeField, SelfFill(true)] private UIService uIService;
    public override void InstallBindings()
    {
        Container.Bind<UIService>().FromInstance(uIService).AsSingle();
    }
}
