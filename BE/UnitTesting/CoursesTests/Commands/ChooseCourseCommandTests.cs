using Application.Courses.Queries.GetCourses;
using Domain.Dtos;
using Moq;
using UnitTests.AutoFixure;
using AutoFixture.NUnit3;
using FluentAssertions;
using Infrastructure.Interfaces;
using Application.Courses.Commands;
using Domain.Entities;

namespace UnitTesting.CoursesTests.Commands
{
    public class ChooseCourseCommandTests
    {

        [Theory]
        [AutoMockData]
        public async Task ChooseCourses_Handle_Success(ChooseCourseCommand request, [Frozen] Mock<ICourseRepository> courseRepository,
            [Frozen] Mock<IContactRepository> contactRepository,
        CourseDto course, ContactDto contact,
       [Greedy] ChooseCourseCommandHandler handler)
        {

            //Arrange
            courseRepository.Setup(pa => pa.GetCourseById(It.IsAny<Guid>())).ReturnsAsync(course);
            contactRepository.Setup(pa => pa.AddCourse(It.IsAny<CourseDto>(), It.IsAny<int>(), new CancellationToken())).ReturnsAsync(contact);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            response.Should().BeEquivalentTo(contact);


        }


        [Theory]
        [AutoMockData]
        public async Task ChooseCourses_Handle_Exception(ChooseCourseCommand request, [Frozen] Mock<ICourseRepository> courseRepository,
            [Frozen] Mock<IContactRepository> contactRepository,
        CourseDto course, ContactDto contact,
       [Greedy] ChooseCourseCommandHandler handler)
        {

            //Arrange
            courseRepository.Setup(pa => pa.GetCourseById(It.IsAny<Guid>())).ReturnsAsync(course);
            contactRepository.Setup(pa => pa.AddCourse(It.IsAny<CourseDto>(), It.IsAny<int>(), new CancellationToken())).ThrowsAsync(new Exception());

            //Act
            Func<Task> func = async () => await handler.Handle(request, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<Exception>();


        }

        
    }
}
