using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateControllers
    {
        public static string Generate(Type type, string thenamespace, string controllerversion)
        {

            // var entityName = type.Name;
            var Output = new StringBuilder();
            string controllername = type.Name + "sController";
            Output.Append(ProduceControllerHeader(thenamespace, type, controllerversion, controllername));
            Output.Append(ProduceControllerConstructor(controllername));
            Output.Append(ProduceControllerCreate(thenamespace, type));
            Output.Append(ProduceControllerDelete(thenamespace, type));
            Output.Append(ProduceControllerUpdate(thenamespace, type));
            Output.Append(ProduceControllerGet(thenamespace, type));
            Output.Append(ProduceControllerGetById(thenamespace, type));
            Output.Append(ProduceControllerGetByEntityUsingBody(thenamespace, type));
            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }
        // throw new NotImplementedException();
        private static string ProduceControllerGetByEntityUsingBody(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.Get, Name = {thenamespace}APIEndPoints.{type.Name}.Get)]" +
     $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Get({type.Name}CreateDTO request, CancellationToken cancellationToken)" +
     $"{GeneralClass.newlinepad(8)}{{";

        }

        private static string ProduceControllerGetById(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(IEnumerable<{type.Name}ResponseDTO>), StatusCodes.Status200OK)]" +
            $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.GetById, Name = {thenamespace}APIEndPoints.{type.Name}.GetById)]" +
            $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> GetById([FromRoute] string NameOrGuid, CancellationToken cancellationToken)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"var x = NameOrGuid.EnsureInputIsNotEmpty(\"Input Cannot be null\");" +
            $"var result = Guid.TryParse(NameOrGuid, out Guid guid);" +
            $" if (result)" +
            $"{{" +
            $"return (await _sender.Send(new GetModelTypeByGuidQuery(new ApplicationModelTypeRequestByGuidDTO(guid)), cancellationToken))" +
            $".Match<IActionResult>(Left: errors => new OkObjectResult(errors)," +
            $"Right: result => new OkObjectResult(new ModelTypeResponseDTO(result.ModelTypesId, result.ModelTypesName, CovertToModelTypeResponse(result.Models)" +
            $"";

        }

        private static string ProduceControllerGet(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(IEnumerable<{type.Name}ResponseDTO>), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.Get, Name = {thenamespace}APIEndPoints.{type.Name}.Get)]" +
               $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Get( CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $"{GeneralClass.newlinepad(12)}return (await _sender.Send(new GetAll{type.Name}Query(), cancellationToken))" +
               $"{GeneralClass.newlinepad(12)}.Match<IActionResult>(Left: errors => new OkObjectResult(errors)," +
               $"{GeneralClass.newlinepad(16)}Right: result => new OkObjectResult(Get{type.Name}ResponseResult(result)));" +
               $"{GeneralClass.newlinepad(8)}}}" +
                $"\n" +
               $"{GeneralClass.newlinepad(8)}private IEnumerable<{type.Name}ResponseDTO> Get{type.Name}ResponseResult(IEnumerable<Application{type.Name}ResponseDTO> result)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $" throw new NotImplementedException(\"Please implement like below\");" +
               $"// return result.Select(x => new ModelTypeResponseDTO(x.ModelTypesId, x.ModelTypesName, CovertToModelTypeResponse(x.Models)));" +
               $"{GeneralClass.newlinepad(8)}}}";


        }

        private static string ProduceControllerUpdate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpPut(template: {thenamespace}APIEndPoints.{type.Name}.Update, Name = {thenamespace}APIEndPoints.{type.Name}.Update)]" +
               $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Update({type.Name}UpdateDTO request,, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $"{GeneralClass.newlinepad(12)}var dto = new Application{type.Name}UpdateDTO(request.);" +
               $"\n" +
               $"{GeneralClass.newlinepad(12)}return dto.EnsureInputIsNotEmpty(\"Input Cannot be Empty\")" +
               $"{GeneralClass.newlinepad(16)}.Bind<Either<GeneralFailure, int>>(modelType => UpdateModelType(dto, cancellationToken).Result)" +
               $"{GeneralClass.newlinepad(16)}.Match<IActionResult>(Left: errors => new OkObjectResult(errors)," +
               $"{GeneralClass.newlinepad(20)} Right: result => result.Match<IActionResult>(" +
               $"{GeneralClass.newlinepad(20)} Left: errors2 => new OkObjectResult(errors2)," +
               $"{GeneralClass.newlinepad(20)} Right: result2 => Created($\"/{{{thenamespace}APIEndPoints.{type.Name}.Create}}/{{dto.}}\", dto)));\r\n        }}" +
               $"{GeneralClass.newlinepad(8)} }}" +
               $"\n" +
               $"{GeneralClass.newlinepad(8)}private async Task<Either<GeneralFailure, int>> Update{type.Name}(Application{type.Name}UpdateDTO updateType, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}=> await _sender.Send(new Update{type.Name}Command(updateType), cancellationToken);";

        }

        private static string ProduceControllerDelete(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpDelete(template: {thenamespace}APIEndPoints.{type.Name}.Delete, Name = {thenamespace}APIEndPoints.{type.Name}.Delete)]" +
                   $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Delete([FromRoute] Guid guid, CancellationToken cancellationToken)" +
                   $"{GeneralClass.newlinepad(8)}{{" +
                   $"{GeneralClass.newlinepad(12)}return (await _sender.Send(new Delete{type.Name}Command(new Application{type.Name}DeleteDTO(guid)), cancellationToken))" +
                   $"{GeneralClass.newlinepad(12)}.Match<IActionResult>(Left: errors => new OkObjectResult(errors)," +
                   $"{GeneralClass.newlinepad(16)}Right: result => new OkObjectResult(result));" +
                   $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string ProduceControllerCreate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpPost(template: {thenamespace}APIEndPoints.{type.Name}.Create, Name = {thenamespace}APIEndPoints.{type.Name}.Create)]" +
                 $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Create({type.Name}CreateDTO request, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(8)}{{" +
                 $"{GeneralClass.newlinepad(12)}var dto = new Application{type.Name}CreateDTO(request.);" +
                 $"\n" +
                 $"{GeneralClass.newlinepad(12)}return dto.EnsureInputIsNotEmpty(\"Input Cannot be Empty\")" +
                 $"{GeneralClass.newlinepad(16)}.Bind<Either<GeneralFailure, int>>(_ => (  Create{type.Name}(dto, cancellationToken).Result   ) )" +
                 $"{GeneralClass.newlinepad(16)}.Match<IActionResult>(Left: errors => new OkObjectResult(errors)," +
                 $"{GeneralClass.newlinepad(20)}Right: result => result.Match<IActionResult>(" +
                 $"{GeneralClass.newlinepad(20)}Left: errors2 => new OkObjectResult(errors2)," +
                 $"Right: result2 => Created($\"/{{{thenamespace}APIEndPoints.{type.Name}.Create}}/{{dto}}\", dto)));" +
                 $"{GeneralClass.newlinepad(8)}}}" +
                 $"\n" +
                 $"{GeneralClass.newlinepad(8)}private async Task<Either<GeneralFailure, int>> Create{type.Name}(Application{type.Name}CreateDTO createType, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(8)}=> await _sender.Send(new Create{type.Name}Command(createType), cancellationToken);";
        }

        private static string ProduceControllerConstructor(string controllername)
        {
            return $"{GeneralClass.newlinepad(8)}public {controllername}(ILogger<{controllername}> logger, ISender sender) : base(logger, sender){{}}";
        }

        private static string ProduceControllerHeader(string name_space, Type type, string controllerversion, string controllername)
        {
            return ($"using {name_space}..Api.Extentions;\n" +
                $"using {name_space}.Application.Contracts.RequestDTO;\n" +
                $"using {name_space}.Application.Contracts.ResponseDTO;\n" +
                $"using {name_space}.Application.CQRS.{type.Name}.Commands;\n" +
                $"using {name_space}.Application.CQRS.{type.Name}.Queries;\n" +
                $"using {name_space}.Contracts.RequestDTO;\n" +
                $"using {name_space}.Contracts.ResponseDTO;\n" +
                $"using {name_space}.Domain.Errors;\n" +
                $"using LanguageExt;\n" +
                $"using  MediatR;\n" +
                $"using Microsoft.AspNetCore.Mvc;\n" +
                $"using System.Linq;\n" +
                $"using System.Threading;\n" +
                $"namespace {name_space}.Api.Controllers.{controllerversion}\n" +
                $"{{{GeneralClass.newlinepad(4)}public  class {controllername}  : DVBaseController<{controllername}>" +
                $"{GeneralClass.newlinepad(4)}{{");

        }
    }
}
