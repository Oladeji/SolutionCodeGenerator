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
        //public bool HasForeignKey { get; set; }
        //public string ForeignKey { get; set; }

        //public string PrincipalEntity { get; set; }
        //public string DependentEntity { get; set; }
        //public string DependentKey { get; set; }
        //public string HasConstraintName { get; set; }
        //public string OnDelete { get; set; }
        //public string OnUpdate { get; set; }
        //public string OnDeleteSql { get; set; }

    }
}
