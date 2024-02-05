using LN_API.App_Start;
using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace LN_API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("api/IniciarSesion")]
        [AllowAnonymous]
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            TokenGenerator tok = new TokenGenerator();

            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             join y in bd.Rol on x.IdRol equals y.IdRol
                             where x.CorreoElectronico == entidad.CorreoElectronico
                                      && x.Contrasenna == entidad.Contrasenna
                                      && x.Estado == true
                             select new 
                             { 
                                x.IdUsuario,
                                x.CorreoElectronico,
                                x.Nombre,
                                x.Identificacion,
                                x.Estado,
                                x.IdRol,
                                x.Caducidad,
                                x.ClaveTemporal,
                                y.NombreRol
                             }).FirstOrDefault();

                if (datos != null)
                {
                    if (datos.ClaveTemporal.Value && datos.Caducidad < DateTime.Now)
                    {
                        return null;
                    }

                    UsuarioEnt resp = new UsuarioEnt();
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.IdRol = datos.IdRol;
                    resp.NombreRol = datos.NombreRol;
                    resp.IdUsuario = datos.IdUsuario;
                    resp.Token = tok.GenerateTokenJwt(datos.IdUsuario);
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpPost]
        [Route("api/RegistrarUsuario")]
        [AllowAnonymous]
        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                Usuario tabla = new Usuario();
                tabla.CorreoElectronico = entidad.CorreoElectronico;
                tabla.Contrasenna = entidad.Contrasenna;
                tabla.Identificacion = entidad.Identificacion;
                tabla.Nombre = entidad.Nombre;
                tabla.Estado = entidad.Estado;
                tabla.IdRol = entidad.IdRol;
                tabla.ClaveTemporal = false;
                tabla.Caducidad = DateTime.Now;

                bd.Usuario.Add(tabla);
                return bd.SaveChanges();
            }

            /*using (var bd = new Tienda_VidreosEntitiesntities())
            {
                return bd.RegistrarUsuario(entidad.CorreoElectronico,
                                           entidad.Contrasenna,
                                           entidad.Identificacion,
                                           entidad.Nombre,
                                           entidad.Estado,
                                           entidad.IdRol);
            }*/
        }

        [HttpPost]
        [Route("api/RecuperarClave")]
        [AllowAnonymous]
        public bool RecuperarClave(UsuarioEnt entidad)
        {
            UtilitariosModel util = new UtilitariosModel();

            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.CorreoElectronico == entidad.CorreoElectronico
                                           && x.Estado == true
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    string pass = util.CreatePassword();
                    string mensaje = "Estimado(a): " + datos.Nombre + ". Se ha generado la siguiente contraseña temporal: " + pass + " valida por los siguientes 15 minutos";
                    util.SendEmail(datos.CorreoElectronico, "Recuperar Contraseña", mensaje);

                    //Update de LiQ
                    datos.Contrasenna = pass;
                    datos.ClaveTemporal = true;
                    datos.Caducidad = DateTime.Now.AddMinutes(15);
                    bd.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        [HttpPut]
        [Route("api/CambiarClave")]
        public int CambiarClave(UsuarioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Contrasenna = entidad.ContrasennaNueva;
                    datos.ClaveTemporal = false;
                    datos.Caducidad = DateTime.Now;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultaUsuarios")]
        public List<UsuarioEnt> ConsultaUsuarios()
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<UsuarioEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new UsuarioEnt
                        {
                            CorreoElectronico = item.CorreoElectronico,
                            Nombre = item.Nombre,
                            Identificacion = item.Identificacion,
                            Estado = item.Estado,
                            IdRol = item.IdRol,
                            IdUsuario = item.IdUsuario
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<UsuarioEnt>();
                }
            }
        }

        [HttpPut]
        [Route("api/CambiarEstado")]
        public int CambiarEstado(UsuarioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    bool estadoActual = datos.Estado;

                    datos.Estado = (estadoActual == true ? false : true);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpGet]
        [Route("api/ConsultaUsuario")]
        public UsuarioEnt ConsultaUsuario(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    UsuarioEnt resp = new UsuarioEnt();
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Nombre = datos.Nombre;
                    resp.Identificacion = datos.Identificacion;
                    resp.Estado = datos.Estado;
                    resp.IdRol = datos.IdRol;
                    resp.IdUsuario = datos.IdUsuario;
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        [Route("api/ConsultaRoles")]
        public List<RolEnt> ConsultaRoles()
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Rol
                             where x.Estado == true
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<RolEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new RolEnt
                        {
                            IdRol = item.IdRol,
                            NombreRol = item.NombreRol,
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<RolEnt>();
                }
            }
        }

        [HttpPut]
        [Route("api/EditarUsuario")]
        public int EditarUsuario(UsuarioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Usuario
                             where x.IdUsuario == entidad.IdUsuario
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.CorreoElectronico = entidad.CorreoElectronico;
                    datos.IdRol = entidad.IdRol;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }
        
    }
}
