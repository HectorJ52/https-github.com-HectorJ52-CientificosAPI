using System;
using System.Collections.Generic;

namespace CientificosAPI.Models;

public partial class Cientifico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Nacionalidad { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Descubrimiento> Descubrimientos { get; set; } = new List<Descubrimiento>();

    public virtual ICollection<Investigacione> Investigaciones { get; set; } = new List<Investigacione>();

    public virtual ICollection<Especialidade> Especialidads { get; set; } = new List<Especialidade>();
}
