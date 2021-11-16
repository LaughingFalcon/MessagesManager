using MessagesManager.Converters;
using MessagesManager.Domain;
using MessagesManager.Enums;
using MessagesManager.ExternalData;
using MessagesManager.Factories;
using MessagesManager.Repository;
using MessagesManager.Services;
using MessagesManager.Services.BaseService;
using MessagesManager.Services.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagesManagerTests.Services
{
    public class ManagerServiceTests
    {
        private readonly IUserBucket _userBucket;
        private readonly IAuthValidator _authValidator;
        private readonly IAuthService _authService;
        private readonly IMessageBucket _messageBucket;
        private readonly IToDeletMessage _toDeletMessage;
        private readonly IToNewMessage _toNewMessage;
        private readonly IToUpdateMessage _toUpdateMessage;
        private readonly IDictionary<MessageActionEnum, Func<IMessageService>> _messageService;
        private readonly IMessageFactory _messageFactory;

        private readonly Mock<IAuthService> _authServiceValid;
        private readonly Mock<IAuthService> _authServiceInvalid;

        private readonly Mock<IMessageFactory> _messageFactoryCreate;
        private readonly Mock<IMessageFactory> _messageFactoryNotCreate;
        private readonly Mock<IMessageFactory> _messageFactoryDelete;
        private readonly Mock<IMessageFactory> _messageFactoryNotDelete;
        private readonly Mock<IMessageFactory> _messageFactoryList;
        private readonly Mock<IMessageFactory> _messageFactoryEdited;
        private readonly Mock<IMessageFactory> _messageFactoryNotEdited;
        private readonly Mock<IMessageFactory> _messageFactoryRetriever;

        private readonly Mock<IMessageService> _createService;
        private readonly Mock<IMessageService> _createFalseService;
        private readonly Mock<IMessageService> _deleteService;
        private readonly Mock<IMessageService> _deleteFalseService;
        private readonly Mock<IMessageService> _listService;
        private readonly Mock<IMessageService> _editService;
        private readonly Mock<IMessageService> _editFalseService;
        private readonly Mock<IMessageService> _retrieverService;

        private readonly IManagerService _managerServiceUnauthorized;
        private readonly IManagerService _managerServiceCreated;
        private readonly IManagerService _managerServiceNotCreated;
        private readonly IManagerService _managerServiceDeleted;
        private readonly IManagerService _managerServiceNotDeleted;
        private readonly IManagerService _managerServiceListed;
        private readonly IManagerService _managerServiceEdited;
        private readonly IManagerService _managerServiceNotEdited;
        private readonly IManagerService _managerServiceRetriever;

        private readonly IManagerService _managerService;
        public ManagerServiceTests()
        {
            var responseTrue = new ResponseModel()
            {
                Data = true,
                Username = "username"
            };
            var responseFalse = new ResponseModel()
            {
                Data = false,
                Username = "username"
            };
            var responseList = new ResponseModel()
            {
                Data = new List<MessageModel>(),
                Username = "username"
            };
            var responseMessage = new ResponseModel()
            {
                Data = new MessageModel(),
                Username = "username"
            };

            #region SERVICES MOQ
            _createService = new Mock<IMessageService>(MockBehavior.Strict);
            _createService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseTrue);
            _createFalseService = new Mock<IMessageService>(MockBehavior.Strict);
            _createFalseService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseFalse);
            _deleteService = new Mock<IMessageService>(MockBehavior.Strict);
            _deleteService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseTrue);
            _deleteFalseService = new Mock<IMessageService>(MockBehavior.Strict);
            _deleteFalseService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseFalse);
            _listService = new Mock<IMessageService>(MockBehavior.Strict);
            _listService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseList);
            _editService = new Mock<IMessageService>(MockBehavior.Strict);
            _editService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseTrue);
            _editFalseService = new Mock<IMessageService>(MockBehavior.Strict);
            _editFalseService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseFalse);
            _retrieverService = new Mock<IMessageService>(MockBehavior.Strict);
            _retrieverService
                .Setup(x => x.ExecuteAsync(It.IsAny<ActionModel>(), It.IsAny<string>()))
                .ReturnsAsync(responseMessage);
            #endregion  
            
            #region AUTH SERVICE
            _authServiceValid = new Mock<IAuthService>(MockBehavior.Strict);
            _authServiceValid
                .Setup(x => x.ValidadeUserAsync(It.IsAny<UserModel>()))
                .ReturnsAsync(true);
            _authServiceInvalid = new Mock<IAuthService>(MockBehavior.Strict);
            _authServiceInvalid
                .Setup(x => x.ValidadeUserAsync(It.IsAny<UserModel>()))
                .ReturnsAsync(false);
            #endregion

            #region FACTORY
            _messageFactoryCreate = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryCreate
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_createService.Object);
            _messageFactoryNotCreate = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryNotCreate
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_createFalseService.Object);
            _messageFactoryDelete = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryDelete
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_deleteService.Object);
            _messageFactoryNotDelete = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryNotDelete
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_deleteFalseService.Object);
            _messageFactoryList = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryList
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_listService.Object);
            _messageFactoryEdited = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryEdited
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_editService.Object);
            _messageFactoryNotEdited = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryNotEdited
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_editFalseService.Object);
            _messageFactoryRetriever = new Mock<IMessageFactory>(MockBehavior.Strict);
            _messageFactoryRetriever
                .Setup(x => x.Create(It.IsAny<MessageActionEnum>()))
                .Returns(_retrieverService.Object);
            #endregion

            #region MANAGER SERVICE
            _managerServiceUnauthorized = new ManagerService(_authServiceInvalid.Object, _messageFactoryCreate.Object);
            _managerServiceCreated = new ManagerService(_authServiceValid.Object, _messageFactoryCreate.Object);
            _managerServiceNotCreated = new ManagerService(_authServiceValid.Object, _messageFactoryNotCreate.Object);
            _managerServiceDeleted = new ManagerService(_authServiceValid.Object, _messageFactoryDelete.Object);
            _managerServiceNotDeleted = new ManagerService(_authServiceValid.Object, _messageFactoryNotDelete.Object);
            _managerServiceListed = new ManagerService(_authServiceValid.Object, _messageFactoryList.Object);
            _managerServiceEdited = new ManagerService(_authServiceValid.Object, _messageFactoryEdited.Object);
            _managerServiceNotEdited = new ManagerService(_authServiceValid.Object, _messageFactoryNotEdited.Object);
            _managerServiceRetriever = new ManagerService(_authServiceValid.Object, _messageFactoryRetriever.Object);
            #endregion

            _userBucket = new UserBucket();
            _authValidator = new AuthValidator(_userBucket);
            _authService = new AuthService(_authValidator);
            _messageBucket = new MessageBucket();
            _toDeletMessage = new ToDeletMessage();
            _toNewMessage = new ToNewMessage();
            _toUpdateMessage = new ToUpdateMessage();
            _messageService = new Dictionary<MessageActionEnum, Func<IMessageService>>()
            {
                { MessageActionEnum.CREATE, () => new MessageCreatorService(_messageBucket, _toNewMessage)},
                { MessageActionEnum.DELETE, () => new MessageDeletorService(_messageBucket, _toDeletMessage)},
                { MessageActionEnum.EDIT, () => new MessageEditorService(_messageBucket, _toUpdateMessage)},
                { MessageActionEnum.LIST, () => new MessageListerService(_messageBucket)},
                { MessageActionEnum.RETRIEVE, () => new MessageRetrieverService(_messageBucket)}
            };
            _messageFactory = new MessageFactory(_messageService);
            _managerService = new ManagerService(_authService, _messageFactory);
        }

        [Fact]
        public async Task MyMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                MessageModel = new MessageModel() 
                {
                    Content = "Content",
                    ExpirationDate = "20/11/2021",
                    StartDate = "17/11/2021",
                    Subject = "Subject"
                },
                Action = MessageActionEnum.CREATE
            };

            var response = await _managerService.ProcessAsync(authBase64Encoded, actionModel);

            Assert.NotNull(response);
        }

        #region UNAUTHORIZED
        [Fact]
        public async Task UnauthorizedMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceUnauthorized.ProcessAsync(authBase64Encoded, actionModel);

            Assert.Null(response);
        }
        #endregion

        #region CREATE
        [Fact]
        public async Task CreateMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.CREATE,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceCreated.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<bool>(response.Data);
            Assert.True((bool)response.Data);
        }

        [Fact]
        public async Task NotCreateMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.CREATE,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceNotCreated.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<bool>(response.Data);
            Assert.False((bool)response.Data);
        }
        #endregion

        #region DELETE
        [Fact]
        public async Task DeleteMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.DELETE,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceDeleted.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<bool>(response.Data);
            Assert.True((bool)response.Data);
        }

        [Fact]
        public async Task NotDeleteMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.CREATE,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceNotDeleted.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<bool>(response.Data);
            Assert.False((bool)response.Data);
        }
        #endregion

        #region LIST
        [Fact]
        public async Task ListMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.LIST,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceListed.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<List<MessageModel>>(response.Data);
            Assert.NotNull((List<MessageModel>)response.Data);
        }
        #endregion

        #region EDIT
        [Fact]
        public async Task EditMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.EDIT,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceEdited.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<bool>(response.Data);
            Assert.True((bool)response.Data);
        }

        [Fact]
        public async Task NotEditMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.EDIT,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceNotEdited.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<bool>(response.Data);
            Assert.False((bool)response.Data);
        }
        #endregion

        #region RETRIEVER
        [Fact]
        public async Task RetrieverMessageTest()
        {
            var authBase64Encoded = "eyJ1c2VybmFtZSI6InVzZXJuYW1lIiwicGFzc3dvcmQiOiJwYXNzd29yZCIsInRva2VuIjoidG9rZW4ifQ==";
            var actionModel = new ActionModel()
            {
                Action = MessageActionEnum.RETRIEVE,
                MessageModel = new MessageModel()
            };

            var response = await _managerServiceRetriever.ProcessAsync(authBase64Encoded, actionModel);

            Assert.IsType<MessageModel>(response.Data);
            Assert.NotNull((MessageModel)response.Data);
        }
        #endregion
    
    }
}
