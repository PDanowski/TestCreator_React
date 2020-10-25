using Autofac;
using TestCreator.Data.Commands.Handlers.Common.Interfaces;
using TestCreator.Data.Converters.DAO;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Converters.DTO;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Queries.Handlers.Common.Interfaces;
using TestCreator.WebApp.Data.Commands;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries;
using TestCreator.WebApp.Data.Queries.Interfaces;

namespace TestCreator.WebApp.Bootstrappers.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(IQueryHandler<,>).Assembly;

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

            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
        }
    }
}
