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
        public static string ProduceEntitySignatureFunction(Type type)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                var x = Nullable.GetUnderlyingType(prop.PropertyType);
                var propertytype = x == null ? prop.PropertyType.Name : x.Name;

                if (propertytype.Contains("ICollection`1") || (propertytype.Contains("IList`1")))
                {
                    var _ = prop.PropertyType.GenericTypeArguments[0];
                }
                else

                if (!prop.PropertyType.BaseType.Name.Contains("BaseEntity"))
                {
                    sb.Append(GeneralClass.PrepareParameter(prop));
                    sb.Append(", ");
                }
                else
                {          // These are member that are inherited from the base entity
                }

            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public static bool IsNullable(PropertyInfo property)
        {
            NullabilityInfoContext nullabilityInfoContext = new NullabilityInfoContext();
            var info = nullabilityInfoContext.Create(property);
            if (info.WriteState == NullabilityState.Nullable || info.ReadState == NullabilityState.Nullable)
            {
                return true;
            }

            return false;
        }
        public static string PrepareProperty_ToBeDeleteBc_MaxLenghtAttribute_IsNoLongerUsed(PropertyInfo prop)
        {
            //string sb = "public  " + prop.PropertyType.Name + prop.Name + "{ get; init; } " + getDatatypeInitialiser(prop);

            var attr = "";
            MaxLengthAttribute hasmaxLengthAttr;
            string propTypeName = getProperDefaultDataType(prop);
            if (propTypeName.Equals("string"))
            {
                hasmaxLengthAttr = prop.TryGetMaxAttributeFromPropertyInfo<MaxLengthAttribute>();
                if (hasmaxLengthAttr != null)
                {
                    attr = $"{GeneralClass.newlinepad(8)}[MaxLength({hasmaxLengthAttr.Length})]";
                }
            }
            var isnullAblResultSymbol = GeneralClass.IsNullable(prop) ? "?" : "";

            return $"{attr}{GeneralClass.newlinepad(8)}public {propTypeName}{isnullAblResultSymbol} {prop.Name}    {getProperDefaultInit(propTypeName)}";
        }
        public static string PrepareProperty(PropertyInfo prop, bool classInheritsFromBaseEntityClass)
        {


            string propTypeName = getProperDefaultDataType(prop);

            var isnullAblResultSymbol = GeneralClass.IsNullable(prop) ? "?" : "";
            string result = $"public {propTypeName}{isnullAblResultSymbol} {prop.Name}    {getProperDefaultInit(propTypeName)}";
            if (result.Contains("public Guid GuidId    { get; init; }"))
            {
                // This has been added to the base entity so that it can be used to identify the entity
                if (classInheritsFromBaseEntityClass)
                    result = "// " + result;
            }
            return $"{GeneralClass.newlinepad(8)}{result}";
        }
        public static string PrepareParameter(PropertyInfo prop)
        {

            return $"{getProperDefaultDataType(prop)}  {FirstCharSubstringToLower(prop.Name)}";

        }
        public static string PrepareAssignment(string propName)
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

        public static string getProperDefaultDataTypeOld(string type)
        {

            var result = type.ToUpper() switch
            {
                "VARCHAR" => "string",
                "ANSISTRING" => "string",
                "ANSISTRINGFIXLENGTH" => "string",
                "STRING" => "string",
                //"String" => "string",
                "BINARY2" => "VarBinary",
                "BOOLEAN" => "bit",
                "BYTE" => "Byte",
                "DATETIME" => "DateTime",
                "DATE" => "DateTime",
                // "DateTime" => "DateTime",
                "DATETIME2" => "DateTime",
                "DATETIMEOFFSET" => "DateTime",

                "DECIMAL" => "decimal",
                //"decimal" => "decimal",
                "DOUBLE" => "Double",
                "GUID" => "Guid",
                "INT" => "int",
                "INT16" => "int",
                "Int32" => "int",
                "INT64" => "int",
                //"Object" => "Object",

                //"SByte" => "SByte",
                //"Single" => "Single",

                //"VarNumeric" => "VarNumeric",

                //"Xml" => "Xml",
                /*

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
                _ => type*/
                _ => type
            };
            return result;
        }
        public static string getProperDefaultDataType(PropertyInfo prop)
        {
            var isnullAbleResult = Nullable.GetUnderlyingType(prop.PropertyType);
            var propertytypeName = isnullAbleResult == null ? prop.PropertyType.Name : isnullAbleResult.Name;
            var result = propertytypeName.ToUpper() switch
            {
                "VARCHAR" => "string",
                "ANSISTRING" => "string",
                "ANSISTRINGFIXLENGTH" => "string",
                "STRING" => "string",
                //"String" => "string",
                "BINARY2" => "VarBinary",
                "BOOLEAN" => "bool",
                "BYTE" => "Byte",
                "DATETIME" => "DateTime",
                "DATE" => "DateTime",
                // "DateTime" => "DateTime",
                "DATETIME2" => "DateTime",
                "DATETIMEOFFSET" => "DateTime",

                "DECIMAL" => "decimal",
                //"decimal" => "decimal",
                "DOUBLE" => "double",
                "GUID" => "Guid",
                "INT" => "int",
                "INT16" => "int",
                "Int32" => "int",
                "INT64" => "int",
                //"Object" => "Object",

                //"SByte" => "SByte",
                //"Single" => "Single",

                //"VarNumeric" => "VarNumeric",

                //"Xml" => "Xml",
                /*

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
                _ => type*/
                _ => propertytypeName
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