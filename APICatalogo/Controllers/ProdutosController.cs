using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(x=>x.ProdutoId==id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null )
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        //Tenho que atualizar todos os dados do produtos.
        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Produto produto)
        {

            if (id != produto.ProdutoId)
            {
                return BadRequest("Id's enviados diferentes.");
            }
            _context.Entry(produto).State= EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

    }
}
