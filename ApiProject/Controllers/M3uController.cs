using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class M3uController : ControllerBase
    {
        // GET: api/M3u/GetChannelUrls
        [HttpGet("GetChannelUrls")]
        public ActionResult<IEnumerable<string>> GetChannelUrls()
        {
            try
            {
                // Ruta del archivo M3U en la carpeta 'Assets'
                string m3uFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "listaCR.m3u");

                // Lee el archivo M3U
                string[] lines = System.IO.File.ReadAllLines(m3uFilePath);

                // Patrón para buscar líneas que contengan URLs de canales
                string pattern = @"^(?!#)(http[s]?:\/\/\S+)$";

                List<string> channelUrls = new List<string>();

                foreach (string line in lines)
                {
                    // Comprueba si la línea coincide con el patrón de URL
                    Match match = Regex.Match(line, pattern);

                    if (match.Success)
                    {
                        // Si hay una coincidencia, agrega la URL del canal a la lista
                        string url = match.Groups[1].Value;
                        channelUrls.Add(url);
                    }
                }

                return Ok(channelUrls);
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir durante la lectura del archivo
                return StatusCode(500, "Error al procesar el archivo M3U: " + ex.Message);
            }
        }
    }
}
