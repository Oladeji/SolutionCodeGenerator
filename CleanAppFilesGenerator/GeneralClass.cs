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
            return "\n" + "".PadRight(space);
        }

        public static string PreparePropertyAsCollectionOrList(string propertytype, string name, string propertytypename)
        {

            //var type = propertytypename.Contains("ICollection`1") ? "ICollection" : "IList";
            // force everything to be a list so that  i can return it as ASReadOnly but put space as the ist for collection
            var type = propertytypename.Contains("ICollection`1") ? " IList " : "IList";
            var sb = $"{GeneralClass.newlinepad(12)}private {type}<{propertytype}> _{name} {{ get;  set;}}  = new List<{propertytype}>();" +
                $"{GeneralClass.newlinepad(12)}public  IEnumerable<{propertytype}> {name} => _{name}.AsReadOnly();";
            return sb;
        }



        public static string PrepareProperty(string propType, string propName)
        {
            //string sb = "public  " + prop.PropertyType.Name + prop.Name + "{ get; init; } " + getDatatypeInitialiser(prop);
            return $"{GeneralClass.newlinepad(12)}public {getProperDefaultDataType(propType)} {propName}    {getProperDefaultInit(propType)}";
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