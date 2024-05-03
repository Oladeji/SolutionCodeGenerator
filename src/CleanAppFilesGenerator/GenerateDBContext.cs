
using System.Configuration;

namespace CleanAppFilesGenerator
{
    internal class GenerateDBContext
    {
        public static string Generate(Type type, string name_space, int selectedIndex, string identityDbContextName)
        {
            var dbContext = "  DbContext";
            if (identityDbContextName.Equals("NONE") || identityDbContextName.Trim() == "")
            {

            }
            else
            {
                dbContext = $"  IdentityDbContext<{identityDbContextName}>";
            }

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
                $"{GeneralClass.newlinepad(4)}public class {name_space}Context : {dbContext}" +
                $"{GeneralClass.newlinepad(4)}{{" +
                $"{GeneralClass.newlinepad(8)}private readonly IConfiguration _configuration;" +
                $"{GenerateOnConfiguring()}" +
                $"{GenerateOnConstructor(name_space)}" +
                $"{GenerateOnModelCreating(name_space)}" +
                $"{GeneralClass.newlinepad(8)}" +
                $"{GenerateSpecific(type)}");

            }
            else
            {

                return $"{GenerateSpecific(type)}";
            }
        }


        public static string GenerateOnConstructor(string name_space)
        {
            return (
            $"{GeneralClass.newlinepad(8)}public {name_space}Context(DbContextOptions<{name_space}Context> options, IConfiguration configuration) : base(options)" +
            $"{GeneralClass.newlinepad(8)}{{" +
            $"{GeneralClass.newlinepad(12)}_configuration = configuration;" +
            $"{GeneralClass.newlinepad(8)}}}");
        }

        public static string GenerateOnConfiguring()
        {
            return (
            $"{GeneralClass.newlinepad(8)}protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)" +
            $"{GeneralClass.newlinepad(8)}{{" +
             $"{GeneralClass.newlinepad(8)}if (optionsBuilder.IsConfigured) return;" +
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