
using CodeGeneratorAttributesLibrary;
using System.Reflection;
using System.Text;


namespace CleanAppFilesGenerator
{
    internal class GenerateEntityConfigClass
    {
        internal static string GenerateEntityConfig(Type type, string thenamespace, int defaultStringlength)
        {
            int tabspace = 12;
            var Output = new StringBuilder();
            Output.Append(GenerateHeader(thenamespace));
            Output.Append('{');
            Output.Append(GeneralClass.newlinepad(4) + $"public class {type.Name}Config : IEntityTypeConfiguration<{type.Name}>");
            Output.Append(GeneralClass.newlinepad(4) + "{");
            Output.Append(GenerateEntityClass(type, tabspace, defaultStringlength));
            Output.Append(GeneralClass.newlinepad(4) + "}");
            Output.Append("\n}");
            return Output.ToString();
        }
        public static string GenerateHeader(string name_space)
        {
            return ($"using Microsoft.EntityFrameworkCore;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\nusing {name_space}.Domain.Entities;\nnamespace {name_space}.Infrastructure.Persistence.EntitiesConfig\n");
        }
        public static string GenerateEntityClass(Type type, int tabspace = 12, int defaultStringlength = 32, int defaultDecimalMax = 18, int defaultDecimalMin = 6)
        {

            var Output = new StringBuilder();
            Output.Append($"{GeneralClass.newlinepad(8)}public void Configure(EntityTypeBuilder<{type.Name}> entity)" +
                 $"{GeneralClass.newlinepad(8)}{{");
            var haskey = GenerateEntityConfigurationHasKey(type);
            if (haskey != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + haskey);
            }
            var hasidentity = GenerateEntityConfigurationHasIdentityColumn(type);

            if (hasidentity != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + hasidentity);
            }


            var maxlenght = GenerateEntityConfigurationMaxAndMinLength(type, defaultStringlength, defaultDecimalMax, defaultDecimalMin, tabspace);
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
            var hardforeignkey = GenerateEntityConfigurationHardForeignKey(type, GeneralClass.newlinepad(12));
            if (hardforeignkey != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + hardforeignkey);
            }

            var principalkey = GenerateEntityConfigurationPricipalKey(type);
            if (principalkey != "")
            {
                Output.Append(GeneralClass.newlinepad(12) + principalkey);
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

        public static string GenerateEntityConfigurationHasIdentityColumn(Type type)
        {
            string Autokeys = "";
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
                            if (attr.IsAutoIncrement)
                            {
                                Autokeys = Autokeys + $"{property.Name},";
                            }
                        }
                    }
                }
            }
            //  entity.Property(e => e.ModelVersionDocumentId).UseMySqlIdentityColumn().ValueGeneratedOnAdd();
            if (Autokeys.Length > 0)
                // Autokeys = $"entity.HasKey(e => new {{ {Autokeys.Substring(0, Autokeys.Length - 1)} }});";
                Autokeys = $"entity.Property(e => e.{Autokeys.Substring(0, Autokeys.Length - 1)}).UseMySqlIdentityColumn().ValueGeneratedOnAdd(); ";
            return Autokeys;
        }



        public static string GenerateEntityConfigurationHardForeignKey(Type type, string tabspace)
        {
            string keys = "";
            string foreignKey = "";


            var dnAttribute = type.GetCustomAttributes();
            if (dnAttribute != null)
            {

                foreach (var attribute in dnAttribute)
                {
                    keys = "";
                    if (attribute is BaseModelsHardForeignKeyAttribute)
                    {
                        var attr = attribute as BaseModelsHardForeignKeyAttribute;
                        if ((attr.HasOne != "") && (attr.WithMany != "") && (attr.Keys.Length > 0))
                        {
                            PropertyInfo[] properties = type.GetProperties();
                            var propertynames = properties.Select(x => x.Name).ToList();
                            foreach (var key in attr.Keys)
                            {
                                if (propertynames.Contains(key))
                                    keys += $"e.{key},";
                                else
                                {// throw new Exception($"The key {key} is not a property of the class {type.Name}");
                                    MessageBox.Show($"The key {key} is not a property of the class {type.Name}");
                                }
                            }
                            foreignKey = foreignKey + $"entity.HasOne<{attr.HasOne}>(e => e.{attr.HasOne}).WithMany(ad => ad.{attr.WithMany}).HasForeignKey(e => new {{{keys.Substring(0, keys.Length - 1)}}});" + tabspace;
                        }
                    }
                }



                // if (keys != "") keys = foreignKey;

            }

            return foreignKey;
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

        public static string GenerateEntityConfigurationPricipalKey(Type type)
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
                            if (attr.IsPrincipalKey)
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
                        if (attribute is BaseModelsPrincipalKeyAttribute)
                        {
                            var attr = attribute as BaseModelsPrincipalKeyAttribute;
                            if ((attr.HasOne != "") && (attr.WithMany != ""))
                            {
                                foreignKey = $"entity.HasOne<{attr.HasOne}>(e => e.{attr.HasOne}).WithMany(ad => ad.{attr.WithMany}).HasPrincipalKey(e => new {{{keys.Substring(0, keys.Length - 1)}}});";
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
        public static string GenerateEntityConfigurationMaxAndMinLength(Type type, int defaultStringlength, int defaultDecimalMax, int defaultDecimalMin, int tabspace)
        {
            string constraints = "";
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property != null)
                {
                    var defaultDataType = GeneralClass.getProperDefaultDataType(property);
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

                                        if (defaultDataType.Equals("string"))
                                        {
                                            constraints = GenerateStringContraint(tabspace, constraints, property.Name, attr.MaxSize);
                                        }


                                        else if (
                                             defaultDataType.Equals("decimal") || defaultDataType.Equals("double"))

                                        {
                                            constraints = GenerateDecimalContraint(tabspace, constraints, property.Name, attr.MaxSize, attr.MinSize);
                                        }
                                    }

                                }
                            }

                        }
                        else
                        {
                            if (defaultDataType.Equals("string"))
                            {

                                int multiplier = 1;
                                if (property.Name.Contains("Description")) multiplier = 5;

                                constraints = GenerateStringContraint(tabspace, constraints, property.Name, defaultStringlength * multiplier);     //constraints + GeneralClass.newlinepad(tabspace) + $"entity.Property(e => e.{property.Name}).HasMaxLength({defaultStringlength * 5}); ";

                            }

                            else if (defaultDataType.Equals("decimal") || defaultDataType.Equals("double"))

                                constraints = GenerateDecimalContraint(tabspace, constraints, property.Name, defaultDecimalMax, defaultDecimalMin);


                        }
                    }

                }
            }
            return constraints;

            static string GenerateStringContraint(int tabspace, string keys, string propertyName, int? attrMaxSize)
            {
                if (keys.Length > 0)

                    keys = keys + GeneralClass.newlinepad(tabspace) + $"entity.Property(e => e.{propertyName}).HasMaxLength({attrMaxSize}); ";
                else
                    keys = $"entity.Property(e => e.{propertyName}).HasMaxLength({attrMaxSize}); ";
                return keys;
            }
        }

        private static string GenerateDecimalContraint(int tabspace, string keys, string propertyName, int attrMaxSize, int attrMinSize)
        {
            if (keys.Length > 0)

                keys = keys + GeneralClass.newlinepad(tabspace) + $"entity.Property(e => e.{propertyName}).HasPrecision({attrMaxSize},{attrMinSize}); ";
            else
                keys = $"entity.Property(e => e.{propertyName}).HasPrecision({attrMaxSize},{attrMinSize}); ";
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