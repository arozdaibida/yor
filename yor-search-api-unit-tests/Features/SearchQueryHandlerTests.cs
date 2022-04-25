using FluentAssertions;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using yor_database_infrastructure.Specification;

using yor_search_api.Features.Search.Queries;
using yor_search_api.Features.Specifications;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Models;

namespace yor_search_api_unit_tests.Features
{
    public class SearchQueryHandlerTests
    {
        [Theory]
        [MemberData(nameof(GetConstructorData))]
        public void GivenNullParameter_WhenConstructing_ThenThrowsArgumentNullException(
            IRepository<User> userRepository,
            IRepository<Tag> tagRepository,
            string invalidParameter)
        {
            // Act
            Func<object> act = () => new SearchQueryHandler(userRepository, tagRepository);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>().WithParameterName(invalidParameter);
        }

        [Fact]
        public async void GivenQuery_WhenHandle_ThenTagRepositoryGetCalledOnce()
        {
            // Arrange
            var footballTag = new Tag
            {
                Name = "Football"
            };

            var bassketBallTag = new Tag
            {
                Name = "Basketball"
            };

            var tags = new List<Tag>
            {
                footballTag, bassketBallTag
            };

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Tags = new List<Tag> { footballTag },
                    City = "Poltava",
                    Country = "Ukraine",
                    DateOfBirth = new DateTime(2001, 3, 4),
                    Email = "email@gmail.com",
                    FirstName = "Name",
                    LastName = "Surname",
                    Gender = "male"
                }
            };


            var tagRepositoryMock = new Mock<IRepository<Tag>>();
            tagRepositoryMock
                .Setup(x => x.Get(It.IsAny<BaseSpecification<Tag>>()))
                .Returns(tags.AsQueryable());

            var userRepositoryMock = new Mock<IRepository<User>>();
            userRepositoryMock
                .Setup(x => x.Get(It.IsAny<BaseSpecification<User>>()))
                .Returns(users.AsQueryable());

            var handler = new SearchQueryHandler(
                userRepositoryMock.Object,
                tagRepositoryMock.Object);

            var query = new SearchQuery
            {
                Tags = new string[] { "Football", "Basketball" }
            };

            // Act
            await handler.Handle(query, default);

            // Assert
            tagRepositoryMock.Verify(x => x.Get(It.IsAny<BaseSpecification<Tag>>()), Times.Once);
        }

        [Fact]
        public async void GivenQuery_WhenHandle_ThenUserRepositoryGetCalledOnce()
        {
            // Arrange
            var footballTag = new Tag
            {
                Name = "Football"
            };

            var bassketBallTag = new Tag
            {
                Name = "Basketball"
            };

            var tags = new List<Tag>
            {
                footballTag, bassketBallTag
            };

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Tags = new List<Tag> { footballTag },
                    City = "Poltava",
                    Country = "Ukraine",
                    DateOfBirth = new DateTime(2001, 3, 4),
                    Email = "email@gmail.com",
                    FirstName = "Name",
                    LastName = "Surname",
                    Gender = "male"
                }
            };


            var tagRepositoryMock = new Mock<IRepository<Tag>>();
            tagRepositoryMock
                .Setup(x => x.Get(It.IsAny<BaseSpecification<Tag>>()))
                .Returns(tags.AsQueryable());

            var userRepositoryMock = new Mock<IRepository<User>>();
            userRepositoryMock
                .Setup(x => x.Get(It.IsAny<BaseSpecification<User>>()))
                .Returns(users.AsQueryable());

            var handler = new SearchQueryHandler(
                userRepositoryMock.Object,
                tagRepositoryMock.Object);

            var query = new SearchQuery
            {
                Tags = new string[] { "Football", "Basketball" }
            };

            // Act
            await handler.Handle(query, default);

            // Assert
            userRepositoryMock.Verify(x => x.Get(It.IsAny<SearchSpecification>()), Times.Once);
        }

        [Fact]
        public async void GivenQuery_WhenHandle_ThenReturnsNotNull()
        {
            // Arrange
            var footballTag = new Tag
            {
                Name = "Football"
            };

            var bassketBallTag = new Tag
            {
                Name = "Basketball"
            };

            var tags = new List<Tag>
            {
                footballTag, bassketBallTag
            };

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Tags = new List<Tag> { footballTag },
                    City = "Poltava",
                    Country = "Ukraine",
                    DateOfBirth = new DateTime(2001, 3, 4),
                    Email = "email@gmail.com",
                    FirstName = "Name",
                    LastName = "Surname",
                    Gender = "male"
                }
            };


            var tagRepositoryMock = new Mock<IRepository<Tag>>();
            tagRepositoryMock
                .Setup(x => x.Get(It.IsAny<BaseSpecification<Tag>>()))
                .Returns(tags.AsQueryable());

            var userRepositoryMock = new Mock<IRepository<User>>();
            userRepositoryMock
                .Setup(x => x.Get(It.IsAny<BaseSpecification<User>>()))
                .Returns(users.AsQueryable());

            var handler = new SearchQueryHandler(
                userRepositoryMock.Object,
                tagRepositoryMock.Object);

            var query = new SearchQuery
            {
                Tags = new string[] { "Football", "Basketball" }
            };

            // Act
            var actual = await handler.Handle(query, default);

            // Assert
            actual.Should().NotBeNull();
        }

        public static IEnumerable<object[]> GetConstructorData()
        {
            var userRepository = Mock.Of<IRepository<User>>();
            var tagRepository = Mock.Of<IRepository<Tag>>();

            var nullUserRepository = null as IRepository<User>;
            var nullTagRepository = null as IRepository<Tag>;

            yield return new object[]
            {
                nullUserRepository, tagRepository, nameof(userRepository)
            };

            yield return new object[]
            {
                userRepository, nullTagRepository, nameof(tagRepository)
            };
        }
    }
}
