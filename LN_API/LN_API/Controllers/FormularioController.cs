using LN_API.App_Start;
using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LN_API.Controllers
{
    public class FormularioController : ApiController
    {
        [HttpPost]
        [Route("api/Fromulario")]
        [AllowAnonymous]
        public FormularioEnt Formulario(FormularioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.FormularioUsuario
                             select new
                             {
                                 x.IdFormulario,
                                 x.IdUsuario,
                                 x.CorreoElectronico,
                                 x.Nombre,
                                 x.Descripcion
                             }).FirstOrDefault();

                if (datos != null)
                {
                    FormularioEnt resp = new FormularioEnt();
                    resp.CorreoElectronico = datos.CorreoElectronico;
                    resp.Nombre = datos.Nombre;
                    resp.Descripcion = datos.Descripcion;
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
