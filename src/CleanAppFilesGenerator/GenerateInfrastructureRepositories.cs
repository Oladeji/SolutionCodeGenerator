using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateInfrastructureClass
    {
        public static string GenerateRepositories(Type type, string name_space)
        {

            var entityName = type.Name;
            var Output = new StringBuilder();
            Output.Append(GenerateInfrastructureHeader(name_space, entityName));

            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        public static string GenerateInfrastructureHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Domain.Interfaces;\nusing {name_space}.Domain.Entities;\nnamespace {name_space}.Infrastructure.Persistence.Repositories\n" +
                $"\n{{{GeneralClass.newlinepad(4)}public  class  {entityName}Repository:GenericRepository<{entityName}>, I{entityName}Repository{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}public   {entityName}Repository( {name_space}Context ctx): base(ctx){GeneralClass.newlinepad(8)}{{}}");
        }

    }


}


