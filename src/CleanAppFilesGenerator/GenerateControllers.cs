﻿
using System.Text;


namespace CleanAppFilesGenerator
{
    public class GenerateControllers
    {

        
        public static string Generate_NoMeadiatr(Type type, string thenamespace, string apiVersion)
        {

            // var entityName = type.Name;
            var Output = new StringBuilder();
            string controllername = type.Name + "sController";
            Output.Append(ProduceControllerHeader_NoMeadiatr(thenamespace, type, apiVersion, controllername));
            Output.Append($"\n");
            Output.Append(ProduceControllerConstructor_NoMeadiatr(controllername,type));
            Output.Append($"\n");
            Output.Append(ProduceControllerGet_NoMeadiatr(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerGetById_NoMeadiatr(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerGetByJSONUsingBody_NoMeadiatr(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerCreate_NoMeadiatr(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerUpdate_NoMeadiatr(thenamespace, type));
            Output.Append($"\n");
            Output.Append(ProduceControllerDelete_NoMeadiatr(thenamespace, type));
            Output.Append($"\n");
            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }
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
               $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof({type.Name}ResponseDTO), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.QueryString, Name = {thenamespace}APIEndPoints.{type.Name}.QueryString)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> QueryString([FromBody] {type.Name}GetRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(16)}=> ( _sender.Send(new Get{type.Name}Query(request), cancellationToken)) .ToEitherActionResult();";
        }
        private static string ProduceControllerGetByJSONUsingBody_NoMeadiatr(string thenamespace, Type type)
        {
            return
               $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof({type.Name}ResponseDTO), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.QueryString, Name = {thenamespace}APIEndPoints.{type.Name}.QueryString)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> QueryString([FromBody] {type.Name}GetRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(16)}=> ( _get{type.Name}QueryHandler.Handle(new Get{type.Name}Query(request), cancellationToken)) .ToEitherActionResult();";
        }

        private static string ProduceControllerGetById_NoMeadiatr(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof({type.Name}ResponseDTO), StatusCodes.Status200OK)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status404NotFound)]" +
            $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.GetById, Name = {thenamespace}APIEndPoints.{type.Name}.GetById)]" +
            $"{GeneralClass.newlinepad(8)}public Task<IActionResult> GetById([FromRoute] string NameOrGuid, CancellationToken cancellationToken)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}return Guid.TryParse(NameOrGuid, out Guid guid)  ?" +
            $"{GeneralClass.newlinepad(16)}(_get{type.Name}ByGuidQueryHandler .Handle(new Get{type.Name}ByGuidQuery(new {type.Name}GetRequestByGuidDTO(guid)), cancellationToken)).ToEitherActionResult()" +
            $"{GeneralClass.newlinepad(16)}:" +
            $"{GeneralClass.newlinepad(16)}(_get{type.Name}ByIdQueryHandler.Handle(new Get{type.Name}ByIdQuery(new {type.Name}GetRequestByIdDTO(NameOrGuid)), cancellationToken)).ToEitherActionResult();" +
            $"{GeneralClass.newlinepad(8)}}}";
        }
        private static string ProduceControllerGetById(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof({type.Name}ResponseDTO), StatusCodes.Status200OK)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status404NotFound)]" +
            $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.GetById, Name = {thenamespace}APIEndPoints.{type.Name}.GetById)]" +
            $"{GeneralClass.newlinepad(8)}public Task<IActionResult> GetById([FromRoute] string NameOrGuid, CancellationToken cancellationToken)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}return Guid.TryParse(NameOrGuid, out Guid guid)  ?" +
            $"{GeneralClass.newlinepad(16)}(_sender.Send(new Get{type.Name}ByGuidQuery(new {type.Name}GetRequestByGuidDTO(guid)), cancellationToken)).ToEitherActionResult()" +
            $"{GeneralClass.newlinepad(16)}:" +
            $"{GeneralClass.newlinepad(16)}(_sender.Send(new Get{type.Name}ByIdQuery(new {type.Name}GetRequestByIdDTO(NameOrGuid)), cancellationToken)).ToEitherActionResult();" +
            $"{GeneralClass.newlinepad(8)}}}";
        }
        //_NoMeadiatr
        private static string ProduceControllerGet(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(IEnumerable<{type.Name}ResponseDTO>), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.Get, Name = {thenamespace}APIEndPoints.{type.Name}.Get)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Get(CancellationToken cToken) => _sender.Send(new GetAll{type.Name}Query(), cToken).ToActionResult();";

        }
        private static string ProduceControllerGet_NoMeadiatr(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(typeof(IEnumerable<{type.Name}ResponseDTO>), StatusCodes.Status200OK)]" +
               $"{GeneralClass.newlinepad(8)}[HttpGet(template: {thenamespace}APIEndPoints.{type.Name}.Get, Name = {thenamespace}APIEndPoints.{type.Name}.Get)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Get(CancellationToken cToken) => _getAll{type.Name}QueryHandler.Handle(new GetAll{type.Name}Query(), cToken).ToActionResult();";

        }

        private static string ProduceControllerUpdate_NoMeadiatr(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status204NoContent)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status400BadRequest)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status409Conflict)]" +
                $"{GeneralClass.newlinepad(8)}[HttpPut(template: {thenamespace}APIEndPoints.{type.Name}.Update, Name = {thenamespace}APIEndPoints.{type.Name}.Update)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Update({type.Name}UpdateRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(12)}=> (_update{type.Name}CommandHandler.Handle(new Update{type.Name}Command(request), cancellationToken)) .ToActionResultCreated($\"{{{thenamespace}APIEndPoints.{type.Name}.Create}}\", request);";
        }

        private static string ProduceControllerUpdate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status204NoContent)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status400BadRequest)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status409Conflict)]" +
                $"{GeneralClass.newlinepad(8)}[HttpPut(template: {thenamespace}APIEndPoints.{type.Name}.Update, Name = {thenamespace}APIEndPoints.{type.Name}.Update)]" +
               $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Update({type.Name}UpdateRequestDTO request, CancellationToken cancellationToken)" +
               $"{GeneralClass.newlinepad(12)}=> (_sender.Send(new Update{type.Name}Command(request), cancellationToken)) .ToActionResultCreated($\"{{{thenamespace}APIEndPoints.{type.Name}.Create}}\", request);";
        }


        private static string ProduceControllerDelete_NoMeadiatr(string thenamespace, Type type)
        {
            return $"\n{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status200OK)]" +
                   $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status400BadRequest)]" +
                   $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status404NotFound)]" +
                    $"{GeneralClass.newlinepad(8)}[HttpDelete(template: {thenamespace}APIEndPoints.{type.Name}.Delete, Name = {thenamespace}APIEndPoints.{type.Name}.Delete)]" +
                   $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Delete([FromRoute] Guid request, CancellationToken cancellationToken)" +
                   $"{GeneralClass.newlinepad(12)}=>_delete{type.Name}CommandHandler.Handle(new Delete{type.Name}Command(new {type.Name}DeleteRequestDTO(request)), cancellationToken).ToActionResult();";

        }

        private static string ProduceControllerDelete(string thenamespace, Type type)
        {
            return $"\n{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status200OK)]" +
                   $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status400BadRequest)]" +
                   $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status404NotFound)]" +
                    $"{GeneralClass.newlinepad(8)}[HttpDelete(template: {thenamespace}APIEndPoints.{type.Name}.Delete, Name = {thenamespace}APIEndPoints.{type.Name}.Delete)]" +
                   $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Delete([FromRoute] Guid request, CancellationToken cancellationToken)" +
                   $"{GeneralClass.newlinepad(12)}=>_sender.Send(new Delete{type.Name}Command(new {type.Name}DeleteRequestDTO(request)), cancellationToken).ToActionResult();";

        }

        private static string ProduceControllerCreate(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status201Created)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status400BadRequest)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status409Conflict)]" +
                 $"{GeneralClass.newlinepad(8)}[HttpPost(template: {thenamespace}APIEndPoints.{type.Name}.Create, Name = {thenamespace}APIEndPoints.{type.Name}.Create)]" +
                 $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Create({type.Name}CreateRequestDTO request, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(12)} => (_sender.Send(new Create{type.Name}Command(request), cancellationToken)).ToActionResultCreated($\"{{{thenamespace}APIEndPoints.{type.Name}.Create}}\", request);";
        }
        private static string ProduceControllerCreate_NoMeadiatr(string thenamespace, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status201Created)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status400BadRequest)]" +
                $"{GeneralClass.newlinepad(8)}[ProducesResponseType(StatusCodes.Status409Conflict)]" +
                 $"{GeneralClass.newlinepad(8)}[HttpPost(template: {thenamespace}APIEndPoints.{type.Name}.Create, Name = {thenamespace}APIEndPoints.{type.Name}.Create)]" +
                 $"{GeneralClass.newlinepad(8)}public Task<IActionResult> Create({type.Name}CreateRequestDTO request, CancellationToken cancellationToken)" +
                 $"{GeneralClass.newlinepad(12)} => (_create{type.Name}CommandHandler.Handle(new Create{type.Name}Command(request), cancellationToken)).ToActionResultCreated($\"{{{thenamespace}APIEndPoints.{type.Name}.Create}}\", request);";
        }
        private static string ProduceControllerConstructor(string controllername)
        {
            return $"{GeneralClass.newlinepad(8)}public {controllername}(ILogger<{controllername}> logger, ISender sender) : base(logger, sender){{}}";
        }

       

        private static string ProduceControllerConstructor_NoMeadiatr(string controllername, Type type)
        {
            return $"{GeneralClass.newlinepad(8)}public {controllername}(ILogger<{controllername}> logger ,  IGetAll{type.Name}QueryHandler getAll{type.Name}QueryHandler," +
                $" IGet{type.Name}ByIdQueryHandler get{type.Name}ByIdQueryHandler ,  " +
                  $" IGet{type.Name}QueryHandler get{type.Name}QueryHandler ,  " +
                $"IGet{type.Name}ByGuidQueryHandler get{type.Name}ByGuidQueryHandler , " +
                $"ICreate{type.Name}CommandHandler create{type.Name}CommandHandler ," +
                $" IUpdate{type.Name}CommandHandler update{type.Name}CommandHandler ," +
                $"IDelete{type.Name}CommandHandler delete{type.Name}CommandHandler ) : base(logger){{" +
                $"{GeneralClass.newlinepad(12)}_getAll{type.Name}QueryHandler = getAll{type.Name}QueryHandler ;\n" +
                  $"{GeneralClass.newlinepad(12)}_get{type.Name}QueryHandler = get{type.Name}QueryHandler ;\n" +
                 $"{GeneralClass.newlinepad(12)} _get{type.Name}ByIdQueryHandler =get{type.Name}ByIdQueryHandler  ;\n" +
                 $"{GeneralClass.newlinepad(12)} _get{type.Name}ByGuidQueryHandler =get{type.Name}ByGuidQueryHandler  ;\n" +
                 $"{GeneralClass.newlinepad(12)}_create{type.Name}CommandHandler =create{type.Name}CommandHandler  ;\n" +
                 $"{GeneralClass.newlinepad(12)} _update{type.Name}CommandHandler =update{type.Name}CommandHandler ;\n" +
                 $"{GeneralClass.newlinepad(12)}_delete{type.Name}CommandHandler =delete{type.Name}CommandHandler ;\n" +
                $"}}";
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

       
       
        
       
       
       
        private static string ProduceControllerHeader_NoMeadiatr(string name_space, Type type, string apiVersion, string controllername)
        {
            return ($"using {name_space}.Api.Extensions;\n" +

                $"using {name_space}.Application.CQRS;\n" +
                $"using Asp.Versioning;\n" +
                //$"using {name_space}.Application.CQRS.{type.Name}.Commands;\n" +
                $"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                $"using {name_space}.Contracts.ResponseDTO.V{apiVersion};\n" +
                $"using GlobalConstants;\n" +
               
                $"using Microsoft.AspNetCore.Mvc;\n" +
                $"namespace {name_space}.Api.Controllers.V{apiVersion}\n" +
                $"{{{GeneralClass.newlinepad(4)} [ApiVersion({apiVersion})]" +
                $"{GeneralClass.newlinepad(4)}public  class {controllername}  : TheBaseController<{controllername}>" +
                $"{GeneralClass.newlinepad(4)}{{" +
                    $"{GeneralClass.newlinepad(8)}  private readonly IGet{type.Name}QueryHandler _get{type.Name}QueryHandler;" +
                  $"{GeneralClass.newlinepad(8)}  private readonly IGetAll{type.Name}QueryHandler _getAll{type.Name}QueryHandler;" +
                  $"{GeneralClass.newlinepad(8)}  private readonly IGet{type.Name}ByIdQueryHandler _get{type.Name}ByIdQueryHandler;" +
                  $"{GeneralClass.newlinepad(8)}  private readonly IGet{type.Name}ByGuidQueryHandler _get{type.Name}ByGuidQueryHandler;" +
                  $"{GeneralClass.newlinepad(8)}  private readonly ICreate{type.Name}CommandHandler _create{type.Name}CommandHandler;" +
                  $"{GeneralClass.newlinepad(8)}  private readonly IUpdate{type.Name}CommandHandler _update{type.Name}CommandHandler;" +
                 $"{GeneralClass.newlinepad(8)}   private readonly IDelete{type.Name}CommandHandler _delete{type.Name}CommandHandler;" +
                  $"{GeneralClass.newlinepad(8)} " +
              $"");

        }
        
    }
}
