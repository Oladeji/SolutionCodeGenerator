using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAppFilesGenerator
{
    public class GenerateApplicationTests
    {
        public static string GenerateCreateHandlerTest(Type type, string thenamespace, string apiVersion)
        {

            // var entityName = type.Name;
            var Output = new StringBuilder();
            string className = "Create" + type.Name + "CommandHandlerTest";
            Output.Append(ProduceCreateTestHeader(thenamespace, type, apiVersion, className));
            Output.Append($"\n");
            Output.Append(ProduceCreateTestConstructor(className, type));
            Output.Append($"\n");
            Output.Append(CreateCommandHandler_ShouldReturn_ValidRight_WhenNewEntityIsAdded(thenamespace, type));
            Output.Append($"\n");
            Output.Append(CreateCommandHandler_ShouldReturn_GeneraFailure_WhenAdditionResultInError(thenamespace, type));
            Output.Append($"\n");
            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }


        private static string CreateCommandHandler_ShouldReturn_GeneraFailure_WhenAdditionResultInError(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[Fact]" +

             $"{GeneralClass.newlinepad(8)}public async Task CreateCommandHandler_ShouldReturn_GeneraFailure_WhenAdditionResultInError()" +
             $"{GeneralClass.newlinepad(8)}{{\n" +
             $"{GeneralClass.newlinepad(12)}//Arrange" +
             $"{GeneralClass.newlinepad(12)}var generalFailure = GeneralFailures.ProblemAddingEntityIntoDbContext(\"Error\");" +
             $"{GeneralClass.newlinepad(12)}_{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock.AddAsync(Arg.Any<Domain.Entities.{type.Name}>(), Arg.Any<CancellationToken>()).Returns(generalFailure);\n" +
             $"{GeneralClass.newlinepad(12)}//Act" +
             $"{GeneralClass.newlinepad(12)}var result = await create{type.Name}CommandHandler.Handle(create{type.Name}Command, CancellationToken.None);\n" +
             $"{GeneralClass.newlinepad(12)}//Assert" +

             $"{GeneralClass.newlinepad(12)}result.IsLeft.Should().BeTrue();" +
             $"{GeneralClass.newlinepad(12)}result.Match(" +
             $"{GeneralClass.newlinepad(24)}Right: r => r.Should().Be(res)," +
             $"{GeneralClass.newlinepad(24)}Left: l => l.Should().BeEquivalentTo(generalFailure));//INTERESTED ONLY IN LEFT SIDE" +
             $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string CreateCommandHandler_ShouldReturn_ValidRight_WhenNewEntityIsAdded(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[Fact]" +

            $"{GeneralClass.newlinepad(8)}public async Task Create_{type.Name}CommandHandler_ShouldRetun_A_ValidRight_WhenNewEntityIsAdded()" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}//Arrange" +
            $"{GeneralClass.newlinepad(12)}_{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock.AddAsync(Arg.Any<Domain.Entities.{type.Name}>(), Arg.Any<CancellationToken>()).Returns(1);\n" +
            $"{GeneralClass.newlinepad(12)}//Act" +

            $"{GeneralClass.newlinepad(12)}var result = await create{type.Name}CommandHandler.Handle(create{type.Name}Command, CancellationToken.None);\n" +
            $"{GeneralClass.newlinepad(12)}//Assert" +
            $"{GeneralClass.newlinepad(12)}result.IsRight.Should().BeTrue();" +
            $"{GeneralClass.newlinepad(12)}result.Match(" +
            $"{GeneralClass.newlinepad(24)}Right: r => r.Should().Be(res),//INTERESTED ONLY IN RIGHT SIDE" +
            $"{GeneralClass.newlinepad(24)}Left: l => l.Should().BeEquivalentTo(GeneralFailures.ProblemAddingEntityIntoDbContext(\"2a7c336a-163c-487d-88ca-c41cc129f118\")));" +
            $"{GeneralClass.newlinepad(8)}}}";

        }

        private static string ProduceCreateTestHeader(string name_space, Type type, string apiVersion, string className)
        {
            return ($"using Microsoft.Extensions.Logging;\n" +
            $"using FluentAssertions;\n" +
            $"using NSubstitute;\n" +
            $"using RegistrationManager.Application.CQRS;\n" +
            $"using RegistrationManager.Contracts.RequestDTO.V1;\n" +
            $"using RegistrationManager.Domain.Errors;\n" +
            $"using RegistrationManager.Domain.Interfaces;\n" +

            $"namespace {name_space}.Application.Tests.CQRS.{type.Name}\n{{" +
            $"{GeneralClass.newlinepad(4)}public  class {className}" +
            $"{GeneralClass.newlinepad(4)}{{" +

            $"{GeneralClass.newlinepad(8)}private static readonly Guid res = Guid.NewGuid();" +
            $"{GeneralClass.newlinepad(8)}private static readonly {type.Name}CreateRequestDTO {GeneralClass.FirstCharSubstringToLower(type.Name)}CreateRequestDTO = new {type.Name}CreateRequestDTO(\"code\", \"name\", \"desc\", res);" +
            $"{GeneralClass.newlinepad(8)}private static readonly Create{type.Name}Command create{type.Name}Command = new Create{type.Name}Command({GeneralClass.FirstCharSubstringToLower(type.Name)}CreateRequestDTO);" +
            $"{GeneralClass.newlinepad(8)}private readonly Create{type.Name}CommandHandler create{type.Name}CommandHandler;" +
            $"{GeneralClass.newlinepad(8)}private readonly I{type.Name}Repository _{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock;" +
            $"{GeneralClass.newlinepad(8)}private readonly ILogger<Create{type.Name}CommandHandler> _loggerMock;" +
            $"{GeneralClass.newlinepad(8)}private readonly IUnitOfWork _unitOfWorkMock;");
        }

        private static string ProduceCreateTestConstructor(string classname, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}public {classname}()" +
                $"{GeneralClass.newlinepad(8)}{{" +
                $"{GeneralClass.newlinepad(12)}_unitOfWorkMock = Substitute.For<IUnitOfWork>();" +
                $"{GeneralClass.newlinepad(12)}_loggerMock = Substitute.For<ILogger<Create{type.Name}CommandHandler>>();" +
                $"{GeneralClass.newlinepad(12)}_{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock = Substitute.For<I{type.Name}Repository>();" +
                $"{GeneralClass.newlinepad(12)}create{type.Name}CommandHandler = new Create{type.Name}CommandHandler(_unitOfWorkMock, _loggerMock, _{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock);" +
                $"{GeneralClass.newlinepad(8)}}}";
        }




        internal static string GenerateGetByIdHandlerTest(Type type, string thenamespace, string apiVersion)
        {
            // var entityName = type.Name;
            var Output = new StringBuilder();

            string className = "Get" + type.Name + "ByIdQueryHandlerTest"; ;
            Output.Append(ProduceByIdQueryTestHeader(thenamespace, type, apiVersion, className));
            Output.Append($"\n");
            Output.Append(ProduceByIdQueryTestConstructor(className, type));
            Output.Append($"\n");
            Output.Append(GetByIdQuery_ShouldRetun_A_ValidRight_WhenNewEntityIsAdded(thenamespace, type));
            Output.Append($"\n");
            Output.Append(GetByIdQuery_ShouldRetunGeneraFailure_WhenEntityDoesNotExist(thenamespace, type));
            Output.Append($"\n");
            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        private static string GetByIdQuery_ShouldRetun_A_ValidRight_WhenNewEntityIsAdded(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[Fact]" +

            $"{GeneralClass.newlinepad(8)}public async Task Get_{type.Name}ByIdQueryHandler_ShouldRetun_A_ValidRight_WhenNewEntityIsAdded()" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}//Arrange" +
            $"{GeneralClass.newlinepad(12)}var a{GeneralClass.FirstCharSubstringToLower(type.Name)}= Domain.Entities.{type.Name}.Create(\"code\", \"name\", \"description\", res);" +
            $"{GeneralClass.newlinepad(12)}_{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock.GetMatch(Arg.Any<System.Linq.Expressions.Expression<System.Func<Domain.Entities.{type.Name}, bool>>>(), null, Arg.Any<System.Threading.CancellationToken>()).Returns(a{GeneralClass.FirstCharSubstringToLower(type.Name)});\n" +
            $"{GeneralClass.newlinepad(12)}//Act" +

            $"{GeneralClass.newlinepad(12)}var result = await get{type.Name}ByIdQueryHandler.Handle(get{type.Name}ByIdQuery, CancellationToken.None);\n" +
            $"{GeneralClass.newlinepad(12)}//Assert" +
            $"{GeneralClass.newlinepad(12)}result.IsRight.Should().BeTrue();" +
            $"{GeneralClass.newlinepad(12)}result.Match(" +
            $"{GeneralClass.newlinepad(24)}Right: r => r.guidId.Should().Be(res),//INTERESTED ONLY IN RIGHT SIDE" +
            $"{GeneralClass.newlinepad(24)}Left: l => l.Should().BeEquivalentTo(GeneralFailures.ProblemAddingEntityIntoDbContext(\"2a7c336a-163c-487d-88ca-c41cc129f118\")));" +
            $"{GeneralClass.newlinepad(8)}}}";

        }

        private static string GetByIdQuery_ShouldRetunGeneraFailure_WhenEntityDoesNotExist(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[Fact]" +

              $"{GeneralClass.newlinepad(8)}public async Task Get_{type.Name}ByIdQueryHandler_ShouldRetunGeneraFailure_WhenEntityDoesNotExist()" +
              $"{GeneralClass.newlinepad(8)}{{" +
              $"{GeneralClass.newlinepad(12)}//Arrange" +
               $"{GeneralClass.newlinepad(12)}var generalFailure = GeneralFailures.ProblemAddingEntityIntoDbContext(\"Error\");" +
              $"{GeneralClass.newlinepad(12)}var a{GeneralClass.FirstCharSubstringToLower(type.Name)}= Domain.Entities.{type.Name}.Create(\"code\", \"name\", \"description\", res);" +
              $"{GeneralClass.newlinepad(12)}_{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock.GetMatch(Arg.Any<System.Linq.Expressions.Expression<System.Func<Domain.Entities.{type.Name}, bool>>>(), null, Arg.Any<System.Threading.CancellationToken>()).Returns(generalFailure);\n" +
              $"{GeneralClass.newlinepad(12)}//Act" +

              $"{GeneralClass.newlinepad(12)}var result = await get{type.Name}ByIdQueryHandler.Handle(get{type.Name}ByIdQuery, CancellationToken.None);\n" +
              $"{GeneralClass.newlinepad(12)}//Assert" +
        $"{GeneralClass.newlinepad(12)}result.IsLeft.Should().BeTrue();" +
             $"{GeneralClass.newlinepad(12)}result.Match(" +
             $"{GeneralClass.newlinepad(24)}Right: r => r.Should().Be(res)," +
             $"{GeneralClass.newlinepad(24)}Left: l => l.Should().BeEquivalentTo(generalFailure));//INTERESTED ONLY IN LEFT SIDE" +
             $"{GeneralClass.newlinepad(8)}}}";


        }

        private static string ProduceByIdQueryTestConstructor(string className, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}public {className}()" +
              $"{GeneralClass.newlinepad(8)}{{" +
              $"{GeneralClass.newlinepad(12)}_unitOfWorkMock = Substitute.For<IUnitOfWork>();" +

              $"{GeneralClass.newlinepad(12)}_loggerMock = Substitute.For<ILogger<Get{type.Name}ByIdQueryHandler>>();" +
              $"{GeneralClass.newlinepad(12)}_{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock = Substitute.For<I{type.Name}Repository>();" +
              $"{GeneralClass.newlinepad(12)}get{type.Name}ByIdQueryHandler = new Get{type.Name}ByIdQueryHandler(_unitOfWorkMock, _loggerMock, _{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock);" +
              $"{GeneralClass.newlinepad(8)}}}";

        }





        private static string ProduceByIdQueryTestHeader(string thenamespace, Type type, string apiVersion, string className)
        {
            return ($"using Microsoft.Extensions.Logging;\n" +
                  $"using FluentAssertions;\n" +
                  $"using NSubstitute;\n" +
                  $"using RegistrationManager.Application.CQRS;\n" +
                  $"using RegistrationManager.Contracts.RequestDTO.V1;\n" +
                  $"using RegistrationManager.Domain.Errors;\n" +
                  $"using RegistrationManager.Domain.Interfaces;\n" +

                  $"namespace {thenamespace}.Application.Tests.CQRS.{type.Name}\n{{" +
                  $"{GeneralClass.newlinepad(4)}public  class {className}" +
                  $"{GeneralClass.newlinepad(4)}{{" +
                  $"{GeneralClass.newlinepad(8)}private static readonly Guid res = Guid.NewGuid();" +
                  $"{GeneralClass.newlinepad(8)}private static readonly {type.Name}GetRequestByIdDTO  {GeneralClass.FirstCharSubstringToLower(type.Name)}GetRequestByIdDTO = new {type.Name}GetRequestByIdDTO(\"Id\");" +
                  $"{GeneralClass.newlinepad(8)}private static readonly Get{type.Name}ByIdQuery   get{type.Name}ByIdQuery = new Get{type.Name}ByIdQuery({GeneralClass.FirstCharSubstringToLower(type.Name)}GetRequestByIdDTO);" +
                  $"{GeneralClass.newlinepad(8)}private readonly Get{type.Name}ByIdQueryHandler   get{type.Name}ByIdQueryHandler;" +
                   $"{GeneralClass.newlinepad(8)}private readonly I{type.Name}Repository   _{GeneralClass.FirstCharSubstringToLower(type.Name)}RepositoryMock;" +
                  $"{GeneralClass.newlinepad(8)}private readonly ILogger<Get{type.Name}ByIdQueryHandler>   _loggerMock;" +

                  $"{GeneralClass.newlinepad(8)}private readonly IUnitOfWork _unitOfWorkMock;");

        }
    }

}