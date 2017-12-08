using System.Data.Entity;
using EstudoArchitectureDDDVS2015Entity.Database;
using EstudoArchitectureDDDVS2015Mapping.Database;

namespace EstudoArchitectureDDDVS2015DataAccess
{
    public partial class TestContext : DbContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);
        }

        public TestContext():base("Name=TestContext")
        {
        }

        public TestContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }

        public DbSet<testeEntity> testeObj { get; set; }
        public DbSet<UsuarioSguEntity> UsuarioSgu { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new testeMap());
            modelBuilder.Configurations.Add(new UsuarioSguMap());
        }
    }
}
