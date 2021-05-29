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
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Repository;

namespace WebApplication1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<StoreUser, IdentityRole>(options => {
				options.Password.RequiredLength = 4;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
			})
           .AddEntityFrameworkStores<ClothingStoreDBContext>();

			var connection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ClothingStoreDBContext>(options => options.UseSqlServer(connection));
			services.AddScoped<IRepository2, RepositorySQL>();

			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});

			services.AddDbContext<ClothingStoreDBContext>(opt => opt.UseInMemoryDatabase("ClothingStore"));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddMvc().AddJsonOptions(Options =>
			{
				var resolver = Options.SerializerSettings.ContractResolver;
				if (resolver != null)
					(resolver as DefaultContractResolver).NamingStrategy = null;
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = "SimpleWebApp";
				options.LoginPath = "/";
				options.AccessDeniedPath = "/";
				options.LogoutPath = "/";
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
				options.Events.OnRedirectToAccessDenied = context =>
				{
					context.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
			});

	}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
		{
			CreateUserRoles(services).Wait();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseAuthentication();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
		private async Task CreateUserRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<StoreUser>>();

			// Создание ролей администратора и пользователя
			if (await roleManager.FindByNameAsync("admin") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("admin"));
			}
			if (await roleManager.FindByNameAsync("user") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("user"));
			}

			// Создание Администратора
			
			if (await userManager.FindByNameAsync("79109846666") == null)
			{
				StoreUser admin = new StoreUser { FirstName = "Александра", LastName = "Прищеп", MiddleName = "Александровна", PhoneNumber = "79109846666", UserName = "79109846666" };
				IdentityResult result = await userManager.CreateAsync(admin, "a6x3666");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(admin, "admin");
				}
			}

			// Создание Пользователя
	
			if (await userManager.FindByNameAsync("79106670707") == null)
			{
				StoreUser user = new StoreUser { FirstName = "Александра", LastName = "Блинова", MiddleName = "Валерьевна", PhoneNumber = "79106670707", UserName = "79106670707" };
				IdentityResult result = await userManager.CreateAsync(user, "blinchik777");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "user");
				}
			}
		}

	}
}
