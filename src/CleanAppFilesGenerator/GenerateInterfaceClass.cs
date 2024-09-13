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
                    $"using {name_space}.DomainBase;\n" +
                          $"using {name_space}.Domain.Errors;\n" +
                          $"namespace {name_space}.Domain.Interfaces\n{{\n" +
                          $"{GeneralClass.newlinepad(4)}public interface IGenericRepository<T> where T : BaseEntity\n" +
                          $"{GeneralClass.newlinepad(4)}{{" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> AddRangeAsync(List<T> entity, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> UpdateAsync(T entity, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> DeleteAsync(T entity, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, List<T>>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression = null, List<string> includes = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, CancellationToken cancellationToken = default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, T>> GetMatch(System.Linq.Expressions.Expression<Func<T, bool>> expression, List<string> includes = null, CancellationToken cancellationToken = default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, T>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> DeleteByQueryAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> ExecuteQueryAsync(string query, Dictionary<string, object>? parameters, CancellationToken cancellationToken);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> GetMaxValueAsync(System.Linq.Expressions.Expression<Func<T, int>> selector, CancellationToken cancellationToken = default);" +
                          $"{GeneralClass.newlinepad(8)}Task<Either<GeneralFailure, int>> AddAsync(T entity, CancellationToken cancellationToken);" +



                          $"{GeneralClass.newlinepad(4)}}}" +
                          $"\n}}");


        }
    }
}

