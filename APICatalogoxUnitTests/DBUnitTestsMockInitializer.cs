using APICatalogo.Context;
using APICatalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogoxUnitTests
{
    internal class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {
            
        }
        public void Seed(AppDbContext? context)
        {
            context.Categorias.Add(
                new Categoria { CategoriaId = 999, Nome = "bebibas999", ImagemURL = "imagem1.jpg"});
            context.Categorias.Add(
                new Categoria { CategoriaId = 998, Nome = "bebibas998", ImagemURL = "imagem2.jpg" });
            context.Categorias.Add(
                new Categoria { CategoriaId = 997, Nome = "bebibas997", ImagemURL = "imagem3.jpg" });
            context.Categorias.Add(
                new Categoria { CategoriaId = 996, Nome = "bebibas996", ImagemURL = "imagem4.jpg" });
            context.Categorias.Add(
                new Categoria { CategoriaId = 995, Nome = "bebibas995", ImagemURL = "imagem5.jpg" });

            context.SaveChanges();

        }
    }
}
