
namespace CleanAppFilesGenerator
{
    internal class GenerateUnitOfWork
    {
        public static string Generate(Type type, string name_space, int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return ($"using LanguageExt;\n" +

                $"using {name_space}.Domain.Errors;\n" +
                $"using Microsoft.EntityFrameworkCore;\n" +
                $"using System.Data;\n" +
                $"using Microsoft.EntityFrameworkCore.Storage;\n" +
                $"using Domain.Interfaces;\n\n" +

                $"namespace {name_space}.Infrastructure.Persistence.Repositories\n{{" +
                $"{GeneralClass.newlinepad(4)}public class UnitOfWork : IUnitOfWork" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}public readonly {name_space}Context _ctx;" +
                $"{GeneralClass.newlinepad(8)}public UnitOfWork({name_space}Context ctx) {{ _ctx = ctx;  }}\n" +
                $"{GeneralClass.newlinepad(8)}public IDbTransaction BeginTransaction(IsolationLevel? isolationLevel = IsolationLevel.ReadCommitted)" +
               $"{GeneralClass.newlinepad(8)}{{" +
               $"{GeneralClass.newlinepad(12)}var transaction = _ctx.Database.BeginTransaction();" +
                $"{GeneralClass.newlinepad(12)}return transaction.GetDbTransaction();" +
                $"{GeneralClass.newlinepad(8)}}}" +
                $"{GeneralClass.newlinepad(8)}public async Task<Either<GeneralFailure, int>> CommitAllChanges(CancellationToken cancellationToken)" +
                $"{GeneralClass.newlinepad(8)}{{" +
                $"{GeneralClass.newlinepad(8)}// I am not even using this for now each repo has savechanges" +
                $"{GeneralClass.newlinepad(8)}//but might be useful if i have something to centrally do before saving else i should remove it" +
                $"{GeneralClass.newlinepad(12)}try" +
                $"{GeneralClass.newlinepad(12)}{{" +
                $"{GeneralClass.newlinepad(16)}return await _ctx.SaveChangesAsync(cancellationToken);" +
                $"{GeneralClass.newlinepad(12)}}}" +
                $"{GeneralClass.newlinepad(12)}catch (DbUpdateException ex)" +
                $"{GeneralClass.newlinepad(12)}{{" +
                $"{GeneralClass.newlinepad(16)}return GeneralFailures.ProblemAddingEntityIntoDbContext(\"Problem Saving Data\");" +
                $"{GeneralClass.newlinepad(12)}}}" +
                $"{GeneralClass.newlinepad(12)}catch (Exception ex)" +
                $"{GeneralClass.newlinepad(12)}{{" +
                $"{GeneralClass.newlinepad(16)}return GeneralFailures.ExceptionThrown(\"GenericRepository-AddAsync\", \"Problem Saving Data\", ex?.InnerException?.Message);" +
                $"{GeneralClass.newlinepad(12)}}}" +
                $"{GeneralClass.newlinepad(8)}}}" +


                $"{GeneralClass.newlinepad(8)}public void Dispose(){{_ctx?.Dispose();  GC.SuppressFinalize(this); }}");
                // $"\n" +
                //$"{GeneralClass.newlinepad(8)}public {type.Name}Repository _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository ;" +
                //$"{GeneralClass.newlinepad(8)}public I{type.Name}Repository {type.Name}Repository => _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository  ??= new {type.Name}Repository(_ctx);");

            }
            else
            {
                return "";
                //return ($"\n" +
                //    $"{GeneralClass.newlinepad(8)}public {type.Name}Repository _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository ;" +
                //    $"{GeneralClass.newlinepad(8)}public I{type.Name}Repository {type.Name}Repository => _{GeneralClass.FirstCharSubstringToLower(type.Name)}Repository  ??= new {type.Name}Repository(_ctx);");
            }


        }
    }
}