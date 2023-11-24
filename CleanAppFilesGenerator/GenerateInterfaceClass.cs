using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    public class GenerateInterfaceClass
    {


        public static string GenerateInterface(Type type, string name_space)
        {

            var entityName = type.Name;
            var Output = new StringBuilder();
            Output.Append(ProduceInterfaceHeader(name_space, entityName));

            Output.Append(GeneralClass.newlinepad(4) + GeneralClass.ProduceClosingBrace());
            Output.Append(GeneralClass.newlinepad(0) + GeneralClass.ProduceClosingBrace());
            return Output.ToString();
        }

        public static string ProduceInterfaceHeader(string name_space, string entityName)
        {
            return ($"using {name_space}.Domain.Entities;\nnamespace {name_space}.Domain.Interfaces\n{{{GeneralClass.newlinepad(4)}public  interface I{entityName}Repository:IGenericRepository< {entityName}>{GeneralClass.newlinepad(4)}{{");
        }


        //public static string ProduceEntityProperties(Type type)
        //{

        //    StringBuilder sb = new StringBuilder();
        //    PropertyInfo[] properties = type.GetProperties();
        //    foreach (PropertyInfo prop in properties)
        //    {
        //        var x = Nullable.GetUnderlyingType(prop.PropertyType);
        //        var propertytype = x == null ? prop.PropertyType.Name : x.Name;

        //        if (propertytype.Contains("ICollection`1"))
        //        {
        //            var xx = prop.PropertyType.GenericTypeArguments[0];
        //            sb.Append(GeneralClass.PreparePropertyAsCollection(xx.Name, prop.Name));

        //        }
        //        else

        //            sb.Append(GeneralClass.PrepareProperty(propertytype, prop.Name));
        //    }

        //    return sb.ToString();
        //}

        public static string GenerateIGenericRepository(string name_space)
        {
            return ($"using LanguageExt;\n" +
                    $"using {name_space}.DomainBase.Base;\n" +
                          $"using {name_space}.Domain.Errors;\n" +
                          $"namespace {name_space}.Domain.Interfaces\n{{\n" +
                          $"{GeneralClass.newlinepad(4)}public interface IGenericRepository<T> where T : BaseEntity\n" +
                          $"{GeneralClass.newlinepad(4)}{{" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailures, int>> AddAsync(T entity, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailures, int>> UpdateAsync(T entity, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailures, int>> DeleteAsync(T entity, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)} Task<Either<GeneralFailures, Task<IReadOnlyList<T>>>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression= null,List<string> includes = null,Func<IQueryable<T>,IOrderedQueryable<T>> orderBy= null,CancellationToken cancellationToken =default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailures, T>> GetMatch(System.Linq.Expressions.Expression<Func<T, bool>> expression,List<string> includes= null , CancellationToken cancellationToken= default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailures, T>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken=default);" +
                          $"{GeneralClass.newlinepad(4)}}}" +
                          $"\n}}");


        }

        //internal static string GenerateInterfaces(Type type, string thenamespace)
        //{
        //   return "NOT YET";
        //}
    }


}




//namespace DocumentVersionManager.BaseModels.Entities
//{
//    public abstract class BaseEntity
//    {

//        public Guid GuidId { get; set; } = default;

//    }
//}