using Player;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //register monobehaviors as a interface
        builder.RegisterComponentInHierarchy<PlayerController>()
            .AsImplementedInterfaces();
        builder.RegisterComponentInHierarchy<AniationSwitchController>();
        builder.RegisterComponentInHierarchy<Weapon>()
            .AsImplementedInterfaces();
        builder.RegisterEntryPoint<PlayerStateSwitchSystem>();
    }
}
