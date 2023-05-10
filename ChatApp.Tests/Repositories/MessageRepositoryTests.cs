using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Realizations;
using NUnit.Framework.Internal;
using System.Linq.Expressions;

namespace ChatApp.NUnitTests.Repositories
{
    public class MessageRepositoryTests
    {
        private MessageRepository _messageRepository;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var context = await InMemoryDBContext.GetChatAppDbContext();
            _messageRepository = new MessageRepository(context);
        }

        [Test]
        public async Task GetById_ExistingId_ReturnsMessage()
        {
            var id = 1;

            var result = await _messageRepository.GetById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<Message>());
                Assert.That(result.Id, Is.EqualTo(id));
            });
        }

        [Test]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            var id = 99;

            var result = await _messageRepository.GetById(id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAll_ReturnsAllMessages()
        {
            var result = _messageRepository.GetAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IQueryable<Message>>());
            Assert.That(result.Count(), Is.EqualTo(InMemoryDBContext.FakeMessages.Count));
        }

        [Test]
        public void GetAllSorted_ReturnsSortedMessages()
        {
            var orderBy = (Expression<Func<Message, object>>)(m => m.SentTime);

            var result = _messageRepository.GetAllSorted(orderBy);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<Message>>());
            Assert.That(result.Count(), Is.EqualTo(InMemoryDBContext.FakeMessages.Count));
            Assert.That(result.SequenceEqual(result.OrderBy(orderBy.Compile())), Is.True);
        }

        [Test]
        public void FindAll_WithPredicate_ReturnsFilteredMessages()
        {
            var predicate = (Expression<Func<Message, bool>>)(m => m.MessageStatus == 1);

            var result = _messageRepository.FindAll(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<Message>>());
            Assert.That(result.Count(), Is.EqualTo(InMemoryDBContext.FakeMessages.Count(m => m.MessageStatus == 1)));
            Assert.That(result.All(m => m.MessageStatus == 1), Is.True);
        }

        [Test]
        public void FindAll_WithoutPredicate_ReturnsAllMessages()
        {
            var result = _messageRepository.FindAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<Message>>());
            Assert.That(result.Count(), Is.EqualTo(InMemoryDBContext.FakeMessages.Count));
        }

        [Test]
        public async Task Create_AddsNewMessage()
        {
            var message = new Message
            {
                Id = 99999,
                Text = "Test message",
                ChatId = 1,
                SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                SentTime = DateTime.UtcNow,
                MessageStatus = 1
            };

            _messageRepository.Create(message);
            var result = await _messageRepository.GetById(message.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Message>());
            Assert.That(result.Id, Is.EqualTo(message.Id));
            Assert.That(result.Text, Is.EqualTo(message.Text));
            Assert.That(result.ChatId, Is.EqualTo(message.ChatId));
            Assert.That(result.SenderId, Is.EqualTo(message.SenderId));
            Assert.That(result.SentTime, Is.EqualTo(message.SentTime));
            Assert.That(result.MessageStatus, Is.EqualTo(message.MessageStatus));
        }

        [Test]
        public async Task CreateMultiple_AddsEntitiesToDatabase()
        {
            var messagesToAdd = new List<Message>
            {
                new Message
                {
                    Id = 998,
                    Text = "Hello, I am fine. How about you?",
                    ChatId = 1,
                    SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                    SentTime = DateTime.UtcNow.AddHours(-2),
                    MessageStatus = 1
                },
                new Message
                {
                    Id = 997,
                    Text = "I am doing great, thanks for asking!",
                    ChatId = 1,
                    SenderId = "3a73a9a8-2f08-4ec8-9981-6a982b32e58b",
                    SentTime = DateTime.UtcNow.AddHours(-1),
                    MessageStatus = 1
                }
            };

            _messageRepository.CreateMultiple(messagesToAdd);

            foreach (var message in messagesToAdd)
            {
                var result = await _messageRepository.GetById(message.Id);

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<Message>());
                Assert.That(result.Id, Is.EqualTo(message.Id));
            }
        }

        [Test]
        public async Task Update_ModifiesExistingEntity()
        {
            var message = new Message
            {
                Id = 875,
                Text = "Hello, world!",
                ChatId = 1,
                SenderId = "user1",
                SentTime = DateTime.UtcNow,
                MessageStatus = 1
            };
            _messageRepository.Create(message);

            message.Text = "Updated message";
            _messageRepository.Update(message);

            var updatedMessage = await _messageRepository.GetById(message.Id);
            Assert.That(updatedMessage, Is.Not.Null);
            Assert.That(updatedMessage.Text, Is.EqualTo(message.Text));
        }

        [Test]
        [TestCase(2,2)]
        public void GetAllByChatId_ReturnsCorrectMessages(int chatId, int messagesCount)
        {
            var result = _messageRepository.GetAllByChatId(chatId);

            Assert.That(result.Count(), Is.EqualTo(messagesCount));
            Assert.That(result.All(m => m.ChatId == chatId), Is.True);
        }
    }
}