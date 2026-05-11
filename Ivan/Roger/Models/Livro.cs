using System;

namespace Roger.Models;

public class Livro
{
    public String Id { get; set; } = Guid.NewGuid().ToString();
    public String? Nome { get; set; }
    public String? Autor { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public Boolean? Disponivel { get; set; }

}
