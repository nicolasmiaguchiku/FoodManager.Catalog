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
            //Pegar o assembly
            var assembly = Assembly.Load("FoodManager.Domain");

            // Verificar se tem dependencia com os outros projetos
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.Application")
                .And()
                .NotHaveDependencyOn("FoodManager.CrossCutting")
                .And()
                .NotHaveDependencyOn("FoodManager.Infrastructure")
                .And()
                .NotHaveDependencyOn("FoodManager.WebApi")
                .GetResult();

            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ApplicationLayerShoudNotDependCroosCuttingAndInfrastructureAndWebApi()
        {
            //Pegar o assembly
            var assembly = Assembly.Load("FoodManager.Application");

            //Verificar se tem dependência com a CroosCutting, Infrastructure e Api
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.CrossCutting")
                .And()
                .NotHaveDependencyOn("FoodManager.Infrastructure")
                .And()
                .NotHaveDependencyOn("FoodManager.WebApi")
                .GetResult();


            //Verificar se é result é verdadeiro
            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void CrossCuttingsLayerSoudNotDepedentToWebApi()
        {
            var assembly = Assembly.Load("FoodManager.CrossCutting");

            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.WebApi")
                .GetResult();

            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void InfrastructurelayerShoudNotToDedpendentWebApiAndCrossCutting()
        {
            var assembly = Assembly.Load("FoodManager.Infrastructure");

            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("FoodManager.WebApi")
                .And()
                .NotHaveDependencyOn("FoodManager.CrossCutting")
                .GetResult();

            result.IsSuccessful
                .Should()
                .BeTrue();
        }

        [Fact]
        public void WebApiLayerShouldNotToDependentDomainAndInfrastructureAndCrossCutting()
        {
            var assembly = Assembly.Load("FoodManager.WebApi");

            var result = Types.InAssembly(assembly)
                .That()
                .DoNotResideInNamespace(".CrossCutting")
                .Should()
                .NotHaveDependencyOn("FoodManager.Domain")
                .And()
                .NotHaveDependencyOn("FoodManager.Infrastructure")
                .GetResult();

            result.IsSuccessful
                .Should()
                .BeTrue();
        }
    }
}