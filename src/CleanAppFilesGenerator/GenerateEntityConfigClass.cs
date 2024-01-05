
using CodeGeneratorAttributesLibrary;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    internal class GenerateEntityConfigClass
    {
        internal static string GenerateEntityConfig(Type type, string thenamespace)
        {

            var Output = new StringBuilder();
            Output.Append(GenerateHeader(thenamespace, type));
            Output.Append(GeneralClass.newlinepad(4) + "{");
            //  Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            // Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }




        public static string GenerateHeader(string name_space, Type type)
        {
            return ($"using Microsoft.EntityFrameworkCore;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing {name_space}.Domain.Entities;\nnamespace {name_space}.Infrastructure.Persistence.EntitiesConfig\n");



        }
        //public class DocumentBasePathConfig : IEntityTypeConfiguration<DocumentBasePath>
        //{
        //    public void Configure(EntityTypeBuilder<DocumentBasePath> entity)
        //    {
        //        entity.HasKey(e => new { e.DocumentBasePathId });
        //        entity.Property(e => e.DocumentBasePathId).HasMaxLength(10);


        //    }
        //}

        public static string GenerateEntityConfiguration(string name_space, Type type)
        {


            string keys = "";
            //Output.Insert(0, GeneralClass.newlinepad(8));
            PropertyInfo property = type.GetProperty(name_space);
            if (property != null)
            {
                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is ProjectBaseModelsAttribute)
                    {
                        var attr = attribute as ProjectBaseModelsAttribute;
                        if (attr.IsKey)
                        {
                            keys = keys + $"e.{property.Name},";
                        }
                    }
                }
            }
            if (keys.Length > 0)

                //keys = keys.Substring(0, keys.Length - 1);
                keys = $"entity.HasKey(e => new {{ {keys.Substring(0, keys.Length - 1)} }});";
            return keys;

            //return (
            //    $"{GeneralClass.newlinepad(8)}public void Configure(EntityTypeBuilder<{type.Name}> entity)" +
            //    $"{GeneralClass.newlinepad(8)}{{" +
            //    $"{GeneralClass.newlinepad(8)}{GenerateHasKey(type.Name)}" +
            //    $"{GeneralClass.newlinepad(8)}{GenerateMaxLenghtForString(type.Name)}" +
            //    $"{GeneralClass.newlinepad(8)}{GenerateHasDataSample(type.Name)}" +
            //    $"{GeneralClass.newlinepad(8)}}}" +
            //    $"");


        }
    }
}