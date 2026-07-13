using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiCoreID.Models;

public partial class Photo
{
    public int IdPhoto { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string Descripcion { get; set; } = null!;
    [Required]
    public string Url { get; set; } = null!;
}
