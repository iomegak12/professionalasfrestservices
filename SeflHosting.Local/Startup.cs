using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntSol.Libraries.Business.Impl;
using IntSol.Libraries.Business.Interfaces;
using IntSol.Libraries.Business.Validations.Impl;
using IntSol.Libraries.Business.Validations.Interfaces;
using IntSol.Libraries.DataAccess.Impl;
using IntSol.Libraries.DataAccess.Interfaces;
using IntSol.Libraries.ORM.Impl;
using IntSol.Libraries.ORM.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace SeflHosting.Local
{
    public class Startup
    {
        private const string CONNECTION_STRING_ENV_VAR = "CRMSystemDBConnectionString";
        private const string INVALID_CONNECTION_STRING = "Invalid Connection String Specified!";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomersContext>(
                dbContextOptionsBuilder =>
                {
                    var connectionString = Environment.GetEnvironmentVariable(CONNECTION_STRING_ENV_VAR);

                    if (string.IsNullOrEmpty(connectionString))
                        throw new ApplicationException(INVALID_CONNECTION_STRING);

                    var decodedConnectionString = Encoding.ASCII.GetString(
                        Convert.FromBase64String(connectionString));

                    dbContextOptionsBuilder.UseSqlServer(decodedConnectionString);
                });

            services.AddScoped<ICustomersContext, CustomersContext>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ICustomersBusinessService, CustomersBusinessService>();
            services.AddScoped<ICustomerNameValidation, CustomerNameValidation>();

            services.AddSwaggerGen(
                swaggerGenOptions =>
                {
                    swaggerGenOptions.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Title = "Customers API",
                        Version = "v1",
                        Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Email = "jd.ramkumar@gmail.com", Name = "Ramkumar JD", Url = @"http://people.intsol.in/jdramkumar" },
                        Description = "Simple CRM System Service",
                        License = new Swashbuckle.AspNetCore.Swagger.License { Name = "MIT", Url = "http://licenses.intsol.in/apis/mit" },
                        TermsOfService = "All Rights Reserved"
                    });

                    swaggerGenOptions.IncludeXmlComments(GetXmlCommentsPath());
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;

            return Path.Combine(app.ApplicationBasePath, @"IntSol.Libraries.Api.Controllers.Impl.xml");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                swaggerUIOptions =>
                {
                    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers API v1");
                });

            app.UseMvc();
        }
    }
}
