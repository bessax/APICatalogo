using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context):base(context) { }

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters parameters)
        {
            //return Get()
            //    .OrderBy(on=>on.Nome)
            //    .Skip((parameters.PageNumber -1) * parameters.PageSize)
            //    .Take(parameters.PageSize)
            //    .ToList();

            return await PagedList<Produto>.ToPagedList(Get().OrderBy(x=>x.Nome), parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(c=>c.Preco).ToListAsync();
        }
    }
}
