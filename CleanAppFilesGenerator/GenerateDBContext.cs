
namespace CleanAppFilesGenerator
{
    internal class GenerateDBContext
    {
        public static string Generate(Type type, string name_space, int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return (
                $"using {name_space}.Domain.Entities;\n" +
                $"using {name_space}.Domain.Utils;\n" +
                $"using {name_space}.Infrastructure.Utils;\n" +
                $"using Microsoft.EntityFrameworkCore;\n" +
                $"using Microsoft.Extensions.Configuration;\n" +

                $"namespace {name_space}.Infrastructure.Persistence\n" +
                $"{{" +
                $"{GeneralClass.newlinepad(4)}public class {name_space}Context : DbContext" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}private readonly IConfiguration _configuration;" +
                $"{GenerateOnConfiguring()}" +
                $"{GenerateOnModelCreating(name_space)}" +
                $"{GeneralClass.newlinepad(8)}" +
                $"{GenerateSpecific(type)}");

            }
            else
            {

                return $"{GenerateSpecific(type)}";

            }
        }




        public static string GenerateOnConfiguring()
        {
            return (
            $"{GeneralClass.newlinepad(8)}protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}var constr = GetConnectionstringName.GetConnectionStrName(Environment.MachineName);" +
            $"{GeneralClass.newlinepad(12)}var conn = _configuration.GetConnectionString(constr);" +
            $"{GeneralClass.newlinepad(12)}optionsBuilder.UseMySql(conn!, GeneralUtils.GetMySqlVersion());" +
            $"{GeneralClass.newlinepad(8)}}}");
        }
        public static string GenerateOnModelCreating(string name_space)
        {
            return (
            $"{GeneralClass.newlinepad(8)}protected override void OnModelCreating(ModelBuilder modelBuilder)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}base.OnModelCreating(modelBuilder);" +
            $"{GeneralClass.newlinepad(12)}modelBuilder.ApplyConfigurationsFromAssembly(typeof({name_space}Context).Assembly);" +
            $"{GeneralClass.newlinepad(8)}}}");
        }


        public static string GenerateSpecific(Type type)
        {
            return (
            $"{GeneralClass.newlinepad(8)}public DbSet<{type.Name}> {type.Name}s {{ get; private set; }}");
        }
    }
}