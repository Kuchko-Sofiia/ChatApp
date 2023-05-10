using ChatApp.BLL.Services;
using ChatApp.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApp.NUnitTests.Services
{
    [TestFixture]
    public class JwtTokenServiceTests
    {
        private Mock<IConfigurationRoot> _configurationMock;
        private JwtTokenService _jwtTokenService;

        [SetUp]
        public void SetUp()
        {
            _configurationMock = new Mock<IConfigurationRoot>();
            _configurationMock.Setup(x => x["Jwt:Issuer"]).Returns("testissuer");
            _configurationMock.Setup(x => x["Jwt:Audience"]).Returns("testaudience");
            _configurationMock.Setup(x => x["Jwt:Key"]).Returns("chatapp_secret_key");
            _configurationMock.Setup(x => x.GetSection("Jwt:Expire").Value).Returns("30");
            _configurationMock.Setup(x => x.GetSection("Jwt:TokenValidityInMinutes").Value).Returns("60");
            _configurationMock.Setup(x => x.GetSection("Jwt:RefreshTokenValidityInDays").Value).Returns("7");
            _configurationMock.Setup(x => x["Jwt:Secret"]).Returns("chatapp_secret_key");

            _jwtTokenService = new JwtTokenService(_configurationMock.Object);
        }

        [Test]
        public void CreateJwtToken_ReturnsValidToken()
        {
            var user = new User
            {
                Id = "1",
                Email = "test@example.com"
            };

            var token = _jwtTokenService.CreateJwtToken(user);

            Assert.That(token, Is.Not.Null);
            Assert.That(token.Issuer, Is.EqualTo("testissuer"));
            Assert.That(token.Claims.Count, Is.EqualTo(8));
        }

        [Test]
        public void CreateToken_ReturnsValidTokenString()
        {
            var user = new User
            {
                Id = "1",
                Email = "test@example.com"
            };

            var tokenString = _jwtTokenService.CreateToken(user);

            Assert.That(tokenString, Is.Not.Null);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenString);
            Assert.That(token.Issuer, Is.EqualTo("testissuer"));
            Assert.That(token.Claims.Count, Is.EqualTo(8));
        }

        [Test]
        public void CreateRefreshToken_ReturnsValidTokenString()
        {
            var tokenString = _jwtTokenService.CreateRefreshToken();

            Assert.That(tokenString, Is.Not.Null);
            Assert.That(tokenString.Length, Is.EqualTo(88)); // base64 encoded length of 64 random bytes
        }
    }
}
