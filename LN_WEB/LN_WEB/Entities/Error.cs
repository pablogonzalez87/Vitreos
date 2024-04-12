using System;


namespace LN_WEB.Entities
{
    public class Error 
    { 
    public int ErrorId { get; set; } 
    public string Descripcion { get; set; } 
    public string CorreoElectronico { get; set; } 
    public string NumeroTelefonico { get; set; } 
    public string DetallesError { get; set; } 
    public DateTime FechaCreacion { get; set; } 
    
    }
}