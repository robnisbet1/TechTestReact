namespace Parmenion
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ThirdPartySDK;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ParmenionDbContext>(opt => opt.UseInMemoryDatabase(nameof(ParmenionDbContext)));

            services.AddControllers();
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

            services.AddTransient<IFundData, FundDataService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                });
            });
        }
    }
}
