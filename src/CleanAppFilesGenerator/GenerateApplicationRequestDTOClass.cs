
using System.Text;

namespace CleanAppFilesGenerator
{
    public class GenerateApplicationRequestDTOClass
    {
        public static string GenerateRequest(Type type, string name_space , string apiVersion)
        {
            var Output = new StringBuilder();
            Output.Append(GenerateRequestHeader(name_space, type, apiVersion));
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        //private static string GenerateRequestHeader(string name_space, Type type,string apiVersion)
        //{
        //    return ($"namespace {name_space}.Application.Contracts.RequestDTO.V{apiVersion}\n{{" +

        //         $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}GetRequestByGuidDTO({type.Name}GetRequestByGuidDTO Value);" +
        //         $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}GetRequestByIdDTO({type.Name}GetRequestByIdDTO Value);" +
        //         $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}GetRequestDTO({type.Name}GetRequestDTO Value);" +

        //        $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}CreateRequestDTO({type.Name}CreateRequestDTO Value );" +
        //        $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}UpdateRequestDTO({type.Name}UpdateRequestDTO Value);" +

        //        $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}DeleteRequestDTO({type.Name}DeleteRequestDTO Value);" +
        //        $"");

        //}
        private static string GenerateRequestHeader(string name_space, Type type, string apiVersion)
        { 
            return ($"namespace {name_space}.Application.Contracts.RequestDTO.V{apiVersion}\n{{" +

                 $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}GetRequestByGuidDTO({type.Name}GetRequestByGuidDTO Value);" +
                 $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}GetRequestByIdDTO({type.Name}GetRequestByIdDTO Value);" +
                 $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}GetRequestDTO({type.Name}GetRequestDTO Value);" +

                $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}CreateRequestDTO(  {GeneralClass.ProduceEntitySignatureFunction(type)} );" +
                $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}UpdateRequestDTO({GeneralClass.ProduceEntitySignatureFunction(type)});" +

                $"{GeneralClass.newlinepad(4)}public  record Application{type.Name}DeleteRequestDTO({type.Name}DeleteRequestDTO Value);" +
                $"");

        }
    }
}