using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using MultipleQueryDbRepro.Migrations;

[assembly: HostingStartup(typeof(MultipleQueryDbRepro.ConfigureDbMigrations))]

namespace MultipleQueryDbRepro;

public class ConfigureDbMigrations : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureAppHost(afterAppHostInit:appHost => {
            var migrator = new Migrator(appHost.Resolve<IDbConnectionFactory>(), typeof(Migration1000).Assembly);
            AppTasks.Register("migrate", _ => migrator.Run());
            AppTasks.Register("migrate.revert", args => migrator.Revert(args[0]));
            AppTasks.Run();
        });
}
