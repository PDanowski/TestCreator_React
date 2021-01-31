using Autofac;
using TestCreator.WebApp.Converters.DTO;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.Converters.ViewModel;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.Mappers;
using TestCreator.WebApp.Mappers.Interfaces;
using TestCreator.WebApp.Services;
using TestCreator.WebApp.Services.Interfaces;

namespace TestCreator.WebApp.Bootstrappers.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Mappers

            builder.RegisterType<TestAttemptViewModelMapper>().As<ITestAttemptViewModelMapper>();

            #endregion

            #region Converters

            builder.RegisterType<TestViewModelConverter>().As<ITestViewModelConverter>();
            builder.RegisterType<AnswerViewModelConverter>().As<IAnswerViewModelConverter>();
            builder.RegisterType<QuestionViewModelConverter>().As<IQuestionViewModelConverter>();
            builder.RegisterType<ResultViewModelConverter>().As<IResultViewModelConverter>();
            builder.RegisterType<UserViewModelConverter>().As<IUserViewModelConverter>();
            builder.RegisterType<TestAttemptAnswerViewModelConverter>().As<ITestAttemptAnswerViewModelConverter>();

            builder.RegisterType<ApplicationUserDtoConverter>().As<IApplicationUserDtoConverter>();
            builder.RegisterType<AnswerDtoConverter>().As<IAnswerDtoConverter>();
            builder.RegisterType<QuestionDtoConverter>().As<IQuestionDtoConverter>();
            builder.RegisterType<ResultDtoConverter>().As<IResultDtoConverter>();
            builder.RegisterType<TestDtoConverter>().As<ITestDtoConverter>();

            #endregion

            #region Services

            builder.RegisterType<TestResultCalculationService>().As<ITestResultCalculationService>();
            builder.RegisterType<TokenService>().As<ITokenService>();

            #endregion
        }
    }
}
