using System;
using System.ComponentModel.DataAnnotations;

public class Error
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Descripción es requerido.")]
    public string Descripcion { get; set; }

    [Display(Name = "Fecha del Error")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    public string Usuario { get; set; }

    public string Tipo { get; set; }
}