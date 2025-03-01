
using Zenject;


namespace Game.Enemy.AI
{
    public sealed class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Install Conditions

            this.Container.Bind<IsDetect>().AsSingle();

            this.Container.Bind<SeePlayer>().AsSingle();

            this.Container.Bind<IsHuntingZone>().AsSingle();

            //Install Actions

            this.Container.Bind<Actions.SeekPlayer>().AsSingle();


            //Install States (Init States first)
            this.Container.BindInterfacesAndSelfTo<Hunting>().AsSingle(); //init <- InHunting

            this.Container.BindInterfacesAndSelfTo<Idle>().AsSingle(); //init <- Main

            this.Container.BindInterfacesAndSelfTo<InHunting>().AsSingle();

            this.Container.BindInterfacesAndSelfTo<SeekPlayer>().AsSingle();

            this.Container.BindInterfacesAndSelfTo<Back>().AsSingle();

            //Install Transitions

            this.Container.BindInterfacesAndSelfTo<IdleToInHunting>().AsSingle();

            this.Container.BindInterfacesAndSelfTo<HuntingToSeekPlayer>().AsSingle();

            this.Container.BindInterfacesAndSelfTo<SeekPlayerToHunting>().AsSingle();

            this.Container.BindInterfacesAndSelfTo<InHuntingToBack>().AsSingle();



            //Install Machine with initial State

            HierarchicalStateMachine mainMachine = new HierarchicalStateMachine(this.Container.Resolve<Idle>());

            this.Container.Bind<HierarchicalStateMachine>().FromInstance(mainMachine).AsSingle();
        }
    }

}