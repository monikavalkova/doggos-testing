using System.Collections.Generic;
using Doggo.API.Controllers;
using Doggo.API.Models;
using Doggo.API.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Doggo.xUnit.Tests;

public class CatsControllerTests
{
    private Mock<ICatsService> _serviceMock;
    private CatsController _controller;

    private static CatDto MOCK_CAT = new CatDto() { Name = "Sphynx" };
    
    private static CatsResponse MOCK_CATS_RESPONSE = new CatsResponse()
    {
        Count = 5,
        PageSize = 1,
        Cats = new List<CatDto> { MOCK_CAT }
    };

    public CatsControllerTests()
    {
        _serviceMock = new Mock<ICatsService>();
        _serviceMock.Setup(serv => serv.GetCatsOfBreed(It.IsAny<string>())).Returns(MOCK_CATS_RESPONSE);
        _controller = new CatsController(_serviceMock.Object);
    }

    [Fact]
    public void find_by_breed_should_not_return_null()
    {
        //act
        //var result = _controller.FindCats();
        //assert
        //result.Should().NotBeNull();
    }
}