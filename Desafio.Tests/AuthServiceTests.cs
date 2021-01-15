using System;
using Desafio.Domain.UserAggregate;
using Moq;
using AutoMapper;
using Xunit;
using Desafio.Application.Authentication;
using Desafio.Application.Authentication.Dto;
using Desafio.Shared.Exception;
using System.Collections.Generic;
using Desafio.Shared.Dto;
using Desafio.Application;
using Desafio.Application.Authentication.Mapping;

namespace Desafio.Tests
{
    public class AuthServiceTests
    {
        public AuthService AuthService;
        public Mock<IUserRepository> UserRepository;
        public IMapper Mapper;

        public AuthServiceTests()
        {
            UserRepository = new Mock<IUserRepository>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new UserProfile()));
            Mapper = new Mapper(configuration);
            AuthService = new AuthService(UserRepository.Object, Mapper);
        }

        [Fact]
        public async void When_RegisterUserWithEmailAlreadyRegistered_Expect_ThrowBusinessException()
        {
            // Arrange
            var inputDto = new RegisterUserInputDto() { Name = "Dev", Email = "dev@global.com", Password = "123456#a" };
            var user = new User("Dev", "dev@global.com", "123456#a", new List<PhoneDto>());
            await AuthService.RegisterUser(inputDto);

            UserRepository.Setup(c => c.GetByEmail(inputDto.Email))
                .ReturnsAsync(user);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () => await AuthService.RegisterUser(inputDto));
            Assert.Equal("E-mail já existente", exception.Message);
        }

        [Fact]
        public async void When_RegisterNewUser_Expect_PersistToken()
        {
            // Arrange
            var inputDto = new RegisterUserInputDto() { Name = "Dev", Email = "dev@global.com", Password = "123456#a" };

            // Act
            var result = await AuthService.RegisterUser(inputDto);

            // Assert
            Assert.True(TokenService.ValidateToken(result.Token));
        }

        [Fact]
        public async void When_RegisterNewUser_Expect_ReturnId()
        {
            // Arrange
            var inputDto = new RegisterUserInputDto() { Name = "Dev", Email = "dev@global.com", Password = "123456#a" };

            // Act
            var result = await AuthService.RegisterUser(inputDto);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async void When_RegisterNewUser_Expect_ReturnCreatedDate()
        {
            // Arrange
            var inputDto = new RegisterUserInputDto() { Name = "Dev", Email = "dev@global.com", Password = "123456#a" };

            // Act
            var result = await AuthService.RegisterUser(inputDto);

            // Assert
            Assert.NotEqual(DateTime.MinValue, result.Created);
        }

        [Fact]
        public async void When_RegisterNewUser_Expect_LastLoginEqualsCreatedDate()
        {
            // Arrange
            var inputDto = new RegisterUserInputDto() { Name = "Dev", Email = "dev@global.com", Password = "123456#a" };

            // Act
            var result = await AuthService.RegisterUser(inputDto);

            // Assert
            Assert.Equal(result.Created, result.LastLogin);
        }

        [Fact]
        public async void When_LoginUserWithInexistentEmail_Expect_ThrowNotFoundException()
        {
            // Arrange
            var inputDto = new LoginUserInputDto() { Email = "dev@global.com", Password = "123456#a" };

            UserRepository.Setup(c => c.GetByEmail(inputDto.Email))
                .ReturnsAsync((User)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await AuthService.Login(inputDto));
            Assert.Equal("Usuário e/ou senha inválidos", exception.Message);
        }

        [Fact]
        public async void When_LoginUserWithWrongPassword_Expect_ThrowAuthenticationException()
        {
            // Arrange
            var inputDto = new LoginUserInputDto() { Email = "dev@global.com", Password = "123456#a" };
            var user = new User("Dev", "dev@global.com", "11111111", new List<PhoneDto>());

            UserRepository.Setup(c => c.GetByEmail(inputDto.Email))
                .ReturnsAsync(user);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AuthenticationException>(async () => await AuthService.Login(inputDto));
            Assert.Equal("Usuário e/ou senha inválidos", exception.Message);
        }

        [Fact]
        public async void When_LoginUser_Expect_UpdateLastLogin()
        {
            // Arrange
            var inputDto = new LoginUserInputDto() { Email = "dev@global.com", Password = "123456#a" };
            var user = new User("Dev", "dev@global.com", "123456#a", new List<PhoneDto>());
            var intialLastLogin = user.LastLogin;

            //Act
            UserRepository.Setup(c => c.GetByEmail(inputDto.Email))
                .ReturnsAsync(user);

            var result = await AuthService.Login(inputDto);

            //Assert
            Assert.True(intialLastLogin < result.LastLogin);
        }

        [Fact]
        public async void When_GetUserPassInvalidToken_Expect_ThrowAuthenticationException()
        {
            // Arrange
            var user = new User("Dev", "dev@global.com", "11111111", new List<PhoneDto>());
            var id = user.Id;

            var invalidToken = TokenService.GenerateToken(user) + "d";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AuthenticationException>(async () => await AuthService.GetById(id, invalidToken));
            Assert.Equal("Não autorizado", exception.Message);
        }

        [Fact]
        public async void When_GetUserPassValidButDiferentToken_Expect_ThrowAuthenticationException()
        {
            // Arrange
            var user = new User("Dev", "dev@global.com", "11111111", new List<PhoneDto>());
            var incorrectUser = new User("Suport", "suport@global.com", "13", new List<PhoneDto>());
            var id = user.Id;

            var incorrectToken = TokenService.GenerateToken(incorrectUser);

            UserRepository.Setup(c => c.GetById(id))
                .ReturnsAsync(user);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AuthenticationException>(async () => await AuthService.GetById(id, incorrectToken));
            Assert.Equal("Não autorizado", exception.Message);
        }

        [Fact]
        public async void When_GetUserPassIncorrectId_Expect_ThrowNotFoundException()
        {
            // Arrange
            var user = new User("Dev", "dev@global.com", "11111111", new List<PhoneDto>());
            var id = user.Id;

            var validToken = TokenService.GenerateToken(user);

            UserRepository.Setup(c => c.GetById(id))
                .ReturnsAsync((User)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await AuthService.GetById(id, validToken));
            Assert.Equal("Usuário não encontrado", exception.Message);
        }

        [Fact]
        public async void When_GetUser_Expect_ReturnUser()
        {
            // Arrange
            var user = new User("Dev", "dev@global.com", "11111111", new List<PhoneDto>());
            var id = user.Id;

            var validToken = TokenService.GenerateToken(user);
            user.UpdateToken(validToken);

            UserRepository.Setup(c => c.GetById(id))
                .ReturnsAsync(user);

            // Act
            var result = await AuthService.GetById(id, validToken);

            // Assert
            Assert.Equal(result.Id, user.Id);
        }


    }
}
