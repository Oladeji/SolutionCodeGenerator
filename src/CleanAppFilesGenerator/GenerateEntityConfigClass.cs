
using CodeGeneratorAttributesLibrary;
using System;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    internal class GenerateEntityConfigClass
    {
        internal static string GenerateEntityConfig(Type type, string thenamespace, int defaultStringlength)
        {
            var Output = new StringBuilder();
            Output.Append(GenerateHeader(thenamespace));
            Output.Append('{');
            Output.Append(GeneralClass.newlinepad(4) + $"public class {type.Name}Config : IEntityTypeConfiguration<{type.Name}>");
            Output.Append(GeneralClass.newlinepad(4) + "{");
            Output.Append(GenerateEntityClass(type, defaultStringlength));
            Output.Append(GeneralClass.newlinepad(4) + "}");
            Output.Append("\n}");
            return Output.ToString();
        }
        public static string GenerateHeader(string name_space)
        {
            return ($"using Microsoft.EntityFrameworkCore;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing {name_space}.Domain.Entities;\nnamespace {name_space}.Infrastructure.Persistence.EntitiesConfig\n");
        }
        public static string GenerateEntityClass(Type type, int defaultStringlength)
        {

            var Output = new StringBuilder();
            Output.Append($"{GeneralClass.newlinepad(8)}public void Configure(EntityTypeBuilder<{type.Name}> entity)" +
                 $"{GeneralClass.newlinepad(8)}{{");
            var haskey = GenerateEntityConfigurationHasKey(type);
            if (haskey != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + haskey);
            }
            var maxlenght = GenerateEntityConfigurationMaxLenghtForString(type, defaultStringlength, 12);
            if (maxlenght != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + maxlenght);
            }
            var isrequired = GenerateEntityConfigurationIsRequired(type);
            if (isrequired != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + isrequired);
            }
            var isunique = GenerateEntityConfigurationIsUnique(type);
            if (isunique != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + isunique);
            }

            var foreignkey = GenerateEntityConfigurationForeignKey(type);
            if (foreignkey != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + foreignkey);
            }
            //var hasdata = GenerateEntityConfigurationHasDataSample(type);
            //if (hasdata != "")
            //{
            //    Output.Append(GeneralClass.newlinepad(12) + hasdata);
            //}

            return Output.Append(GeneralClass.newlinepad(8) + "}").ToString();
        }
        public static string GenerateEntityConfigurationHasKey(Type type)
        {
            string keys = "";
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property != null)
                {
                    var attributes = property.GetCustomAttributes();
                    foreach (var attribute in attributes)
                    {
                        if (attribute is BaseModelBasicAttribute)
                        {
                            var attr = attribute as BaseModelBasicAttribute;
                            if (attr.IsKey)
                            {
                                keys = keys + $"e.{property.Name},";
                            }
                        }
                    }
                }
            }
            if (keys.Length > 0)
                keys = $"entity.HasKey(e => new {{ {keys.Substring(0, keys.Length - 1)} }});";
            return keys;
        }

        public static string GenerateEntityConfigurationForeignKey(Type type)
        {
            string keys = "";
            string foreignKey = "";
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property != null)
                {
                    var attributes = property.GetCustomAttributes();
                    foreach (var attribute in attributes)
                    {
                        if (attribute is BaseModelBasicAttribute)
                        {
                            var attr = attribute as BaseModelBasicAttribute;
                            if (attr.IsForeignKey)
                            {
                                keys = keys + $"e.{property.Name},";
                            }
                        }
                    }
                }
            }
            if (keys.Length > 0)
            {
                var dnAttribute = type.GetCustomAttributes();
                if (dnAttribute != null)
                {

                    foreach (var attribute in dnAttribute)
                    {
                        if (attribute is BaseModelsForeignKeyAttribute)
                        {
                            var attr = attribute as BaseModelsForeignKeyAttribute;
                            if ((attr.HasOne != "") && (attr.WithMany != ""))
                            {
                                foreignKey = $"entity.HasOne<{attr.HasOne}>(e => e.{attr.HasOne}).WithMany(ad => ad.{attr.WithMany}).HasForeignKey(e => new {{{keys.Substring(0, keys.Length - 1)}}});";
                            }
                        }
                    }


                }
                if (foreignKey == "") keys = ""; else keys = foreignKey;

            }

            return keys;
        }
        public static string GenerateEntityConfigurationIsUnique(Type type)
        {
            string keys = "";
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property != null)
                {
                    var attributes = property.GetCustomAttributes();
                    foreach (var attribute in attributes)
                    {
                        if (attribute is BaseModelBasicAttribute)
                        {
                            var attr = attribute as BaseModelBasicAttribute;
                            if (attr.IsUnique)
                            {
                                keys = keys + $"e.{property.Name},";
                            }
                        }
                    }
                }
            }
            if (keys.Length > 0)
                keys = $"entity.HasIndex(e => new {{ {keys.Substring(0, keys.Length - 1)} }}).IsUnique();";
            return keys;
        }
        public static string GenerateEntityConfigurationMaxLenghtForString(Type type, int defaultStringlength, int tabspace)
        {
            string keys = "";
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Contains("VersionDescription"))
                {
                    int x = 0;

                }

                if (property != null)
                {
                    var attributes = property.GetCustomAttributes();
                    if (attributes != null)
                    {
                        if (attributes.ToList().Count() > 0)
                        {
                            foreach (var attribute in attributes)
                            {
                                if (attribute is BaseModelBasicAttribute)
                                {
                                    var attr = attribute as BaseModelBasicAttribute;
                                    if (attr.MaxSize > 0)
                                    {
                                        if (keys.Length > 0)

                                            keys = keys + GeneralClass.newlinepad(tabspace) + $"entity.Property(e => e.{property.Name}).HasMaxLength({attr.MaxSize}); ";
                                        else
                                            keys = $"entity.Property(e => e.{property.Name}).HasMaxLength({attr.MaxSize}); ";
                                    }

                                }
                            }

                        }
                        else
                        {
                            if (GeneralClass.getProperDefaultDataType(property.PropertyType.Name).Equals("string"))
                            {
                                if (keys.Length > 0)
                                    keys = keys + GeneralClass.newlinepad(tabspace) + $"entity.Property(e => e.{property.Name}).HasMaxLength({defaultStringlength}); ";
                                else
                                    keys = $"entity.Property(e => e.{property.Name}).HasMaxLength({defaultStringlength}); ";
                            }


                        }
                    }

                }
            }
            return keys;
        }
        public static string GenerateEntityConfigurationIsRequired(Type type)
        {
            string keys = "";
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property != null)
                {
                    var attributes = property.GetCustomAttributes();
                    foreach (var attribute in attributes)
                    {
                        if (attribute is BaseModelBasicAttribute)
                        {
                            var attr = attribute as BaseModelBasicAttribute;
                            if (attr.IsRequired)
                            {
                                keys = $"entity.Property(e => e.{property.Name}).IsRequired(); ";
                            }

                        }
                    }
                }
            }
            return keys;
        }
    }
}