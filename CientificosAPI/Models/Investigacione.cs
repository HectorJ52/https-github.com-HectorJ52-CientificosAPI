using System;
using System.Collections.Generic;

namespace CientificosAPI.Models;

public partial class Investigacione
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? AreaEstudio { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Estado { get; set; }

    public int? CientificoId { get; set; }

    public virtual Cientifico? Cientifico { get; set; }
}
