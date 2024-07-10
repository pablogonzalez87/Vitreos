using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using Tienda_Vidreos.Entities;
using LN_WEB.Entities;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace LN_WEB.Models
{
    public class CarritoModel
    {
        public List<CarritoEnt> ConsultaVidreoUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                string url = $"{ConfigurationManager.AppSettings["urlApi"]}api/ConsultaVidreoUsuario?q={q}";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result ?? new List<CarritoEnt>();
                }

                return new List<CarritoEnt>();
            }
        }

        public List<CarritoEnt> ConsultaVidreosUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                string url = $"{ConfigurationManager.AppSettings["urlApi"]}api/ConsultaVidreosUsuario?q={q}";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result ?? new List<CarritoEnt>();
                }

                return new List<CarritoEnt>();
            }
        }

        public List<CarritoEnt> ConsultaVidreoCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string url = $"{ConfigurationManager.AppSettings["urlApi"]}api/ConsultaVidreoCarrito?q={q}";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result ?? new List<CarritoEnt>();
                }

                return new List<CarritoEnt>();
            }
        }

        public int AgregarVidreoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = $"{ConfigurationManager.AppSettings["urlApi"]}api/AgregarVidreoCarrito";
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    var jsonResponse = resp.Content.ReadAsStringAsync().Result;
                    try
                    {
                        return int.Parse(jsonResponse);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deserializando la respuesta: {ex.Message}");
                        Console.WriteLine($"Contenido de la respuesta: {jsonResponse}");
                        return 0;
                    }
                }
                else
                {
                    var errorResponse = resp.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Error en la solicitud: {resp.StatusCode}");
                    Console.WriteLine($"Contenido del error: {errorResponse}");
                }

                return 0;
            }
        }

        public int RemoverVidreoCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string url = $"{ConfigurationManager.AppSettings["urlApi"]}api/RemoverVidreoCarrito?q={q}";
                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    var jsonResponse = resp.Content.ReadAsStringAsync().Result;
                    try
                    {
                        return int.Parse(jsonResponse);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deserializando la respuesta: {ex.Message}");
                        Console.WriteLine($"Contenido de la respuesta: {jsonResponse}");
                        return 0;
                    }
                }
                else
                {
                    var errorResponse = resp.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Error en la solicitud: {resp.StatusCode}");
                    Console.WriteLine($"Contenido del error: {errorResponse}");
                }

                return 0;
            }
        }
        public async Task<long> SubirImagenComprobante(HttpPostedFileBase archivo)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ImagenComprobante";

                var content = new MultipartFormDataContent();
                if (archivo != null && archivo.ContentLength > 0)
                {
                    var streamContent = new StreamContent(archivo.InputStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);
                    content.Add(streamContent, "file", archivo.FileName);
                }
                else
                {
                    throw new ArgumentException("No se ha proporcionado un archivo válido.");
                }

                HttpResponseMessage resp = await client.PostAsync(url, content);

                if (resp.IsSuccessStatusCode)
                {
                    return await resp.Content.ReadFromJsonAsync<long>();
                }

                throw new Exception("Error al subir la imagen del comprobante.");
            }
        }

        public int PagarVidreoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = $"{ConfigurationManager.AppSettings["urlApi"]}api/PagarVidreoCarrito";
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    var jsonResponse = resp.Content.ReadAsStringAsync().Result;
                    try
                    {
                        return int.Parse(jsonResponse);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deserializando la respuesta: {ex.Message}");
                        Console.WriteLine($"Contenido de la respuesta: {jsonResponse}");
                        return 0;
                    }
                }
                else
                {
                    var errorResponse = resp.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Error en la solicitud: {resp.StatusCode}");
                    Console.WriteLine($"Contenido del error: {errorResponse}");
                }

                return 0;
            }
        }
    }
}
