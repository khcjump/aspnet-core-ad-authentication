using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo213 {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;

              var builder = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            var ldapconfig = builder.Build();
            LDAPUtil.Register(ldapconfig);
            //LDAPUtil.Register (Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
            services.AddAuthorization (options => {

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            //k:使用Cookies判斷是否有登入鑑權
            app.UseCookieAuthentication (new CookieAuthenticationOptions () {
                AuthenticationScheme = Configuration.GetValue<string> ("CookieName"),
                    LoginPath = new PathString ("/Account/Login/"),
                    AccessDeniedPath = new PathString ("/Account/Login/"),
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true
            });

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}