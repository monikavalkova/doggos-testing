using System.Collections.Generic;
using Doggo.API.Controllers;
using Doggo.API.Models;
using Doggo.API.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Doggo.xUnit.Tests;

public class DogsControllerTests
{
    private DogsController _controller;
    private Mock<IDogsService> _serviceMock;

    private static DogDto MOCK_DOG = new DogDto() { Name = "doberman" };
    
    private static DogsResponse MOCK_DOGS_RESPONSE = new DogsResponse()
    {
        Count = 5,
        PageSize = 1,
        Dogs = new List<DogDto> { MOCK_DOG }
    };

    public DogsControllerTests()
    {
        _serviceMock = new Mock<IDogsService>();
        _serviceMock.Setup(serv => serv.GetDogsOfBreed(It.IsAny<string>())).Returns(MOCK_DOGS_RESPONSE);
        _controller = new DogsController(_serviceMock.Object);
    }

    [Fact]
    public void find_by_breed_should_not_return_null()
    {
        //act
        var result = _controller.GetDogsByBreed();
        //assert
        result.Should().NotBeNull();
    }


}