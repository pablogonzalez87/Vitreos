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
    public class VidreoController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultaVidreos")]
        public List<VidreoEnt> ConsultVidreos()
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidreo
                             select x).ToList();

                if (datos.Count > 0)
                {
                    List<VidreoEnt> res = new List<VidreoEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new VidreoEnt
                        {
                            IdVidreo = item.idVidreo,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Imagen = item.Imagen,
                       
                        });
                    }
                    return res;
                }
                else
                {
                    return new List<VidreoEnt>();
                }
            }
        }

    }
}
