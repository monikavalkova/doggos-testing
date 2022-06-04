using System.Collections.Generic;
using Doggo.API.Controllers;
using Doggo.API.Models;
using Doggo.API.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Doggo.xUnit.Tests;

public class AFAControllerTests
{
    private AnimaController _controller;
    private Mock<IAFAService> _serviceMock;
 
    public AFAControllerTests()
    {
        _serviceMock = new Mock<IAFAService>();
        
        _controller = new AnimaController(_serviceMock.Object);
    }


}