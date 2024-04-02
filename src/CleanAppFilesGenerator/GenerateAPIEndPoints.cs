using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public static class GenerateAPIEndPoints
    {
        public static string Generate(Type type, string name_space, int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return (


                $"namespace {name_space}.Api\n{{" +
                $"{GeneralClass.newlinepad(4)}public static class {name_space}APIEndPoints" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}public const string APIBase = \"api/v{{version:apiVersion}}\";" +
                $"{GenerateSpecific(type)}");

            }
            else
            {

                return $"{GenerateSpecific(type)}";

            }
        }



        public static string GenerateSpecific(Type type)
        {

            return (
            $"{GeneralClass.newlinepad(8)}public static class {type.Name}" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}public const string Controller = \"{type.Name}s\";" +
            $"{GeneralClass.newlinepad(12)}public const string Create = $\"{{APIBase}}/{{Controller}}\";" +
            $"{GeneralClass.newlinepad(12)}public const string Delete = $\"{{APIBase}}/{{Controller}}/{{{{request}}}}\";" +
            $"{GeneralClass.newlinepad(12)}public const string GetById = $\"{{APIBase}}/{{Controller}}/{{{{NameOrGuid}}}}\";" +
            $"{GeneralClass.newlinepad(12)}public const string GetByJSONBody = $\"{{APIBase}}/{{Controller}}/JsonBody\";" +
            $"{GeneralClass.newlinepad(12)}public const string Get = $\"{{APIBase}}/{{Controller}}\";" +
            $"{GeneralClass.newlinepad(12)}public const string Update = $\"{{APIBase}}/{{Controller}}\";" +
            $"{GeneralClass.newlinepad(8)}}}");


        }
    }
}