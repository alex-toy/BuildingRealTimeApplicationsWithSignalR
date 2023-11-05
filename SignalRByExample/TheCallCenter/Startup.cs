using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheCallCenter.Data;
using TheCallCenter.Hubs;

namespace TheCallCenter
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<CallCenterContext>()
        .AddEntityFrameworkSqlServer();

      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddControllersWithViews();

      services.AddSignalR(config => config.EnableDetailedErrors = true);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();
      app.UseRouting();
      app.UseCookiePolicy();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<CallCenterHub>("/callcenter");
      });

      //app.UseEndpoints(routes =>
      //{
      //  routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      //});
    }
  }
}
