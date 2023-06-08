using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.Tests.Common.Mocks;
using Xunit;

namespace OOD_Project_Backend.Tests.Core.DataAccess
{
    public class BaseRepositoryTests
    {
        private readonly IBaseRepository<MockEntity> _sut;
        private readonly AppDbContext _appDbContext;

        public BaseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql()
                .Options;
            _appDbContext = Substitute.For<AppDbContext>(options);
            _sut = new BaseRepository<MockEntity>(_appDbContext);
        }



        [Fact]
        public async Task Create_ShouldReceiveCall_WhenEntityWasPassedToTheMethodAsync()
        {

            // Arrange
            var mockEntity = new MockEntity()
            {
                Id = 1,
                Name = "Foo"
            };

            // Act
            await _sut.Create(mockEntity);

            // Assert
            await _appDbContext.Set<MockEntity>().Received(1).AddAsync(mockEntity);
        }

        [Fact]
        public void Update_ShouldReceiveCall_WhenEntityWasPassedToTheMethod()
        {

            // Arrange
            var mockEntity = new MockEntity()
            {
                Id = 1,
                Name = "Foo"
            };

            // Act
            _sut.Update(mockEntity);

            // Assert
            _appDbContext.Set<MockEntity>().Received(1).Update(mockEntity);
        }

        [Fact]
        public void Delete_ShouldReceiveCall_WhenEntityWasPassedToTheMethod()
        {

            // Arrange
            var mockEntity = new MockEntity()
            {
                Id = 1,
                Name = "Foo"
            };

            // Act
            _sut.Delete(mockEntity);

            // Assert
            _appDbContext.Set<MockEntity>().Received(1).Remove(mockEntity);
        }

        [Fact]
        public void FindByCondition_ShouldReturnCorrectResult_WhenConditionWasPassedToMethod()
        {
            // Arrange
            var entityList = new List<MockEntity>()
            {
                new MockEntity() { Id = 1, Name = "Foo" },
                new MockEntity { Id = 2,Name = "Bar"}
            };

            var dbSetSubstitute = Substitute.For<DbSet<MockEntity>, IQueryable<MockEntity>>();
            ((IQueryable<MockEntity>)dbSetSubstitute).Provider.Returns(entityList.AsQueryable().Provider);
            ((IQueryable<MockEntity>)dbSetSubstitute).Expression.Returns(entityList.AsQueryable().Expression);
            ((IQueryable<MockEntity>)dbSetSubstitute).ElementType.Returns(entityList.AsQueryable().ElementType);
            ((IQueryable<MockEntity>)dbSetSubstitute).GetEnumerator().Returns(entityList.AsQueryable().GetEnumerator());

            _appDbContext.Set<MockEntity>().Returns(dbSetSubstitute);
            var expected = new MockEntity() { Id = 1, Name = "Foo" };

            // Act

            var result = _sut.FindByCondition(x => x.Id == 1,false).First();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_ShouldReturnCorrectResult_WhenConditionWasPassedToMethod()
        {
            // Arrange
            var entityList = new List<MockEntity>()
            {
                new MockEntity() { Id = 1, Name = "Foo" },
                new MockEntity { Id = 2,Name = "Bar"}
            };

            var dbSetSubstitute = Substitute.For<DbSet<MockEntity>, IQueryable<MockEntity>>();
            ((IQueryable<MockEntity>)dbSetSubstitute).Provider.Returns(entityList.AsQueryable().Provider);
            ((IQueryable<MockEntity>)dbSetSubstitute).Expression.Returns(entityList.AsQueryable().Expression);
            ((IQueryable<MockEntity>)dbSetSubstitute).ElementType.Returns(entityList.AsQueryable().ElementType);
            ((IQueryable<MockEntity>)dbSetSubstitute).GetEnumerator().Returns(entityList.AsQueryable().GetEnumerator());

            _appDbContext.Set<MockEntity>().Returns(dbSetSubstitute);

            // Act

            var result = _sut.GetAll(false).ToList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(entityList);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldReceiveCall_WhenItIsInvokedAsync()
        {
            // Arrange

            // Act
            await _sut.SaveChangesAsync();

            // Assert
            await _appDbContext.Received(1).SaveChangesAsync();
        }



    }
}
