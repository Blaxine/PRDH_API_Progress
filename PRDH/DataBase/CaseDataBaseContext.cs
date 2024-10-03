using Microsoft.EntityFrameworkCore;// nugget entityFramework.core
using PRDH.models;

namespace PRDH.DataBase
{
    public class CaseDataBaseContext : DbContext
    {

        public DbSet<CaseModel> Cases {get; set;}
        public CaseDataBaseContext(DbContextOptions<CaseDataBaseContext> options) :base (options){ }
    }
}
