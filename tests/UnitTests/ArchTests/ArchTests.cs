using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace UnitTests.ArchTests
{
    public class ArchTests
    {
        [Fact]
        public void DomainLayerShouldNotDependOtherProjects()
        {
            //Arrange
            var assembly = Assembly.Load("FoodManager.Catalog.Domain");

            //Act
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.Catalog.Application")
                .And()
                .NotHaveDependencyOn("FoodManager.Catalog.CrossCutting")
                .And()
                .NotHaveDependencyOn("FoodManager.Catalog.Infrastructure")
                .And()
                .NotHaveDependencyOn("FoodManager.Catalog.WebApi")
                .GetResult();

            //Assert
            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ApplicationLayerShoudNotDependCroosCuttingAndInfrastructureAndWebApi()
        {
            //Arrange
            var assembly = Assembly.Load("FoodManager.Catalog.Application");

            //Act
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.Catalog.CrossCutting")
                .And()
                .NotHaveDependencyOn("FoodManager.Catalog.Infrastructure")
                .And()
                .NotHaveDependencyOn("FoodManager.Catalog.WebApi")
                .GetResult();


            //Assert
            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void CrossCuttingsLayerSoudNotDepedentToWebApi()
        {
            //Arrange
            var assembly = Assembly.Load("FoodManager.Catalog.CrossCutting");

            //Act
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.Catalog.WebApi")
                .GetResult();

            //Assert
            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void InfrastructurelayerShoudNotToDedpendentWebApiAndCrossCutting()
        {
            //Arrange
            var assembly = Assembly.Load("FoodManager.Catalog.Infrastructure");

            //Act
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.Catalog.WebApi")
                .And()
                .NotHaveDependencyOn("FoodManager.Catalog.CrossCutting")
                .GetResult();

            //Assert
            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void WebApiLayerShouldNotToDependentDomainAndInfrastructure()
        {
            //Arrange
            var assembly = Assembly.Load("FoodManager.Catalog.WebApi");

            //Act
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.Catalog.Infrastructure")
                .GetResult();

            //Assert
            result.IsSuccessful
                .Should()
                .BeTrue();
        }
    }
}