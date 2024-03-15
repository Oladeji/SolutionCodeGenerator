
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateCQRSCommandClass
    {
        //public static string GenerateCreateCommand(Type type, string name_space)
        //{
        //    var Output = new StringBuilder();
        //    Output.Append(ProduceCreateCommandHeader(name_space, type.Name));
        //    Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
        //    return Output.ToString();
        //}
        //public static string GenerateDeleteCommand(Type type, string name_space)
        //{
        //    var Output = new StringBuilder();
        //    Output.Append(ProduceDeleteCommandHeader(name_space, type.Name));
        //    Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
        //    return Output.ToString();
        //}

        //public static string GenerateUpdate(Type type, string name_space)
        //{
        //    var Output = new StringBuilder();
        //    Output.Append(ProduceUpdateCommandHeader(name_space, type.Name));
        //    Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
        //    return Output.ToString();
        //}

        public static string GenerateCQRSCommand(Type type, string name_space, Func<string, string, string> produceheader)
        {
            var Output = new StringBuilder();
            Output.Append(produceheader(name_space, type.Name));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }
        public static string ProduceCreateCommandHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Contracts.RequestDTO;\n" +
                   $"using {name_space}.Domain.Errors;\nusing {name_space}.DomainBase.Result;\nusing MediatR;\n" +
            // $"namespace {name_space}.Application.CQRS.{entityName}.Commands\n{{{GeneralClass.newlinepad(4)}public  record Create{entityName}Command({entityName}CreateRequestDTO  Create{entityName}DTO) :  IRequest<Result<GeneralFailure, Guid>>;");
            $"namespace {name_space}.Application.CQRS\n{{{GeneralClass.newlinepad(4)}" +
            $"public  record Create{entityName}Command({entityName}CreateRequestDTO  Create{entityName}DTO) :  IRequest<Result<GeneralFailure, Guid>>;");

        }


        public static string ProduceDeleteCommandHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Contracts.RequestDTO;\n" +
         $"using {name_space}.Domain.Errors;\nusing {name_space}.DomainBase.Result;\nusing MediatR;\n" +
         //$"namespace {name_space}.Application.CQRS.{entityName}.Commands\n" +
         $"namespace {name_space}.Application.CQRS\n" +
         $"{{{GeneralClass.newlinepad(4)}public  record Delete{entityName}Command({entityName}DeleteRequestDTO  Delete{entityName}DTO) :  IRequest<Result<GeneralFailure, int>>;");

        }



        public static string ProduceUpdateCommandHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Contracts.RequestDTO;\n" +
             $"using {name_space}.Domain.Errors;\nusing {name_space}.DomainBase.Result;\nusing MediatR;\n" +
             //$"namespace {name_space}.Application.CQRS.{entityName}.Commands\n" +
             $"namespace {name_space}.Application.CQRS\n" +
             $"{{{GeneralClass.newlinepad(4)}public  record Update{entityName}Command({entityName}UpdateRequestDTO  Update{entityName}DTO) :  IRequest<Result<GeneralFailure, int>>;");

        }
    }
}