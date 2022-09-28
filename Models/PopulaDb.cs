using Microsoft.EntityFrameworkCore;

namespace mvc1.Models
{
    public static class PopulaDb
    {
        public static void IncluiDadosDB(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            { 
                IncluiDadosDB(scope.ServiceProvider.GetRequiredService<AppDbContext>());            
            }
        }
        public static void IncluiDadosDB(AppDbContext context)
        {
            System.Console.WriteLine("Aplicando Migrations...");
            context.Database.Migrate();
            if (!context.Produtos.Any())
            {
                System.Console.WriteLine("Criando dados...");
                context.Produtos.AddRange(
                    new Produto("Luvas de goleiro", "Futebol", 25),
                    new Produto("Bola de basquete", "Basquete", 48.95m),
                    new Produto("Bola de Futebol", "Futebol", 19.50m),
                    new Produto("Meias Grandes", "Futebol", 50),
                    new Produto("Cesta para quadra", "Basquete", 29.95m)
                );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Dados já existem...");
            }
        }
    }
}
