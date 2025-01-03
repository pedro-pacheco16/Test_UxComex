using Teste_UxComex.Repositories.Interfaces;
using Teste_UxComex.Repositories;

namespace Teste_UxComex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
            builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pessoa}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "endereco",
                pattern: "Endereco/{action=Index}/{id?}",
                defaults: new { controller = "Endereco" });


            app.Run();
        }
    }
}
