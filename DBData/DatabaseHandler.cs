using System.ComponentModel.Composition;
using Data;
using Data.DataModel;
using DBData.DBMetadata;

namespace DBData
{
    [Export(typeof(ISerializer))]
    public class DatabaseHandler : ISerializer
    {
        private void ClearDB()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ParameterMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM PropertyMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM MethodMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM TypeMetadata ");
                context.Database.ExecuteSqlCommand("DELETE FROM NamespaceMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM AssemblyMetadata WHERE ID != -1");
                context.SaveChanges();
            }
        }

        public BaseAssemblyMetadata Deserialize(string path)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.NamespaceMetadata
                    .Include(n => n.Types)
                    .Load();
                context.TypeMetadata
                    .Include(t => t.Constructors)
                    .Include(t => t.BaseType)
                    .Include(t => t.DeclaringType)
                    .Include(t => t.Fields)
                    .Include(t => t.ImplementedInterfaces)
                    .Include(t => t.GenericArguments)
                    .Include(t => t.Methods)
                    .Include(t => t.NestedTypes)
                    .Include(t => t.Properties)
                    .Include(t => t.TypeGenericArguments)
                    .Include(t => t.TypeImplementedInterfaces)
                    .Include(t => t.TypeNestedTypes)
                    .Include(t => t.MethodGenericArguments)
                    .Include(t => t.TypeBaseTypes)
                    .Include(t => t.TypeDeclaringTypes)
                    .Load();
                context.ParameterMetadata
                    .Include(p => p.Type)
                    .Include(p => p.TypeFields)
                    .Include(p => p.MethodParameters)
                    .Load();
                context.MethodMetadata
                    .Include(m => m.GenericArguments)
                    .Include(m => m.Parameters)
                    .Include(m => m.ReturnType)
                    .Include(m => m.TypeConstructors)
                    .Include(m => m.TypeMethods)
                    .Load();
                context.PropertyMetadata
                    .Include(p => p.Type)
                    .Include(p => p.TypeProperties)
                    .Load();


                DBAssemblyMetadata dbAssemblyMetadata = context.AssemblyMetadata
                    .Include(a => a.NamespaceMetadatas)
                    .ToList().FirstOrDefault();
                if (dbAssemblyMetadata == null)
                    throw new ArgumentException("Database is empty");
                return dbAssemblyMetadata;
            }
        }

        public void Serialize(string path, BaseAssemblyMetadata obj)
        {
            ClearDB();
            using (DatabaseContext context = new DatabaseContext())
            {
                DBAssemblyMetadata assemblyMetadata = (DBAssemblyMetadata)obj;
                context.AssemblyMetadata.Add(assemblyMetadata);
                context.SaveChanges();
            }
        }
       
    }
}