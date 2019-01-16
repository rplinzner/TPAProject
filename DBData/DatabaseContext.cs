using System.Data.Entity;
using DBData.DBMetadata;

namespace DBData
{
    public class DatabaseContext : DbContext
    {
        //private const string _connectionString =
          //  @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TPAProject;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";
        public DatabaseContext() : base("TPAProject")
        {

        }
        public virtual DbSet<DBAssemblyMetadata> AssemblyMetadata { get; set; }
        public virtual DbSet<DBMethodMetadata> MethodMetadata { get; set; }
        public virtual DbSet<DBNamespaceMetadata> NamespaceMetadata { get; set; }
        public virtual DbSet<DBParameterMetadata> ParameterMetadata { get; set; }
        public virtual DbSet<DBPropertyMetadata> PropertyMetadata { get; set; }
        public virtual DbSet<DBTypeMetadata> TypeMetadata { get; set; }
        public virtual DbSet<LogModel> Log { get; set; }
    }
}