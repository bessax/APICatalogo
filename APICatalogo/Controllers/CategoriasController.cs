using APICatalogo.Models;
using APICatalogo.Repository.UofW;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger _logger;
    public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger)
    {
        _uof = uof;
        _logger = logger;
    }

    [HttpGet("categorias")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        var categoria = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
        if (categoria is null)
        {
            return NotFound("Categoria não encontrados.");
        }
        return Ok(categoria);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            var categoria = _uof.CategoriaRepository.Get().ToList();
            if (categoria is null)
            {
                return NotFound("Categoria não encontrados.");
            }
            return categoria;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,"Ocorreum um problema ao tratar sua solicitação.");
        }    
               
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            var categoria = _uof.CategoriaRepository.GetById(x => x.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrado.");
            }
            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreum um problema ao tratar sua solicitação.");
        }        
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest();
        }
        _uof.CategoriaRepository.Add(categoria);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    //Tenho que atualizar todos os dados do produtos.
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {

        if (id != categoria.CategoriaId)
        {
            return BadRequest("Id's enviados diferentes.");
        }
        _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.GetById(x => x.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound("Categoria não encontrado.");
        }
        _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        return Ok(categoria);
    }

}
