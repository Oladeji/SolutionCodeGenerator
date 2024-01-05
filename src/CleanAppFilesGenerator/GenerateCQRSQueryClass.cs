
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateCQRSQueryClass
    {

        public static string GenerateCQRSQuery(Type type, string name_space)
        {
            var Output = new StringBuilder();
            Output.Append(ProduceHeader(name_space, type.Name));
            Output.Append(ProduceGetQuery(name_space, type.Name));
            Output.Append(ProduceGetQueryByGuid(name_space, type.Name));
            Output.Append(ProduceGetQueryById(name_space, type.Name));
            Output.Append(ProduceGetAllQuery(name_space, type.Name)); // This is Get
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }


        public static string ProduceHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Application.Contracts.RequestDTO;\n" +
                $"using {name_space}.Application.Contracts.ResponseDTO;\n" +
               $"using {name_space}.Domain.Errors;\nusing LanguageExt;\nusing MediatR;\n" +
               $"namespace {name_space}.Application.CQRS.{entityName}.Queries" +
               $"{{");

        }
        public static string ProduceGetQuery(string name_space, string entityName)
        {
            return ($"{GeneralClass.newlinepad(4)}public  record Get{entityName}Query(Application{entityName}GetRequestDTO  Request{entityName}DTO) :  IRequest<Either<GeneralFailure, Application{entityName}ResponseDTO>>;");

        }


        public static string ProduceGetQueryByGuid(string name_space, string entityName)
        {
            return ($"{GeneralClass.newlinepad(4)}public  record Get{entityName}ByGuidQuery(Application{entityName}GetRequestByGuidDTO  Request{entityName}DTO) :  IRequest<Either<GeneralFailure, Application{entityName}ResponseDTO>>;");

        }

        public static string ProduceGetQueryById(string name_space, string entityName)
        {
            return (
               $"{GeneralClass.newlinepad(4)}public  record Get{entityName}ByIdQuery(Application{entityName}GetRequestByIdDTO  Request{entityName}DTO) :  IRequest<Either<GeneralFailure, Application{entityName}ResponseDTO>>;");

        }




        public static string ProduceGetAllQuery(string name_space, string entityName)
        {
            return ($"{GeneralClass.newlinepad(4)}public  record GetAll{entityName}Query :  IRequest<Either<GeneralFailure, IEnumerable<Application{entityName}ResponseDTO>>>;");

        }


    }
}