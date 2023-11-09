using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateEntityClass
    {
        public static string GenerateEntity(Type type, string name_space)
        {
            string baseEntity = " : BaseEntity";
            var entityName = type.Name;
            var Output = new StringBuilder();
            Output.Append(ProduceEntityHeader(name_space, entityName, baseEntity));
            Output.Append(ProduceEntityProperties(type));
            Output.Append(GeneralClass.newlinepad(8) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        public static string ProduceEntityHeader(string name_space, string entityName, string baseEntity = " : BaseEntity")
        {


            return ($"namespace {name_space}.Domain.BaseModels.Entities\n{{{GeneralClass.newlinepad(4)}public  class {entityName} {baseEntity}{GeneralClass.newlinepad(8)}{{");
           // return ($"namespace {name_space}.BaseModels.Entities\n{{\n    public  class {entityName} {baseEntity}\n    {{\n");
        }


        public static string ProduceEntityProperties(Type type)
        {

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                var x = Nullable.GetUnderlyingType(prop.PropertyType);
                var propertytype = x == null ? prop.PropertyType.Name : x.Name;

                if (propertytype.Contains("ICollection`1"))
                {
                    var xx = prop.PropertyType.GenericTypeArguments[0];
                    sb.Append(GeneralClass.PreparePropertyAsCollection(xx.Name, prop.Name));

                }
                else

                    sb.Append(GeneralClass.PrepareProperty(propertytype, prop.Name));
            }

            return sb.ToString();
        }

        public static string GenerateBaseEntity( string name_space)
        {
            string baseEntity = "";
            var entityName = "BaseEntity";
            var Output = new StringBuilder();
            Output.Append(ProduceEntityHeader(name_space, entityName, baseEntity));
            Output.Append(GeneralClass.newlinepad(12)+"public Guid GuidId { get; set; } = default;");
            Output.Append(GeneralClass.newlinepad(8) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }


    }


}


