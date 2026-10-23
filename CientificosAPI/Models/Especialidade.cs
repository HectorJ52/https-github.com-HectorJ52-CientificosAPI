using System;
using System.Collections.Generic;

namespace CientificosAPI.Models;

public partial class Especialidade
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Cientifico> Cientificos { get; set; } = new List<Cientifico>();
}
