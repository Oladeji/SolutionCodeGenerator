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
               $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> GetByJSONBody([FromBody] {type.Name}GetRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $"{GeneralClass.newlinepad(12)}var x = request.EnsureInputIsNotNull(\"Input Cannot be null\");" +
               $"{GeneralClass.newlinepad(12)}return (await _sender.Send(new Get{type.Name}Query(new Application{type.Name}GetRequestDTO(request)), cancellationToken))" +
               $"{GeneralClass.newlinepad(12)}.Match<IActionResult>(Left: errors => new NotFoundObjectResult(errors)," +
               $"{GeneralClass.newlinepad(16)}Right: result => new OkObjectResult(MapApplication{type.Name}ResponseDTO_To_{type.Name}ResponseDTO(result)));" +
               $"{GeneralClass.newlinepad(8)}}}";

        }

        private static string ProduceControllerGetById(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof({type.Name}ResponseDTO), StatusCodes.Status200OK)]" +
            $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.GetById, Name = {thenamespace}APIEndPoints.{type.Name}.GetById)]" +
            $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> GetById([FromRoute] string NameOrGuid, CancellationToken cancellationToken)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}var x = NameOrGuid.EnsureInputIsNotEmpty(\"Input Cannot be null\");" +
            $"{GeneralClass.newlinepad(12)}var result = Guid.TryParse(NameOrGuid, out Guid guid);" +
            $"{GeneralClass.newlinepad(12)}if (result)" +
            $"{GeneralClass.newlinepad(12)}{{" +
            $"{GeneralClass.newlinepad(16)}var {type.Name}RequestByIdDTO = new {type.Name}GetRequestByGuidDTO(guid);" +
            $"{GeneralClass.newlinepad(16)}return (await _sender.Send(new Get{type.Name}ByGuidQuery(new Application{type.Name}GetRequestByGuidDTO({type.Name}RequestByIdDTO)), cancellationToken))" +
            $"{GeneralClass.newlinepad(16)}.Match<IActionResult>(Left: errors => new NotFoundObjectResult(errors)," +
            $"{GeneralClass.newlinepad(20)}Right: result => new OkObjectResult(MapApplication{type.Name}ResponseDTO_To_{type.Name}ResponseDTO(result)));" +
            $"{GeneralClass.newlinepad(12)}}}" +
            $"{GeneralClass.newlinepad(12)}else" +
            $"{GeneralClass.newlinepad(12)}{{" +
            $"{GeneralClass.newlinepad(16)}var {type.Name}RequestByIdDTO = new {type.Name}GetRequestByIdDTO(NameOrGuid);" +
            $"{GeneralClass.newlinepad(16)}return (await _sender.Send<Either<GeneralFailure, Application{type.Name}ResponseDTO>>(new Get{type.Name}ByIdQuery(new Application{type.Name}GetRequestByIdDTO({type.Name}RequestByIdDTO)), cancellationToken))" +
            $"{GeneralClass.newlinepad(16)}.Match<IActionResult>(Left: errors => new NotFoundObjectResult(errors)," +
            $"{GeneralClass.newlinepad(20)}Right: result => new OkObjectResult(MapApplication{type.Name}ResponseDTO_To_{type.Name}ResponseDTO(result)));" +
            $"{GeneralClass.newlinepad(12)}}}" +
            $"{GeneralClass.newlinepad(8)}}}" +
            $"\n" +
            $"{GeneralClass.newlinepad(8)}private {type.Name}ResponseDTO MapApplication{type.Name}ResponseDTO_To_{type.Name}ResponseDTO(Application{type.Name}ResponseDTO result)" +
            $"{GeneralClass.newlinepad(8)}=> throw new NotImplementedException(\"Please implement like below\");" +
            $"{GeneralClass.newlinepad(8)}// => new ModelTypeResponseDTO(result.ModelTypesId, result.ModelTypesName, CovertToModelResponse(result.Models));" +
            $"\n" +
            $"{GeneralClass.newlinepad(8)} private ICollection<ModelResponseDTO> CovertToModelResponse(ICollection<ApplicationModelResponseDTO> models)" +
            $"{GeneralClass.newlinepad(8)}=> throw new NotImplementedException(\"Please implement like below\");" +
            $"{GeneralClass.newlinepad(8)}// => models.Select(x => new ModelResponseDTO(x.ModelId, x.ModelName, x.ModelTypesName)).ToList();";



        }

        private static string ProduceControllerGet(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(IEnumerable<{type.Name}ResponseDTO>), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.Get, Name = {thenamespace}APIEndPoints.{type.Name}.Get)]" +
               $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Get( CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $"{GeneralClass.newlinepad(12)} return (await _sender.Send<Either<GeneralFailure, IEnumerable<Application{type.Name}ResponseDTO>>>(new GetAll{type.Name}Query(), cancellationToken))" +
               $"{GeneralClass.newlinepad(12)}.Match<IActionResult>(Left: errors => new BadRequestObjectResult(errors)," +
               $"{GeneralClass.newlinepad(16)}Right: result => new OkObjectResult(Get{type.Name}ResponseResult(result)));" +
               $"{GeneralClass.newlinepad(8)}}}" +
                $"\n" +
               $"{GeneralClass.newlinepad(8)}private IEnumerable<{type.Name}ResponseDTO> Get{type.Name}ResponseResult(IEnumerable<Application{type.Name}ResponseDTO> result)" +
               $"{GeneralClass.newlinepad(8)}" +
               $"{GeneralClass.newlinepad(8)}=> throw new NotImplementedException(\"Please implement like below\");" +
               $"{GeneralClass.newlinepad(8)}//=> result.Select(x => new ModelTypeResponseDTO(x.ModelTypesId, x.ModelTypesName, Covert{type.Name}Response(x.Models)));" +
               $"{GeneralClass.newlinepad(8)}" +
               $"\n" +
               $"{GeneralClass.newlinepad(8)}private ICollection<ModelResponseDTO> CovertModelTypeResponse(ICollection<ApplicationModelResponseDTO> models)" +
               $"{GeneralClass.newlinepad(8)}=> throw new NotImplementedException(\"Please implement like below\");" +
               $"{GeneralClass.newlinepad(8)}//=> models.Select(x => new ModelResponseDTO(x.ModelId, x.ModelName, x.ModelTypesName)).ToList();";


        }

        private static string ProduceControllerUpdate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpPut(template: {thenamespace}APIEndPoints.{type.Name}.Update, Name = {thenamespace}APIEndPoints.{type.Name}.Update)]" +
               $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Update({type.Name}UpdateRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $"{GeneralClass.newlinepad(12)}var dto = new Application{type.Name}UpdateRequestDTO(request);" +
               $"\n" +
               $"{GeneralClass.newlinepad(12)}return dto.EnsureInputIsNotEmpty(\"Input Cannot be Empty\")" +
               $"{GeneralClass.newlinepad(16)}.Bind<Either<GeneralFailure, int>>(modelType => Update{type.Name}(dto, cancellationToken).Result)" +
               $"{GeneralClass.newlinepad(16)}.Match<IActionResult>(Left: errors => new BadRequestObjectResult(errors)," +
               $"{GeneralClass.newlinepad(20)} Right: result => result.Match<IActionResult>(" +
               $"{GeneralClass.newlinepad(20)} Left: errors2 => new     BadRequestObjectResult(errors2)," +
               $"{GeneralClass.newlinepad(20)} Right: result2 => Created($\"/{{{thenamespace}APIEndPoints.{type.Name}.Create}}/{{dto}}\", dto)));\r\n" +
               $"{GeneralClass.newlinepad(8)} }}" +
               $"\n" +
               $"{GeneralClass.newlinepad(8)}private async Task<Either<GeneralFailure, int>> Update{type.Name}(Application{type.Name}UpdateRequestDTO updateType, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}=> await _sender.Send(new Update{type.Name}Command(updateType), cancellationToken);";

        }

        private static string ProduceControllerDelete(string thenamespace, Type type)
        {
            return $"\n{GeneralClass.newlinepad(8)}[HttpDelete(template: {thenamespace}APIEndPoints.{type.Name}.Delete, Name = {thenamespace}APIEndPoints.{type.Name}.Delete)]" +
                   $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Delete([FromRoute] Guid request, CancellationToken cancellationToken)" +
                   $"{GeneralClass.newlinepad(8)}{{" +
                   $"{GeneralClass.newlinepad(8)}var result = new {type.Name}DeleteRequestDTO(request);" +
                   $"{GeneralClass.newlinepad(8)}var guid = new Application{type.Name}DeleteRequestDTO(result);" +
                   $"{GeneralClass.newlinepad(8)}return guid.EnsureInputIsNotEmpty(\"Input Cannot be null\")" +
                   $"{GeneralClass.newlinepad(12)}.Bind<Either<GeneralFailure, int>>(guid => Delete{type.Name}(guid, cancellationToken).Result)" +
                   $"{GeneralClass.newlinepad(12)}.Match<IActionResult>(Left: errors => new BadRequestObjectResult(errors)," +
                   $"{GeneralClass.newlinepad(16)}Right: result => new OkObjectResult(result));" +
                   $"{GeneralClass.newlinepad(8)}}}" +
                   $"\n" +
               $"{GeneralClass.newlinepad(8)}private async Task<Either<GeneralFailure, int>> Delete{type.Name}(Application{type.Name}DeleteRequestDTO dto, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(8)}=>  await _sender.Send(new Delete{type.Name}Command(dto), cancellationToken);";

        }

        private static string ProduceControllerCreate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[HttpPost(template: {thenamespace}APIEndPoints.{type.Name}.Create, Name = {thenamespace}APIEndPoints.{type.Name}.Create)]" +
                 $"{GeneralClass.newlinepad(8)}public async Task<IActionResult> Create({type.Name}CreateRequestDTO request, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(8)}{{" +
                 $"{GeneralClass.newlinepad(12)}var dto = new Application{type.Name}CreateRequestDTO(request);" +
                 $"\n" +
                 $"{GeneralClass.newlinepad(12)}return dto.EnsureInputIsNotEmpty(\"Input Cannot be Empty\")" +
                 $"{GeneralClass.newlinepad(16)}.Bind<Either<GeneralFailure, Guid>>(_ => (  Create{type.Name}(dto, cancellationToken).Result   ) )" +
                 $"{GeneralClass.newlinepad(16)}.Match<IActionResult>(Left: errors => new BadRequestObjectResult(errors)," +
                 $"{GeneralClass.newlinepad(20)}Right: result => result.Match<IActionResult>(" +
                 $"{GeneralClass.newlinepad(20)}Left: errors2 => new BadRequestObjectResult(errors2)," +
                 $"{GeneralClass.newlinepad(20)}Right: result2 => Created($\"/{{{thenamespace}APIEndPoints.{type.Name}.Create}}/{{result2}}\", dto)));" +
                 $"{GeneralClass.newlinepad(8)}}}" +
                 $"\n" +
                 $"{GeneralClass.newlinepad(8)}private async Task<Either<GeneralFailure, Guid>> Create{type.Name}(Application{type.Name}CreateRequestDTO createType, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(8)}=> await _sender.Send(new Create{type.Name}Command(createType), cancellationToken);";
        }

        private static string ProduceControllerConstructor(string controllername)
        {
            return $"{GeneralClass.newlinepad(8)}public {controllername}(ILogger<{controllername}> logger, ISender sender) : base(logger, sender){{}}";
        }

        private static string ProduceControllerHeader(string name_space, Type type, string controllerversion, string controllername)
        {
            return ($"using {name_space}.Api.Extentions;\n" +
                $"using {name_space}.Application.Contracts.RequestDTO;\n" +
                $"using {name_space}.Application.Contracts.ResponseDTO;\n" +
                $"using {name_space}.Application.CQRS.{type.Name}.Commands;\n" +
                $"using {name_space}.Application.CQRS.{type.Name}.Queries;\n" +
                $"using {name_space}.Contracts.RequestDTO;\n" +
                $"using {name_space}.Contracts.ResponseDTO;\n" +
                $"using {name_space}.Domain.Errors;\n" +
                $"using LanguageExt;\n" +
                $"using MediatR;\n" +
                $"using Microsoft.AspNetCore.Mvc;\n" +
                $"using System.Linq;\n" +
                $"using System.Threading;\n" +
                $"namespace {name_space}.Api.Controllers.{controllerversion}\n" +
                $"{{{GeneralClass.newlinepad(4)}public  class {controllername}  : TheBaseController<{controllername}>" +
                $"{GeneralClass.newlinepad(4)}{{");

        }
    }
}
