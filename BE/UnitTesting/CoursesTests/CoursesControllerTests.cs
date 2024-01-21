using APIs.Controllers;
using Application.Courses.Queries.GetCourses;
using AutoFixture.NUnit3;
using Domain.Dtos;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Moq;
using UnitTests.AutoFixure;

namespace UnitTesting.CoursesTests
{
    public class CoursesControllerTests
    {
        [Theory]
        [AutoMockData]
        public async Task GetCourses_ValidData_Success(  GetCoursesQuery command, List<CourseDto> result,
        [Frozen] Mock<IMediator> mediator,
          [Greedy] CoursesController coursesController)
        {
            
            //Arrange
            //getCoursesQueryHandler.Setup(pa => pa.Handle(command, new CancellationToken())).ReturnsAsync(result);
            mediator.Setup(pa => pa.Send(It.IsAny<GetCoursesQuery>(), new CancellationToken())).ReturnsAsync(result);

            //Act
            var response = await coursesController.GetCourses(command);

            //Assert
            response.Should().BeEquivalentTo(result);

        }

        [Theory]
        [AutoMockData]
        public async Task GetCourses_ValidData_Exception(GetCoursesQuery command, List<CourseDto> result,
         [Frozen] Mock<IMediator> mediator,
         [Greedy] CoursesController coursesController)
        {

            //Arrange
            //getCoursesQueryHandler.Setup(pa => pa.Handle(command, new CancellationToken())).ReturnsAsync(result);
            mediator.Setup(pa => pa.Send(It.IsAny<GetCoursesQuery>(), new CancellationToken())).ThrowsAsync(new Exception());


            //Act
            Func<Task> func = async () =>  await coursesController.GetCourses(command);


            //Assert
            await func.Should().ThrowAsync<Exception>();

        }

    }
}
