using NUnit.Framework;
using ChatApp.BLL.Services;
using ChatApp.DAL.UnitOfWork;
using ChatApp.DTO;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;

namespace ChatApp.NUnitTests.Services
{
    [TestFixture]
    public class ChatServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IChatRepository> _chatRepositoryMock;
        private Mock<IChatMemberRepository> _chatMemberRepositoryMock;
        private ChatService _chatService;

        [SetUp]
        public void SetUp()
        {
            _chatRepositoryMock = new Mock<IChatRepository>();
            _chatMemberRepositoryMock = new Mock<IChatMemberRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uow => uow.GetRepository<IChatRepository>()).Returns(_chatRepositoryMock.Object);
            _chatService = new ChatService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task GetChatById_ShouldReturnChat()
        {
            var chatId = 1123;
            var chat = new Chat { Id = chatId, Name = "Test Chat", Description = "Test Description" };
            _chatRepositoryMock.Setup(repo => repo.GetById(chatId)).ReturnsAsync(chat);

            var result = await _chatService.GetChatById(chatId);

            Assert.That(result, Is.EqualTo(chat));
            _chatRepositoryMock.Verify(repo => repo.GetById(chatId), Times.Once);
        }
    }
}
