using Autofac;
using TestCreator.WebApp.Bootstrappers.Modules;

namespace TestCreator.WebApp.Bootstrappers
{
    public static class AutofacBootstrapper
    {
        public static void RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DataModule>();
        }
    }
}
