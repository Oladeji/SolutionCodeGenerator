
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GeneratResponseDTOClass
    {
        public static string GenerateResponse(Type type, string name_space)
        {
            var Output = new StringBuilder();
            Output.Append(GenerateResponseHeader(name_space, type.Name));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        private static string  GenerateResponseHeader(object name_space, string entityName)
        {
            return ($"namespace {name_space}.Application.Contracts.ResponseDTO\n{{" +
                $"{GeneralClass.newlinepad(4)}public  record ApplicationResponse{entityName}DTO();" +

                $"");
        }
    }
}