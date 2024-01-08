namespace CodeGeneratorAttributesLibrary
{
    public class ProjectBaseModelsAttribute : System.Attribute
    {
        //public ProjectBaseModelsAttribute(int maxSize, bool isKey, bool isUnique, bool isRequired , bool isForeignKey)
        //{
        //    MaxSize = maxSize;
        //    IsKey = isKey;
        //    IsUnique = isUnique;
        //    IsForeignKey = isForeignKey;
        //    IsRequired = isRequired;
        //}
        //public ProjectBaseModelsAttribute(int maxSize, int minSize, bool isKey, bool isRequired)
        //{
        //    MaxSize = maxSize;
        //    MinSize = minSize;
        //    IsKey = isKey;
        //    IsRequired = isRequired;
        //}
        public ProjectBaseModelsAttribute(int maxSize, int minSize, bool isKey, bool isUnique, bool isRequired, bool isForeignKey, bool hasDefaultStringValue, string defaultStringValue)
        {
            MaxSize = maxSize;
            MinSize = minSize;
            IsKey = isKey;
            IsUnique = isUnique;
            IsRequired = isRequired;
            IsForeignKey = isForeignKey;
            DefaultStringValue = defaultStringValue;
            HasDefaultStringValue = hasDefaultStringValue;
        }

        public int MaxSize { get; set; }
        public int MinSize { get; set; }
        //private bool IsPrimaryKey { get; set; }
        public bool IsKey { get; set; }
        //private bool IsNullable { get; set; }
        public bool IsUnique { get; set; }

        public bool IsForeignKey { get; set; }
        //private bool IsReadOnly { get; set; }
        public string DefaultStringValue { get; set; }
        public bool HasDefaultStringValue { get; set; }

        //private bool IsAutoIncrement { get; set; }
        //private bool IsIndexed { get; set; }

        public bool IsRequired { get; set; }
        //private bool IsIgnored { get; set; }
        //private bool IsDefault { get; set; }
        //private bool IsDefaultSet { get; set; }
        //private bool IsDefaultNull { get; set; }
        //private bool IsDefaultEmpty { get; set; }
        //private bool IsDefaultZero { get; set; }
        //private bool IsDefaultFalse { get; set; }
        //private bool IsDefaultTrue { get; set; }
        //private bool IsDefaultDateTimeNow { get; set; }
        //private bool IsDefaultDateTimeUtcNow { get; set; }
        //private bool IsDefaultDateTimeToday { get; set; }
        //private bool IsDefaultDateTimeYesterday { get; set; }
        //private bool IsDefaultDateTimeTomorrow { get; set; }
        //private bool IsDefaultDateTimeMinValue { get; set; }
        //private bool IsDefaultDateTimeMaxValue { get; set; }
        //private bool IsDefaultGuidEmpty { get; set; }
        //private bool IsDefaultGuidNewGuid { get; set; }
        //private bool IsDefaultGuidSequentialGuid { get; set; }
        //private bool IsDefaultGuidComb { get; set; }
        //private bool IsDefaultGuidNewSequentialGuid { get; set; }


    }
}
