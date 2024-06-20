using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerControls>().AsSingle();
    }
}
