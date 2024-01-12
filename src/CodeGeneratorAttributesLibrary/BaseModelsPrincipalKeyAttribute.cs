namespace CodeGeneratorAttributesLibrary
{
    public class BaseModelsPrincipalKeyAttribute : System.Attribute
    {
        public BaseModelsPrincipalKeyAttribute(string hasOne, string withMany, string hasPrincipalKey, string principalKey)
        {
            HasOne = hasOne;
            WithMany = withMany;
            HasPrincipalKey = hasPrincipalKey;
            PrincipalKey = principalKey;
        }

        public string HasOne { get; set; }
        public string WithMany { get; set; }
        public string HasPrincipalKey { get; set; }
        public string PrincipalKey { get; set; }
        //public string PrincipalEntity { get; set; }
        //public string DependentEntity { get; set; }
        //public string DependentKey { get; set; }
        //public string HasConstraintName { get; set; }
        //public string OnDelete { get; set; }
        //public string OnUpdate { get; set; }
        //public string OnDeleteSql { get; set; }

    }
}
