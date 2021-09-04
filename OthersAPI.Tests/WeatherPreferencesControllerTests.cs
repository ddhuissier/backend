using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OthersAPI.Controllers;
using OthersAPI.Models;
using System;
using WebApi.Repositories;
using Xunit;

namespace OthersAPI.Tests
{
    /// <summary>
    /// Tests with Fluent Assertions
    /// </summary>
    public class WeatherPreferencesControllerTests
    {
        private readonly Mock<IWeatherPrefRepository> _repositoryStub = new Mock<IWeatherPrefRepository>();
        private readonly Mock<ILogger<WeatherPreferencesController>> _loggerStub = new Mock<ILogger<WeatherPreferencesController>>();
        private  WeatherPreferencesController controller;

        public WeatherPreferencesControllerTests()
        {
        }

        [Fact]
        public void GetWeatherPref_WithUnexisingWeatherPref_ReturnsNotFound()
        {
            // Arrange
            _repositoryStub.Setup(repo => repo.Get(It.IsAny<Guid>()))
                .Returns((WeatherPreference)null);
            controller = new WeatherPreferencesController(_repositoryStub.Object, _loggerStub.Object);


            // Act
            var result = controller.Get(Guid.NewGuid());

            // Asserts

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetWeatherPref_WithExisingWeatherPref_ReturnsFound()
        {
            // Arrange
            var expectedResult = CreateWeatherPreferenceItem();
            _repositoryStub.Setup(repo => repo.Get(It.IsAny<Guid>()))
              .Returns(expectedResult);

            controller = new WeatherPreferencesController(_repositoryStub.Object, _loggerStub.Object);


            // Act
            var result = controller.Get(Guid.NewGuid());

            // Asserts
            result.Value.Should().BeEquivalentTo(
                expectedResult,
                options => options.ComparingByMembers<WeatherPreference>() );
        }
        [Fact]
        public void GetWeatherPrefs_WithExisingWeatherPrefs_ReturnsPrefs()
        {
            // Arrange
            var expectedPrefs = new[]
            {
                CreateWeatherPreferenceItem(),
                CreateWeatherPreferenceItem(),
                CreateWeatherPreferenceItem()
            };
            _repositoryStub.Setup(repo => repo.Get())
               .Returns(expectedPrefs);

            controller = new WeatherPreferencesController(_repositoryStub.Object, _loggerStub.Object);

            // Act
            var result = controller.Get();

            // Asserts
            result.Should().BeEquivalentTo(
                expectedPrefs,
                options => options.ComparingByMembers<WeatherPreference>());
        }

       
       

        private WeatherPreference CreateWeatherPreferenceItem()
        {
            return new WeatherPreference
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                CreateDate = DateTimeOffset.Now
            };
        }
    }
}
