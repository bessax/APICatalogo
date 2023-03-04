using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context):base(context) { }
            public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(x=>x.Produtos).ToList();
        }
    }
}
