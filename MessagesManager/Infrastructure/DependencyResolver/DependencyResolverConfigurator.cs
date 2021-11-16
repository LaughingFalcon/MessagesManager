using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Infrastructure.DependencyResolver
{
    public static class DependencyResolverConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(provider =>
                new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build()
            );
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

            services.AddSingleton<ExternalData.IMessageBucket, ExternalData.MessageBucket>();
            services.AddSingleton<ExternalData.IUserBucket, ExternalData.UserBucket>();

            services.AddTransient<Converters.IToDeletMessage, Converters.ToDeletMessage>();
            services.AddTransient<Converters.IToNewMessage, Converters.ToNewMessage>();
            services.AddTransient<Converters.IToUpdateMessage, Converters.ToUpdateMessage>();

            services.AddSingleton<Repository.IAuthValidator, Repository.AuthValidator>();

            services.AddSingleton<Factories.IMessageFactory, Factories.MessageFactory>(provider =>
                new Factories.MessageFactory(
                    new Dictionary<Enums.MessageActionEnum, Func<Services.Implementations.IMessageService>>()
                    {
                        { Enums.MessageActionEnum.CREATE, () => provider.GetService<Services.Implementations.IMessageCreatorService>() },
                        { Enums.MessageActionEnum.DELETE, () => provider.GetService<Services.Implementations.IMessageDeletorService>() },
                        { Enums.MessageActionEnum.EDIT, () => provider.GetService<Services.Implementations.IMessageEditorService>() },
                        { Enums.MessageActionEnum.LIST, () => provider.GetService<Services.Implementations.IMessageListerService>() },
                        { Enums.MessageActionEnum.RETRIEVE, () => provider.GetService<Services.Implementations.IMessageRetrieverService>() }
                    }
                )
            );
            services.AddTransient<Services.Implementations.IMessageCreatorService, Services.Implementations.MessageCreatorService>();
            services.AddTransient<Services.Implementations.IMessageDeletorService, Services.Implementations.MessageDeletorService>();
            services.AddTransient<Services.Implementations.IMessageEditorService, Services.Implementations.MessageEditorService>();
            services.AddTransient<Services.Implementations.IMessageListerService, Services.Implementations.MessageListerService>();
            services.AddTransient<Services.Implementations.IMessageRetrieverService, Services.Implementations.MessageRetrieverService>();

            services.AddSingleton<Services.IAuthService, Services.AuthService>();
            services.AddSingleton<Services.BaseService.IManagerService, Services.BaseService.ManagerService>();

        }
    }
}
