// using System.Collections.Generic;
// using System.Data;
// using CodeWorks.Auth0Provider;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.OpenApi.Models;
// using MySqlConnector;
// using spring21_burgershack.Repositories;
// using spring21_burgershack.Services;

// namespace burgershack
// {
//   public class Startup
//   {
//     public Startup(IConfiguration configuration)
//     {
//       Configuration = configuration;
//     }

//     public IConfiguration Configuration { get; }
//     public void ConfigureServices(IServiceCollection services)
//     {
//       ConfigureCors(services);
//       ConfigureAuth(services);
//       services.AddControllers();
//       services.AddSwaggerGen(c =>
//       {
//         c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmaZen", Version = "v1" });
//       });

//       services.AddScoped<IDbConnection>(x => CreateDbConnection());

//       // REPOS
//       services.AddScoped<AccountRepository>();
//       services.AddScoped<BurgersRepository>();
//       services.AddScoped<SidesRepository>();
//       services.AddScoped<DrinksRepository>();
//       services.AddScoped<WarehousesRepository>();
//       services.AddScoped<ProductsRepository>();
//       services.AddScoped<WarehouseProductsRepository>();

//       // BUSINESS RULES

//       services.AddTransient<AccountService>();
//       services.AddTransient<BurgersService>();
//       services.AddTransient<SidesService>();
//       services.AddTransient<DrinksService>();
//       services.AddTransient<WarehousesService>();
//       services.AddTransient<ProductsService>();
//       services.AddTransient<WarehouseProductsService>();

//     }

//     private void ConfigureCors(IServiceCollection services)
//     {
//       services.AddCors(options =>
//       {
//         options.AddPolicy("CorsDevPolicy", builder =>
//               {
//                 builder
//                       .AllowAnyMethod()
//                       .AllowAnyHeader()
//                       .AllowCredentials()
//                       .WithOrigins(new string[]{
//                         "http://localhost:8080", "http://localhost:8081"
//                   });
//               });
//       });
//     }

//     private void ConfigureAuth(IServiceCollection services)
//     {
//       services.AddAuthentication(options =>
//       {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//       }).AddJwtBearer(options =>
//       {
//         options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
//         options.Audience = Configuration["Auth0:Audience"];
//       });

//     }

//     private IDbConnection CreateDbConnection()
//     {
//       string connectionString = Configuration["DB:gearhost"];
//       return new MySqlConnection(connectionString);
//     }

//     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//       if (env.IsDevelopment())
//       {
//         app.UseDeveloperExceptionPage();
//         app.UseSwagger();
//         app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmaZen v1"));
//         app.UseCors("CorsDevPolicy");
//       }
//       Auth0ProviderExtension.ConfigureKeyMap(new List<string>() { "id" });

//       app.UseHttpsRedirection();

//       app.UseRouting();

//       app.UseAuthentication();

//       app.UseAuthorization();

//       app.UseEndpoints(endpoints =>
//       {
//         endpoints.MapControllers();
//       });
//     }
//   }
// }