using System.Collections.Generic;
using Anima.WebAPI.Controllers;
using Anima.WebAPI.Models;
using Anima.WebAPI.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Anima.API.Tests
{
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
}

