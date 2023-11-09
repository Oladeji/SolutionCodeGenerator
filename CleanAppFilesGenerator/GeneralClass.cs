using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanAppFilesGenerator
{
    public class GeneralClass
    {

        public static string newlinepad(int space)
        {
           return "\n"+"".PadRight(space);
        }

        public static string PreparePropertyAsCollection(string propertytype, string name)
        {
            //public ICollection<Model> Models { get; set; }
            //string sb = $"public {getProperDefaultDataType(propType)} {propName}    {getProperDefaultInit(propType)} \n";
           
            var  sb = $"{GeneralClass.newlinepad(12)}public ICollection<{propertytype}> {name} {{ get; set;}}";
            return sb;
        }



        public static string PrepareProperty(string propType,string propName)
        {
            //string sb = "public  " + prop.PropertyType.Name + prop.Name + "{ get; init; } " + getDatatypeInitialiser(prop);
            return  $"{GeneralClass.newlinepad(12)}public {getProperDefaultDataType(propType)} {propName}    {getProperDefaultInit(propType)}";          
        }


        private static string getProperDefaultInit(string type)
        {
           
            var result = type switch
            {
                "VarChar" => "{ get; init; }  = string.Empty;",
                "AnsiString" => "{ get; init; }  = string.Empty;",
                "AnsiStringFixedLength" => "{ get; init; } = string.Empty; ",
                "string" => "{ get; init; }  = string.Empty; ",
                "String" => "{ get; init; }  = string.Empty; ",


                //"binary2" => "VarBinary",
                //"boolean" => "bit",
                //"byte" => "Byte",
                //"Datetime" => "Datetime",
                //"Date" => "Datetime",
                //"DateTime" => "Datetime",
                //"DateTime2" => "Datetime",
                //"DateTimeOffset" => "Datetime",

            //"Decimal" => "decimal",
            //"Double" => "Double",
            //"Guid" => "Guid",
            //"Int" => "int",
            //"Int16" => "int",
            //"Int32" => "int",
            //"Int64" => "int",
            //"Object" => "Object",

            //"SByte" => "SByte",
            //"Single" => "Single",

            //"VarNumeric" => "VarNumeric",

            //"Xml" => "Xml",
                _ => "{ get; init; } ",
            };
            return result;
        }

        private static string getProperDefaultDataType(string type)
        {
            
            var result = type switch
            {
                "VarChar" => "string",
                "AnsiString" => "string",
                "AnsiStringFixedLength" => "string",
                "string" => "string",
                "String" => "string",
                "binary2" => "VarBinary",
                "boolean" => "bit",
                "byte" => "Byte",
                "Datetime" => "DateTime",
                "Date" => "DateTime",
                "DateTime" => "DateTime",
                "DateTime2" => "DateTime",
                "DateTimeOffset" => "DateTime",

                "Decimal" => "decimal",
                "decimal" => "decimal",
                "Double" => "Double",
                "Guid" => "Guid",
                "Int" => "int",
                "Int16" => "int",
                "Int32" => "int",
                "Int64" => "int",
                //"Object" => "Object",

                //"SByte" => "SByte",
                //"Single" => "Single",

                //"VarNumeric" => "VarNumeric",

                //"Xml" => "Xml",
                _ => type
            };
            return result;
            }

        //private static string getDatatypeInitialiser(PropertyInfo prop)
        //{
        //    return " = string.Empty";
        //}


        public static string ProduceClosingBrace()
        {
            return "}";
        }
    }
}