
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

        public static string GenerateCQRSCommand(Type type, string name_space,Func<string,string,string> produceheader)
        {
            var Output = new StringBuilder();
            Output.Append(produceheader(name_space, type.Name));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }
        public static string ProduceCreateCommandHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Application.Contracts.RequestDTO;\n" +
                   $"using {name_space}.Domain.Errors;\nusing LanguageExt;\nusing MediatR;\n" +
                   $"namespace {name_space}.Application.CQRS.{entityName}.Commands\n{{{GeneralClass.newlinepad(4)}public  record Create{entityName}Command(ApplicationCreate{entityName}DTO  Create{entityName}DTO) :  IRequest<Either<GeneralFailures, int>>;");

        }


        public static string ProduceDeleteCommandHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Application.Contracts.RequestDTO;\n" +
         $"using {name_space}.Domain.Errors;\nusing LanguageExt;\nusing MediatR;\n" +
         $"namespace {name_space}.Application.CQRS.{entityName}.Commands\n{{{GeneralClass.newlinepad(4)}public  record Delete{entityName}Command(ApplicationDelete{entityName}DTO  Delete{entityName}DTO) :  IRequest<Either<GeneralFailures, int>>;");

        }



        public static string ProduceUpdateCommandHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Application.Contracts.RequestDTO;\n" +
             $"using {name_space}.Domain.Errors;\nusing LanguageExt;\nusing MediatR;\n" +
             $"namespace {name_space}.Application.CQRS.{entityName}.Commands\n{{{GeneralClass.newlinepad(4)}public  record Update{entityName}Command(ApplicationUpdate{entityName}DTO  Update{entityName}DTO) :  IRequest<Either<GeneralFailures, int>>;");

        }
    }
}