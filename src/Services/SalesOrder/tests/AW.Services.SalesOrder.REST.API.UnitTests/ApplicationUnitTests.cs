﻿using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Xunit;

namespace AW.Services.SalesOrder.REST.API.UnitTests
{
    public class ApplicationUnitTests
    {
        [Theory, AutoMoqData]
        public void Namespace_ReturnsApiNamespace(
            Application app
        )
        {
            //Arrange

            //Act
            var ns = app.Namespace;

            //Assert
            ns.Should().Be("AW.Services.SalesOrder.REST.API");
        }

        [Theory, AutoMoqData]
        public void AppName_ReturnsApiNamespace(
            Application app
        )
        {
            //Arrange

            //Act
            var appName = app.AppName;

            //Assert
            appName.Should().Be("SalesOrder.REST.API");
        }
    }
}