using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context):base(context) { }
        public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
           return await Get().Include(x=>x.Produtos).ToListAsync();
        }

        public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters parameters)
        {  
           return await PagedList<Categoria>.ToPagedList(Get().Include(x => x.Produtos).OrderBy(x => x.Nome), parameters.PageNumber, parameters.PageSize);
        }
    }
}
