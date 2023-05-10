using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.NUnitTests
{
    public class InMemoryDBContext
    {
        public static ChatAppDbContext? _chatAppDbContext;
        public static List<User> FakeUsers => _users;
        public static List<Message> FakeMessages => _messages;
        public static List<Avatar> FakeAvatars => _avatars;
        public static List<Chat> FakeChats => _chats;
        public static List<ChatInfo> FakeChatInfos => _chatInfos;
        public static List<ChatMember> FakeChatMembers => _chatMembers;

        public static async Task<ChatAppDbContext> GetChatAppDbContext()
        {
            if (_chatAppDbContext == null)
            {
                _chatAppDbContext = await SetUpDbContextAsync();
            }

            return _chatAppDbContext;
        }

        public static async Task<ChatAppDbContext> SetUpDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ChatAppDbContext>()
              .UseInMemoryDatabase(databaseName: new Guid().ToString())
              .Options;

            var context = new ChatAppDbContext(options);

            return await SeedDbContextDataAsync(context);
        }

        public static async Task<ChatAppDbContext> SeedDbContextDataAsync(ChatAppDbContext context)
        {
            await context.Users.AddRangeAsync(FakeUsers);
            await context.Messages.AddRangeAsync(FakeMessages); 
            await context.Avatars.AddRangeAsync(FakeAvatars);
            await context.Chats.AddRangeAsync(FakeChats);
            //await context.ChatInfo.AddRangeAsync(FakeChatInfos);
            await context.ChatMembers.AddRangeAsync(FakeChatMembers);

            await context.SaveChangesAsync();
            return context;
        }

        public static List<User> _users = new List<User>()
        {
            new User
            {
                Id = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                UserName = "user1",
                Email = "user1@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+380734636585",
            },
            new User
            {
                Id = "05786c29-074c-4499-ab29-21b6669c4f2b",
                UserName = "user2",
                Email = "user2@example.com",
                FirstName = "Jane",
                LastName = "Doe",
                PhoneNumber = "+380674111985",
            },
            new User
            {
                Id = "0782e029-6e0f-4dde-98d3-02b11c0cd088",
                UserName = "user3",
                Email = "user3@example.com",
                FirstName = "Bob",
                LastName = "Smith",
                PhoneNumber = "+380664000685",
            },
            new User
            {
                Id = "153f78cf-fa5b-482f-98da-bf912c19869e",
                UserName = "user4",
                Email = "user4@example.com",
                FirstName = "Alice",
                LastName = "Johnson",
                PhoneNumber = "+380668779985",
            },
            new User
            {
                Id = "17163417-86c3-4eaa-b429-986842b224b4",
                UserName = "user5",
                Email = "user5@example.com",
                FirstName = "Mike",
                LastName = "Davis",
                PhoneNumber = "+380994679985"
            },
            new User
            {
                Id = "1ca61d6f-3d8a-455b-bbe6-c3c43fdfaf41",
                UserName = "user6",
                Email = "user6@example.com",
                FirstName = "Sarah",
                LastName = "Wilson",
                PhoneNumber = "+380994679985"
            },
            new User
            {
                Id = "81697e33-465f-4a83-919d-c686ceeb66da",
                UserName = "user7",
                Email = "user7@example.com",
                FirstName = "David",
                LastName = "Anderson",
                PhoneNumber = "+380674679985"
            },
            new User
            {
                Id = "94e55362-8eaf-4000-b1da-437028554916",
                UserName = "user8",
                Email = "user8@example.com",
                FirstName = "Karen",
                LastName = "Thomas",
                PhoneNumber = "+380634679985"
            },
            new User
            {
                Id = "961d0281-c396-4cb0-8e87-fdf984ad5bf1",
                UserName = "user9",
                Email = "user9@example.com",
                FirstName = "Tom",
                LastName = "Jackson",
                PhoneNumber = "+380734679985"
            },
            new User
            {
                Id = "ac563701-4a80-435e-932f-6e999f77f3d8",
                UserName = "user10",
                Email = "user10@example.com",
                FirstName = "Lisa",
                LastName = "Parker",
                PhoneNumber = "+380634679555"
            },
            new User
            {
                Id = "bbeb2107-f786-4d66-b07e-7f026af3b303",
                UserName = "user11",
                Email = "user11@example.com",
                FirstName = "Sam",
                LastName = "Taylor",
                PhoneNumber = "+380674679985"
            },
            new User
            {
                Id = "bd0b5c02-5b83-4b22-a8d7-0b682bcb84e6",
                UserName = "user12",
                Email = "user12@example.com",
                FirstName = "Megan",
                LastName = "Harris",
                PhoneNumber = "+380674635985"
            },
            new User
            {
                Id = "dbbf7b3b-438d-46ad-9b6b-f25954c2cd28",
                UserName = "user13",
                Email = "user13@example.com",
                FirstName = "Peter",
                LastName = "Garcia",
                PhoneNumber = "+380734679685"
            },
            new User
            {
                Id = "e7646305-8540-43dd-96b7-d241ec4f62cc",
                UserName = "user14",
                Email = "user14@example.com",
                FirstName = "Merry",
                LastName = "Harrison",
                PhoneNumber = "+380674079985"
            },
            new User
            {
                Id = "ea2b14af-0d14-45c9-80af-2138090088c4",
                UserName = "user15",
                Email = "user15@example.com",
                FirstName = "Tom",
                LastName = "Smith",
                PhoneNumber = "+380734673385"
            }
        };

        public static List<Message> _messages = new List<Message>()
        {
            new Message
            {
                Id = 1,
                Text = "Hi there, how are you?",
                ChatId = 1,
                SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                SentTime = DateTime.UtcNow.AddHours(-1),
                MessageStatus = 1
            },
            new Message
            {
                Id = 2,
                Text = "I'm doing great, thanks for asking!",
                ChatId = 1,
                SenderId = "05786c29-074c-4499-ab29-21b6669c4f2b",
                SentTime = DateTime.UtcNow.AddMinutes(-45),
                MessageStatus = 1
            },
            new Message
            {
                Id = 3,
                Text = "Did you finish that report?",
                ChatId = 2,
                SenderId = "0782e029-6e0f-4dde-98d3-02b11c0cd088",
                SentTime = DateTime.UtcNow.AddDays(-1),
                MessageStatus = 2
            },
            new Message
            {
                Id = 4,
                Text = "Not yet, but I'll get it done today.",
                ChatId = 2,
                SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                SentTime = DateTime.UtcNow.AddHours(-5),
                MessageStatus = 1
            },
            new Message
            {
                Id = 5,
                Text = "Can you pick me up at the airport tomorrow?",
                ChatId = 3,
                SenderId = "05786c29-074c-4499-ab29-21b6669c4f2b",
                SentTime = DateTime.UtcNow.AddHours(-12),
                MessageStatus = 1
            },
            new Message
            {
                Id = 6,
                Text = "Sure thing, what time is your flight?",
                ChatId = 3,
                SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                SentTime = DateTime.UtcNow.AddHours(-10),
                MessageStatus = 1
            },
            new Message
            {
                Id = 7,
                Text = "Do you want to grab lunch later?",
                ChatId = 4,
                SenderId = "0782e029-6e0f-4dde-98d3-02b11c0cd088",
                SentTime = DateTime.UtcNow.AddHours(-3),
                MessageStatus = 1
            },
            new Message
            {
                Id = 8,
                Text = "Sorry, I have a meeting then.",
                ChatId = 4,
                SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                SentTime = DateTime.UtcNow.AddHours(-1),
                MessageStatus = 1
            },
            new Message
            {
                Id = 9,
                Text = "Can you help me move this weekend?",
                ChatId = 5,
                SenderId = "153f78cf-fa5b-482f-98da-bf912c19869e",
                SentTime = DateTime.UtcNow.AddDays(-3),
                MessageStatus = 1
            },
            new Message
            {
                Id = 10,
                Text = "I'd love to, but I'm out of town.",
                ChatId = 5,
                SenderId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                SentTime = DateTime.UtcNow.AddHours(-24),
                MessageStatus = 1
            },
            new Message
            {
                Id = 11,
                Text = "What did you think of the movie?",
                ChatId = 6,
                SenderId = "05786c29-074c-4499-ab29-21b6669c4f2b",
                SentTime = DateTime.UtcNow.AddDays(-2),
                MessageStatus = 1
            }
        };

        public static List<Avatar> _avatars = new List<Avatar>()
        {
            new Avatar
            {
                Id = 1,
                ChatId = 1,
                UserId = null,
                FileName = "file",
                Content = "avatar1",
                ContentType = "jpg"
            },
            new Avatar
            {
                Id = 2,
                ChatId = 2,
                UserId = null,
                FileName = "file",
                Content = "avatar2",
                ContentType = "jpg"
            },
            new Avatar
            {
                Id = 3,
                ChatId = 2,
                UserId = null,
                FileName = "file",
                Content = "avatar3",
                ContentType = "jpg"
            },
            new Avatar
            {
                Id = 4,
                ChatId = null,
                UserId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                FileName = "file",
                Content = "avatar4",
                ContentType = "jpg"
            },
            new Avatar
            {
                Id = 5,
                ChatId = null,
                UserId = "02889dcc-31a8-4457-a08b-8ae2c0ffdec6",
                FileName = "file",
                Content = "avatar5",
                ContentType = "jpg"
            },
            new Avatar
            {
                Id = 6,
                ChatId = null,
                UserId = "05786c29-074c-4499-ab29-21b6669c4f2b",
                FileName = "file",
                Content = "avatar6",
                ContentType = "jpg"
            },
            new Avatar
            {
                Id = 7,
                ChatId = null,
                UserId = "0782e029-6e0f-4dde-98d3-02b11c0cd088",
                FileName = "file",
                Content = "avatar7",
                ContentType = "jpg"
            },
        };

        public static List<Chat> _chats = new List<Chat>()
        {
            new Chat { Id = 1, Name = "General", Description = "General chat for everyone", MembersCount = 10 },
            new Chat { Id = 2, Name = "Marketing", Description = "Marketing department chat", MembersCount = 5 },
            new Chat { Id = 3, Name = "Engineering", Description = "Engineering department chat", MembersCount = 8 },
            new Chat { Id = 4, Name = "Sales", Description = "Sales department chat", MembersCount = 3 },
            new Chat { Id = 5, Name = "Support", Description = "Customer support chat", MembersCount = 7 },
            new Chat { Id = 6, Name = "Design", Description = "Design department chat", MembersCount = 4 },
        };

        public static List<ChatInfo> _chatInfos = new List<ChatInfo>()
        {
            new ChatInfo { ChatId = 1, Name = "General", Description = "General chat for everyone", MembersCount = 10 },
            new ChatInfo { ChatId = 2, Name = "Marketing", Description = "Marketing department chat", MembersCount = 5 },
            new ChatInfo { ChatId = 3, Name = "Engineering", Description = "Engineering department chat", MembersCount = 8 },
            new ChatInfo { ChatId = 4, Name = "Sales", Description = "Sales department chat", MembersCount = 3 },
            new ChatInfo { ChatId = 5, Name = "Support", Description = "Customer support chat", MembersCount = 7 },
            new ChatInfo { ChatId = 6, Name = "Design", Description = "Design department chat", MembersCount = 4 },
        };

        public static List<ChatMember> _chatMembers = new List<ChatMember>()
        {
            new ChatMember { Id = 1, ChatId = 1, UserId = "user1" },
            new ChatMember { Id = 2, ChatId = 1, UserId = "user2" },
            new ChatMember { Id = 3, ChatId = 1, UserId = "user3" },
            new ChatMember { Id = 4, ChatId = 2, UserId = "user2" },
            new ChatMember { Id = 5, ChatId = 2, UserId = "user4" },
            new ChatMember { Id = 6, ChatId = 3, UserId = "user1" },
            new ChatMember { Id = 7, ChatId = 3, UserId = "user5" },
            new ChatMember { Id = 8, ChatId = 3, UserId = "user6" }
        };
    }
}
