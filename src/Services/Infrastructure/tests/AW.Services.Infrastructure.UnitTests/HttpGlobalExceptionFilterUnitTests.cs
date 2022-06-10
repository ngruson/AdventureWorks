using AW.Services.Infrastructure.Filters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AW.Services.Infrastructure.UnitTests
{
    public class HttpGlobalExceptionFilterUnitTests
    {
        [Theory, AutoMoqData]
        public void OnException_DomainException_BadRequest(
            HttpGlobalExceptionFilter sut,
            DomainException exception,
            ExceptionContext context,
            DefaultHttpContext httpContext
        )
        {
            //Arrange
            context.Exception = exception;
            context.HttpContext = httpContext;

            //Act
            sut.OnException(context);

            //Assert
            context.ExceptionHandled.Should().BeTrue();
            context.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Theory, AutoMoqData]
        public void OnException_OtherException_InternalServerError(
            HttpGlobalExceptionFilter sut,
            Exception exception,
            ExceptionContext context,
            DefaultHttpContext httpContext
        )
        {
            //Arrange
            context.Exception = exception;
            context.HttpContext = httpContext;

            //Act
            sut.OnException(context);

            //Assert
            context.ExceptionHandled.Should().BeTrue();
            context.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}