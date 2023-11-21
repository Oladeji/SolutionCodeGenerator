
using System.Text;

namespace CleanAppFilesGenerator
{
    public class GeneratRequestDTOClass
    {
        public static string GenerateRequest(Type type, string name_space)
        {
            var Output = new StringBuilder();
            Output.Append(GenerateRequestHeader(name_space, type.Name));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        private static string  GenerateRequestHeader(string name_space, string entityName)
        {
            return ($"namespace {name_space}.Application.Contracts.RequestDTO\n{{" +
                $"{GeneralClass.newlinepad(4)}public  record ApplicationRequest{entityName}DTO();" +
                $"{GeneralClass.newlinepad(4)}public  record ApplicationCreate{entityName}DTO();" +
                $"{GeneralClass.newlinepad(4)}public  record ApplicationUpdate{entityName}DTO();" +
                $"{GeneralClass.newlinepad(4)}public  record ApplicationDelete{entityName}DTO();" +
                $"");

        }
    }
}