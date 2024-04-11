
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateCQRSQueryClass
    {

        public static string GenerateCQRSQuery(Type type, string name_space,string apiVersion)
        {
            var Output = new StringBuilder();
            Output.Append(ProduceHeader(name_space, type.Name, apiVersion));
            Output.Append(ProduceGetQuery(name_space, type.Name));
            Output.Append(ProduceGetQueryByGuid(name_space, type.Name));
            Output.Append(ProduceGetQueryById(name_space, type.Name));
            Output.Append(ProduceGetAllQuery(name_space, type.Name)); // This is Get
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }


        public static string ProduceHeader(string name_space, string entityName,string apiVersion)
        {
            return ($"using {name_space}.Contracts.RequestDTO.V{apiVersion};\n" +
                $"using {name_space}.Contracts.ResponseDTO.V{apiVersion};\n" +
               $"using {name_space}.Domain.Errors;\nusing LanguageExt;\nusing MediatR;\n" +
               //$"namespace {name_space}.Application.CQRS.{entityName}.Queries" +
               $"namespace {name_space}.Application.CQRS" +
               $"{{");

        }
        public static string ProduceGetQuery(string name_space, string entityName)
        {
            return ($"{GeneralClass.newlinepad(4)}public  record Get{entityName}Query({entityName}GetRequestDTO  Request{entityName}DTO) :  IRequest<Either<GeneralFailure, {entityName}ResponseDTO>>;");

        }


        public static string ProduceGetQueryByGuid(string name_space, string entityName)
        {
            return ($"{GeneralClass.newlinepad(4)}public  record Get{entityName}ByGuidQuery({entityName}GetRequestByGuidDTO  Request{entityName}DTO) :  IRequest<Either<GeneralFailure, {entityName}ResponseDTO>>;");

        }

        public static string ProduceGetQueryById(string name_space, string entityName)
        {
            return (
               $"{GeneralClass.newlinepad(4)}public  record Get{entityName}ByIdQuery({entityName}GetRequestByIdDTO  Request{entityName}DTO) :  IRequest<Either<GeneralFailure, {entityName}ResponseDTO>>;");

        }




        public static string ProduceGetAllQuery(string name_space, string entityName)
        {
            return ($"{GeneralClass.newlinepad(4)}public  record GetAll{entityName}Query :  IRequest<Either<GeneralFailure, IEnumerable<{entityName}ResponseDTO>>>;");

        }


    }
}