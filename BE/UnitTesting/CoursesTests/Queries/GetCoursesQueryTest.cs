using Application.Courses.Queries.GetCourses;
using Domain.Dtos;
using Moq;
using UnitTests.AutoFixure;
using AutoFixture.NUnit3;
using FluentAssertions;
using Infrastructure.Interfaces;

namespace UnitTesting.CoursesTests.Queries
{
    public class GetCoursesQueryTest
    {

        [Theory]
        [AutoMockData]
        public async Task GetCourses_Handle_Success(GetCoursesQuery request, [Frozen] Mock<ICourseRepository> courseRepository,
          List<CourseDto> result,
         [Greedy] GetCoursesQueryHandler handler)
        {

            //Arrange
            courseRepository.Setup(pa => pa.GetCourses(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), new CancellationToken())).ReturnsAsync(result);

            //Act
            var response = await handler.Handle(request,CancellationToken.None);

            //Assert
            response.Should().BeEquivalentTo(result);


        }

        [Theory]
        [AutoMockData]
        public async Task GetCourses_Handle_Exception(GetCoursesQuery request, [Frozen] Mock<ICourseRepository> courseRepository,
          List<CourseDto> result,
         [Greedy] GetCoursesQueryHandler handler)
        {

            //Arrange
            courseRepository.Setup(pa => pa.GetCourses(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), new CancellationToken())).ThrowsAsync(new Exception());

            //Act
            Func<Task> func = async () => await handler.Handle(request, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<Exception>();

        }
    }


}
