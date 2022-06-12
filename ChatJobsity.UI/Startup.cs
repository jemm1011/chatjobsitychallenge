using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using ChatJobsity.Identity;
using ChatJobsity.UI.Apis;
using System;
using ChatJobsity.UI.Hubs;
using MassTransit;
using ChatJobsity.UI.Handlers;

namespace ChatJobsity.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuthConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(policy =>
            {
                policy.AddPolicy("OpenCors", options =>
                {
                    options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyMethod();
                });
            });

            services.AddMassTransit(conf =>
            {
                conf.AddConsumer<QuoteConsumer>();
                conf.AddBus(context => Bus.Factory.CreateUsingRabbitMq(config =>
                {                    
                    config.Host(new Uri(Configuration.GetSection("RabbitMq:Connection").Value), c =>
                    {
                        c.Username(Configuration.GetSection("RabbitMq:User").Value);
                        c.Password(Configuration.GetSection("RabbitMq:Password").Value);
                    });
                    config.AutoStart = true;
                    config.ConfigureEndpoints(context, KebabCaseEndpointNameFormatter.Instance);
                }));
            });

            services.AddRefitClient<IChatApi>()
                .ConfigureHttpClient(c =>  c.BaseAddress = new Uri(Configuration.GetSection("Apis:ChatApi").Value));

            services.AddRefitClient<IBotApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("Apis:BotApi").Value));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            services.AddMemoryCache();
            services.AddRazorPages();

            services.AddSignalR(c => c.EnableDetailedErrors = true);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("OpenCors");

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
