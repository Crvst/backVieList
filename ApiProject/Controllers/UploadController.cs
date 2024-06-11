using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Model;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly ApiProjectContext _context;

    public UploadController(ApiProjectContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Endpoint para subir y procesar un archivo M3U.
    /// </summary>
    /// <param name="file">Archivo M3U a ser procesado</param>
    /// <returns>Resultado de la operación</returns>
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        if (!file.FileName.EndsWith(".m3u", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Invalid file format. Only .m3u files are allowed.");
        }

        try
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = await reader.ReadToEndAsync();
                var channels = ParseM3UContent(content);

                if (channels == null || !channels.Any())
                {
                    return BadRequest("The file does not contain valid channel information.");
                }

                // Guardar los canales en la base de datos
                await SaveChannelsToDatabase(channels);

                return Ok("File uploaded successfully");
            }
        }
        catch (Exception ex)
        {
            // Log the exception (not implemented in this snippet)
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
        }
    }

    /// <summary>
    /// Parse the content of an M3U file and extract channel information.
    /// </summary>
    /// <param name="content">Content of the M3U file</param>
    /// <returns>List of channels extracted from the M3U file</returns>
    private List<Canal> ParseM3UContent(string content)
    {
        var channels = new List<Canal>();

        // Regular expression to extract channel information
        var regex = new Regex(@"#EXTINF:.*?tvg-logo=""(.*?)"".*?group-title=""(.*?)"".*?,(.*?)\r?\n(.*?)\r?\n");

        var matches = regex.Matches(content);

        foreach (Match match in matches)
        {
            var iconUrl = match.Groups[1].Value;
            var groupTitle = match.Groups[2].Value;
            var channelName = match.Groups[3].Value.Trim();
            var channelUrl = match.Groups[4].Value.Trim();

            var channel = new Canal
            {
                group_title = groupTitle,
                nombre = channelName,
                url = channelUrl,
                icon_url = iconUrl
            };

            channels.Add(channel);
        }

        return channels;
    }

    /// <summary>
    /// Save a list of channels to the database.
    /// </summary>
    /// <param name="channels">List of channels to be saved</param>
    private async Task SaveChannelsToDatabase(List<Canal> channels)
    {
        // Use AddRangeAsync for bulk insert
        await _context.Canal.AddRangeAsync(channels);
        await _context.SaveChangesAsync();
    }
}
