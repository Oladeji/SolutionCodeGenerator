
namespace CleanAppFilesGenerator
{
    internal class GenerateUnitOfWork
    {
        public static string Generate(Type type, string name_space, int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return ($"using {name_space}.DomainBase.Result;\n" +

                $"using {name_space}.Domain.Errors;\n" +
                $"using {name_space}.Domain.Interfaces;\n\n" +
                $"namespace {name_space}.Infrastructure.Persistence.Repositories\n{{" +
                $"{GeneralClass.newlinepad(4)}public class UnitOfWork : IUnitOfWork" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}public readonly {name_space}Context _ctx;" +
                $"{GeneralClass.newlinepad(8)}public UnitOfWork({name_space}Context ctx) {{ _ctx = ctx;  }}\n" +
                $"{GeneralClass.newlinepad(8)}public async Task<Result<GeneralFailure, int>> CommitAllChanges(CancellationToken cancellationToken)=>throw new NotImplementedException(\"Its not been used to commit for now individual repo implemented savechanges\"); " +
                $"{GeneralClass.newlinepad(8)}public void Dispose(){{_ctx?.Dispose();  GC.SuppressFinalize(this); }}" +
                $"\n" +
                $"{GeneralClass.newlinepad(8)}public {type.Name}Repository _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository ;" +
                $"{GeneralClass.newlinepad(8)}public I{type.Name}Repository {type.Name}Repository => _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository  ??= new {type.Name}Repository(_ctx);");

            }
            else
            {
                return ($"\n" +
                    $"{GeneralClass.newlinepad(8)}public {type.Name}Repository _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository ;" +
                    $"{GeneralClass.newlinepad(8)}public I{type.Name}Repository {type.Name}Repository => _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository  ??= new {type.Name}Repository(_ctx);");
            }


        }
    }
}