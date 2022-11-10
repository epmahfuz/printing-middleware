using Infrastructure.Shared.Models;
using KioskPaymentMiddleware.ServiceExtensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace FnbPrintingMiddleware
{
    public class Startup
    {
        public IConfiguration _Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHostedService<ApplicationLifeCycleHookService>();

            services.AddOptions();
           // services.Configure<PaymentConfiguration>(_Configuration.GetSection("PaymentConfiguration"));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.RegisterApplicationServices(_Configuration);

            //services.AddPaymentServices();
           // services.AddPaymentCommandHandlerMediatorServices();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PaymentMiddleware"
                });
            });

            #endregion Swagger

            #region CORS

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5009");
                });
            });

            #endregion CORS
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

           // getTerminalReady(sixPaymentService, sixPayment);

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentMiddleware v1");
            });

            #endregion


            app.UseCors("CorsPolicy");

            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        
    }
}