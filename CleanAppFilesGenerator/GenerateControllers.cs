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
            Output.Append(ProduceControllerHeader(thenamespace, type, controllerversion));
            // Output.Append(ProduceEntityProperties(type));
            Output.Append(GeneralClass.newlinepad(8) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        private static string ProduceControllerHeader(string name_space, Type type, string controllerversion)
        {
            return ($"using {name_space}..Api.Extentions;\n" +
                $"using {name_space}.Application.Contracts.RequestDTO;\n" +
                $"using {name_space}.Application.Contracts.ResponseDTO;\n" +
                $"using {name_space}.Application.CQRS.{type.Name}.Commands;\n" +
                $"using {name_space}.Application.CQRS.{type.Name}.Queries;\n" +
                $"using {name_space}.Contracts.RequestDTO;\n" +
                $"using {name_space}.Contracts.ResponseDTO;\n" +
                $"using {name_space}.Domain.Errors;\n" +
                $"using LanguageExt;\n" +
                $"using  MediatR;\n" +
                $"using Microsoft.AspNetCore.Mvc;\n" +
                $"using System.Linq;\n" +
                $"using System.Threading;\n" +
                $"namespace {name_space}.Api.Controllers.{controllerversion}\n" +
                $"{{{GeneralClass.newlinepad(4)}public  class {type.Name}sController  : DVBaseController<{type.Name}sController>{GeneralClass.newlinepad(8)}{{");

        }
    }
}
