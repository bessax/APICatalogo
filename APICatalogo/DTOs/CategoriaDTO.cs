﻿using APICatalogo.Models;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }   
        public string? Nome { get; set; }   
        public string? ImagemURL { get; set; }
        public ICollection<ProdutoDTO>? Produtos { get; set; }
    }
}
