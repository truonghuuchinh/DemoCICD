using DemoCICD.Contract;
using DemoCICD.Contract.Abstractions.Messages;
using FluentAssertions;
using NetArchTest.Rules;

namespace DemoCICD.Architecture.Tests;

/// <summary>
/// Note if we don't use any class of reference project, it'll be same like 
/// we don't reference anything
/// </summary>
public class Architecture
{
    private const string ApplicationNamespace = "DemoCICD.Application";
    private const string DomainNamespace = "DemoCICD.Domain";
    private const string InfrastructureNamespace = "DemoCICD.Infrastructure";
    private const string WebAPINamespace = "DemoCICD.API";
    private const string PersistenceNamespace = "DemoCICD.Persistence";
    private const string PresentationNamespace = "DemoCICD.Presentation";

    [Fact]
    public void Domain_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Domain.AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            WebAPINamespace,
            PersistenceNamespace,
            PresentationNamespace
        };

        // Action
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            WebAPINamespace,
            PersistenceNamespace,
            PresentationNamespace
        };

        // Action
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            WebAPINamespace,
            PresentationNamespace,
        };

        // Action
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            WebAPINamespace,
            PresentationNamespace,
            InfrastructureNamespace,
            ApplicationNamespace,
        };

        // Action
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            WebAPINamespace,
            PersistenceNamespace,
        };

        // Action
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    #region ============== Command ================

    [Fact]
    public void Command_Should_Have_NamingConventionEndingCommand()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var endSuffix = "Command";

        // Action
        var typeCommand = typeof(ICommand);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .HaveNameEndingWith(endSuffix, StringComparison.Ordinal)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandGeneric_Should_Have_NamingConventionEndingCommand()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var endSuffix = "Command";

        // Action
        var typeCommand = typeof(ICommand<>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .HaveNameEndingWith(endSuffix, StringComparison.Ordinal)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Action
        var typeCommand = typeof(ICommand<>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandGeneric_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Action
        var typeCommand = typeof(ICommand<>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handler_Should_Have_NamingConventionEndingCommand()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var endSuffix = "Handler";

        // Action
        var typeCommand = typeof(ICommandHandler<>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .HaveNameEndingWith(endSuffix, StringComparison.Ordinal)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void HandlerGeneric_Should_Have_NamingConventionEndingCommand()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var endSuffix = "Handler";

        // Action
        var typeCommand = typeof(ICommandHandler<,>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .HaveNameEndingWith(endSuffix, StringComparison.Ordinal)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }


    [Fact]
    public void Handler_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Action
        var typeCommand = typeof(ICommandHandler<>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void HandlerGeneric_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Action
        var typeCommand = typeof(ICommandHandler<,>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeCommand)
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    #endregion

    #region ============== Query ================

    [Fact]
    public void QueryGeneric_Should_Have_NamingConventionEndingQuery()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var endSuffix = "Query";

        // Action
        var typeQuery = typeof(IQuery<>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeQuery)
            .Should()
            .HaveNameEndingWith(endSuffix, StringComparison.Ordinal)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlerGeneric_Should_Have_NamingConventionEndingQuery()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;
        var endSuffix = "Handler";

        // Action
        var typeQuery = typeof(IQueryHandler<,>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeQuery)
            .Should()
            .HaveNameEndingWith(endSuffix, StringComparison.Ordinal)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlerGeneric_Should_Have_BeSealed()
    {
        // Arrange
        var assembly = AssemblyReference.Assembly;

        // Action
        var typeQuery = typeof(IQueryHandler<,>);
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeQuery)
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    #endregion
}
