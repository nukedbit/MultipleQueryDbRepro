using Funq;
using ServiceStack;
using MultipleQueryDbRepro.ServiceInterface;

[assembly: HostingStartup(typeof(MultipleQueryDbRepro.AppHost))]

namespace MultipleQueryDbRepro;

public class AppHost : AppHostBase, IHostingStartup
{
    public AppHost() : base("MultipleQueryDbRepro", typeof(MyServices).Assembly) { }

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });

        Plugins.Add(new CorsFeature(allowedHeaders: "Content-Type,Authorization",
            allowOriginWhitelist: new[]{
            "http://localhost:5000",
            "https://localhost:5001",
            "https://" + Environment.GetEnvironmentVariable("DEPLOY_CDN")
        }, allowCredentials: true));
    }

    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => 
            services.ConfigureNonBreakingSameSiteCookies(context.HostingEnvironment));
}
