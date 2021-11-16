using MessagesManager.Domain;
using MessagesManager.ExternalData;
using MessagesManager.Infrastructure.DependencyResolver;
using MessagesManager.Repository;
using MessagesManager.Services;
using MessagesManager.Services.BaseService;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MessagesManager
{
    public class Function
    {
        private readonly IManagerService _managerService;
        public Function()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            DependencyResolverConfigurator.Configure(serviceCollection);
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            _managerService = serviceProvider.GetService<IManagerService>();
        }
        public async Task<ResponseModel> Main(string authEncoded, string actionJson)
        {
            var actionModel = JsonConvert.DeserializeObject<ActionModel>(actionJson);
            var response = await _managerService.ProcessAsync(authEncoded, actionModel);

            Console.WriteLine(JsonConvert.SerializeObject(response));

            return response;
        }
    }
}
