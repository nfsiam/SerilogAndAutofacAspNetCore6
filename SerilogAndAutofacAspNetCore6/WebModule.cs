using Autofac;
using SerilogAndAutofacAspNetCore6.Services;

namespace SerilogAndAutofacAspNetCore6;
public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TestService>().AsSelf()
            .SingleInstance();
        base.Load(builder);
    }
}