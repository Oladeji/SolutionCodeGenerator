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
        public static string Generate(Type type, string thenamespace, string apiVersion)
        {

            // var entityName = type.Name;
            var Output = new StringBuilder();
            string controllername = type.Name + "sController";
            Output.Append(ProduceControllerHeader(thenamespace, type, apiVersion, controllername));
            Output.Append($"\n");
            Output.Append(ProduceControllerConstructor(controllername));
            Output.Append($"\n");
            Output.Append(ProduceControllerGet(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerGetById(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerGetByJSONUsingBody(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerCreate(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerUpdate(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerDelete(thenamespace, type));
            Output.Append($"\n");
            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }
        // throw new NotImplementedException();
        private static string ProduceControllerGetByJSONUsingBody(string thenamespace, Type type)
        {
            return
               $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(ModelTypeResponseDTO), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.GetByJSONBody, Name = {thenamespace}APIEndPoints.{type.Name}.GetByJSONBody)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> GetByJSONBody([FromBody] {type.Name}GetRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(16)}=> ( _sender.Send(new Get{type.Name}Query(request), cancellationToken)) .ToEitherActionResult();";
        }
        private static string ProduceControllerGetById(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof({type.Name}ResponseDTO), StatusCodes.Status200OK)]" +
            $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.GetById, Name = {thenamespace}APIEndPoints.{type.Name}.GetById)]" +
            $"{GeneralClass.newlinepad(8)}public Task<IActionResult> GetById([FromRoute] string NameOrGuid, CancellationToken cancellationToken)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}return Guid.TryParse(NameOrGuid, out Guid guid)  ?" +
            $"{GeneralClass.newlinepad(16)}(_sender.Send(new Get{type.Name}ByGuidQuery(new {type.Name}GetRequestByGuidDTO(guid)), cancellationToken)).ToEitherActionResult()" +
            $"{GeneralClass.newlinepad(16)}:" +
            $"{GeneralClass.newlinepad(16)}(_sender.Send(new Get{type.Name}ByIdQuery(new {type.Name}GetRequestByIdDTO(NameOrGuid)), cancellationToken)).ToEitherActionResult();" +
            $"{GeneralClass.newlinepad(8)}}}";
        }

        private static string ProduceControllerGet(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(IEnumerable<{type.Name}ResponseDTO>), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.Get, Name = {thenamespace}APIEndPoints.{type.Name}.Get)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Get(CancellationToken cToken) => _sender.Send(new GetAll{type.Name}Query(), cToken).ToActionResult();";

        }
        private static string ProduceControllerUpdate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpPut(template: {thenamespace}APIEndPoints.{type.Name}.Update, Name = {thenamespace}APIEndPoints.{type.Name}.Update)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Update({type.Name}UpdateRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(12)}=> (_sender.Send(new Update{type.Name}Command(request), cancellationToken)) .ToActionResultCreated($\"{{{thenamespace}APIEndPoints.{type.Name}.Create}}\", request);";
        }

        private static string ProduceControllerDelete(string thenamespace, Type type)
        {
            return $"\n{GeneralClass.newlinepad(8)}[HttpDelete(template: {thenamespace}APIEndPoints.{type.Name}.Delete, Name = {thenamespace}APIEndPoints.{type.Name}.Delete)]" +
                   $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Delete([FromRoute] Guid request, CancellationToken cancellationToken)" +
                   $"{GeneralClass.newlinepad(12)}=>_sender.Send(new Delete{type.Name}Command(new {type.Name}DeleteRequestDTO(request)), cancellationToken).ToActionResult();";

        }

        private static string ProduceControllerCreate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpPost(template: {thenamespace}APIEndPoints.{type.Name}.Create, Name = {thenamespace}APIEndPoints.{type.Name}.Create)]" +
                 $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Create({type.Name}CreateRequestDTO request, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(12)} => (_sender.Send(new Create{type.Name}Command(request), cancellationToken)).ToActionResultCreated($\"{{{thenamespace}APIEndPoints.{type.Name}.Create}}\", request);";
        }

        private static string ProduceControllerConstructor(string controllername)
        {
            return $"{GeneralClass.newlinepad(8)}public {controllername}(ILogger<{controllername}> logger, ISender sender) : base(logger, sender){{}}";
        }

        private static string ProduceControllerHeader(string name_space, Type type, string apiVersion, string controllername)
        {
            return ($"using {name_space}.Api.Extensions;\n" +

                $"using {name_space}.Application.CQRS;\n" +
                $"using Asp.Versioning;\n" +
                //$"using {name_space}.Application.CQRS.{type.Name}.Commands;\n" +
                $"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                $"using {name_space}.Contracts.ResponseDTO.V{apiVersion};\n" +
                $"using MediatR;\n" +
                $"using {name_space}.Api.Controllers;\n" +
                $"using Microsoft.AspNetCore.Mvc;\n" +
                $"namespace {name_space}.Api.Controllers.V{apiVersion}\n" +
                $"{{{GeneralClass.newlinepad(4)} [ApiVersion({apiVersion})]" +
                $"{GeneralClass.newlinepad(4)}public  class {controllername}  : TheBaseController<{controllername}>" +
                $"{GeneralClass.newlinepad(4)}{{");

        }
    }
}
