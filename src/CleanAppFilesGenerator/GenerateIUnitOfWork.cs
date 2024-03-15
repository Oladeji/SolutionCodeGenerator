
using System.Xml.Linq;

namespace CleanAppFilesGenerator
{
    internal class GenerateIUnitOfWork
    {
        public static string Generate(Type type, string name_space, int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                //return ($"using {name_space}.DomainBase.Result;\n" +

                return ($"using {name_space}.Domain.Errors;\n" +
                 $"using {name_space}.DomainBase.Result;\n" +
                $"namespace {name_space}.Domain.Interfaces\n{{" +
                $"{GeneralClass.newlinepad(4)}public interface IUnitOfWork : IDisposable" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}Task<Result<GeneralFailure, int>> CommitAllChanges(CancellationToken cancellationToken);" +
                $"{GeneralClass.newlinepad(8)}I{type.Name}Repository {type.Name}Repository {{ get; }}");

            }
            else
            {

                return ($"{GeneralClass.newlinepad(8)}I{type.Name}Repository {type.Name}Repository {{ get; }}");

            }


        }
    }
}