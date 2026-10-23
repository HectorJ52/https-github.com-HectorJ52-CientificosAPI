using System;
using System.Collections.Generic;

namespace CientificosAPI.Models;

public partial class Descubrimiento
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly? FechaDescubrimiento { get; set; }

    public string? Lugar { get; set; }

    public int? CientificoId { get; set; }

    public virtual Cientifico? Cientifico { get; set; }
}
