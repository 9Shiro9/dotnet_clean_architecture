using Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Application.UnitTests.Common
{
    public class ApplicationContextFactory
    {
        protected ApplicationContextFactory() { }

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();       

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}