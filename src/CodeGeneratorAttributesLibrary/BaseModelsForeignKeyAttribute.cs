using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratorAttributesLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BaseModelsForeignKeyAttribute : System.Attribute
    {
        public BaseModelsForeignKeyAttribute(string hasOne, string withMany)
        {
            HasOne = hasOne;
            WithMany = withMany;
        }

        public string HasOne { get; set; }
        public string WithMany { get; set; }


    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BaseModelsHardForeignKeyAttribute : System.Attribute
    {
        public BaseModelsHardForeignKeyAttribute(string hasOne, string withMany, string[] keys)
        {
            HasOne = hasOne;
            WithMany = withMany;
            Keys = keys;
        }

        public string HasOne { get; set; }
        public string WithMany { get; set; }

        public string[] Keys { get; set; } = [];


    }
}
