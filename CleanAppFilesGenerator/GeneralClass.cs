using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var type = propertytypename.Contains("ICollection`1") ? " List " : "List";
            var sb = $"{GeneralClass.newlinepad(8)}private {type}<{propertytype}> _{name} {{ get;  set;}}  = new List<{propertytype}>();" +
                $"{GeneralClass.newlinepad(8)}public  IReadOnlyCollection<{propertytype}> {name} => _{name};";
            return sb;
        }



        public static string PrepareProperty(string propType, PropertyInfo prop)
        {
            //string sb = "public  " + prop.PropertyType.Name + prop.Name + "{ get; init; } " + getDatatypeInitialiser(prop);

            var attr = "";
            MaxLengthAttribute hasmaxLengthAttr = null;

            if (getProperDefaultDataType(propType).Equals("string"))
            {
                hasmaxLengthAttr = prop.TryGetMaxAttributeFromPropertyInfo<MaxLengthAttribute>();
                if (hasmaxLengthAttr != null)
                {
                    attr = $"{GeneralClass.newlinepad(12)}[MaxLength({hasmaxLengthAttr.Length})]";
                }
            }
            return $"{attr}{GeneralClass.newlinepad(12)}public {getProperDefaultDataType(propType)} {prop.Name}    {getProperDefaultInit(propType)}";
        }

        public static string PrepareParameter(string propType, string propName)
        {
            return $"{getProperDefaultDataType(propType)}  {FirstCharSubstringToLower(propName)}";
            // return $"{GeneralClass.newlinepad(12)}public {getProperDefaultDataType(propType)} {propName}    {getProperDefaultInit(propType)}";
        }
        public static string PrepareAssignment(string propType, string propName)
        {
            return $"{propName} = {FirstCharSubstringToLower(propName)}";
            // return $"{GeneralClass.newlinepad(12)}public {getProperDefaultDataType(propType)} {propName}    {getProperDefaultInit(propType)}";
        }

        public static string FirstCharSubstringToLower(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return $"{input[0].ToString().ToLower()}{input.Substring(1)}";
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