using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using TestCreatorWebApp.Data.Converters.DAO;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Converters.DTO;
using TestCreatorWebApp.Data.Converters.DTO.Interfaces;

namespace TestCreatorWebApp.Bootstrappers
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
