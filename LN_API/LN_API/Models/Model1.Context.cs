﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LN_API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Tienda_VidreosEntities : DbContext
    {
        public Tienda_VidreosEntities()
            : base("name=Tienda_VidreosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vidreo> Vidreo { get; set; }
        public virtual DbSet<VidreoCarrito> VidreoCarrito { get; set; }
        public virtual DbSet<VidreoUsuario> VidreoUsuario { get; set; }
    
        public virtual int AjustarStockVidreo(Nullable<long> idVidreo, Nullable<int> nuevaCantidad)
        {
            var idVidreoParameter = idVidreo.HasValue ?
                new ObjectParameter("IdVidreo", idVidreo) :
                new ObjectParameter("IdVidreo", typeof(long));
    
            var nuevaCantidadParameter = nuevaCantidad.HasValue ?
                new ObjectParameter("NuevaCantidad", nuevaCantidad) :
                new ObjectParameter("NuevaCantidad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AjustarStockVidreo", idVidreoParameter, nuevaCantidadParameter);
        }
    
        public virtual int DecrementarStockVidreo(Nullable<long> idVidreo, Nullable<int> cantidad)
        {
            var idVidreoParameter = idVidreo.HasValue ?
                new ObjectParameter("IdVidreo", idVidreo) :
                new ObjectParameter("IdVidreo", typeof(long));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DecrementarStockVidreo", idVidreoParameter, cantidadParameter);
        }
    
        public virtual int IncrementarStockVidreo(Nullable<long> idVidreo, Nullable<int> cantidad)
        {
            var idVidreoParameter = idVidreo.HasValue ?
                new ObjectParameter("IdVidreo", idVidreo) :
                new ObjectParameter("IdVidreo", typeof(long));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IncrementarStockVidreo", idVidreoParameter, cantidadParameter);
        }
    
        public virtual ObjectResult<IniciarSesion_Result> IniciarSesion(string correoElectronico, string contrasenna)
        {
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<IniciarSesion_Result>("IniciarSesion", correoElectronicoParameter, contrasennaParameter);
        }
    
        public virtual int RegistrarUsuario(string correoElectronico, string contrasenna, string identificacion, string nombre, Nullable<bool> estado, Nullable<int> idRol)
        {
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("Identificacion", identificacion) :
                new ObjectParameter("Identificacion", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var estadoParameter = estado.HasValue ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(bool));
    
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarUsuario", correoElectronicoParameter, contrasennaParameter, identificacionParameter, nombreParameter, estadoParameter, idRolParameter);
        }
    }
}
