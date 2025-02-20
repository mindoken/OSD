
using UnityEngine;
using Zenject;

namespace Game
{
    [CreateAssetMenu(
        fileName ="GameSystemInstaller",
        menuName ="Installers/New GameSystemInstaller")]
    public sealed class GameSystemInstaller : ScriptableObjectInstaller
    {
        private readonly Vector3 cameraOffset = new Vector3(0, 0, 10);
        public override void InstallBindings()
        {
            
            Container
                .BindInterfacesTo<MoveInput>() //TODO: ����������� ����� � ���������
                .AsSingle();

            Container
                .BindInterfacesTo<MoveController>() //TODO: ����������� ������ ��������� ��� NonLazy
                .AsSingle();

            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<CameraFollower>()
                .AsSingle()
                .WithArguments(cameraOffset);

        }
    }
}