
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateApplicationResponseDTOClass
    {
        public static string GenerateResponse(Type type, string name_space,string apiVersion)
        {
            var Output = new StringBuilder();
            Output.Append(GenerateResponseHeader(name_space, type.Name, apiVersion));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        private static string GenerateResponseHeader(object name_space, string entityName,string apiVersion)
        {
            return ($"namespace {name_space}.Application.Contracts.ResponseDTO.V{apiVersion}\n{{" +
                $"{GeneralClass.newlinepad(4)}public  record Application{entityName}ResponseDTO(Object Value);" +

                $"");
        }
    }
}