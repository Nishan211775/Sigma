using Domain.Model;
using Domain;
using Moq;
using Repository.Interface;
using Service.Implementation;
using Domain.ViewModel;
using FluentAssertions;

namespace Test
{
    public class CandidateTest
    {
        private readonly CandidateService _service;
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;

        public CandidateTest()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _service = new CandidateService(_candidateRepositoryMock.Object);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccess_WhenCandidateExists()
        {
            // Arrange
            var candidate = new Candidate { Id = 1 };
            _candidateRepositoryMock
                .Setup(repo => repo.GetCandidateByIdAsync(1))
                .ReturnsAsync(candidate);
            _candidateRepositoryMock
                .Setup(repo => repo.DeleteAsync(candidate))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _service.DeleteAsync(1);

            // Assert
            response.ResponseType.Should().Be(ResponseType.Success);
            _candidateRepositoryMock.Verify(repo => repo.DeleteAsync(candidate), Times.Once);
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnSuccess_WhenEmailDoesNotExist()
        {
            // Arrange
            var candidateModel = new CandidateViewModel { 
                FirstName = "", 
                LastName = "",
                Email = "test@example.com", 
                TimeInterval = "2",
                Comment = "This is comment"
            };
            _candidateRepositoryMock
                .Setup(repo => repo.SaveAsync(It.IsAny<Candidate>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _service.SaveAsync(candidateModel);

            // Assert
            response.ResponseType.Should().Be(ResponseType.Success);
            _candidateRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<Candidate>()), Times.Once);
        }
    }
}