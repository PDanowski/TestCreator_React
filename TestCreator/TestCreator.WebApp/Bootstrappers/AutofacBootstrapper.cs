using Autofac;
using TestCreator.Data.Converters.DAO;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Converters.DTO;
using TestCreator.Data.Converters.DTO.Interfaces;

namespace TestCreator.WebApp.Bootstrappers
{
    public static class AutofacBootstrapper
    {
        public static void RegisterInfrastructure(this ContainerBuilder builder)
        {

        }

        public static void RegisterConverters(this ContainerBuilder builder)
        {

        }

        public static void RegisterDataConverters(this ContainerBuilder builder)
        {
            builder.RegisterType<TestDtoConverter>().As<ITestDtoConverter>();
            builder.RegisterType<AnswerDtoConverter>().As<IAnswerDtoConverter>();
            builder.RegisterType<QuestionDtoConverter>().As<IQuestionDtoConverter>();
            builder.RegisterType<TokenDtoConverter>().As<ITokenDtoConverter>();
            builder.RegisterType<ResultDtoConverter>().As<IResultDtoConverter>();
            builder.RegisterType<ApplicationUserDtoConverter>().As<IApplicationUserDtoConverter>();

            builder.RegisterType<TestConverter>().As<ITestConverter>();
            builder.RegisterType<AnswerConverter>().As<IAnswerConverter>();
            builder.RegisterType<QuestionConverter>().As<IQuestionConverter>();
            builder.RegisterType<TokenConverter>().As<ITokenConverter>();
            builder.RegisterType<ResultConverter>().As<IResultConverter>();
            builder.RegisterType<ApplicationUserConverter>().As<IApplicationUserConverter>();
        }

    }
}
