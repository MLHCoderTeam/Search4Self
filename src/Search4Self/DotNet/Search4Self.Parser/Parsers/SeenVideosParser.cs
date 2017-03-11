using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Search4Self.Parser.Models;
using System.Configuration;

namespace Search4Self.Parser.Parsers
{
    public static class SeenVideosParser
    {
        public static async Task<SeenVideosHistoryModel> ParseSeenVideosAsync(Stream stream, string pythonExecutablePath)
        {
            var fileName = Path.Combine("temp", Guid.NewGuid().ToString());

            try
            {
                using (var file = File.Create(fileName))
                {
                    await stream.CopyToAsync(file).ConfigureAwait(false);
                }

                var processInfo = new ProcessStartInfo(ConfigurationManager.AppSettings["PythonPath"])
                {
                    Arguments = $"{pythonExecutablePath} {fileName}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                string executionResult;
                using (var process = Process.Start(processInfo))
                using (var reader = process?.StandardOutput)
                {
                    if (reader == null)
                    {
                        return null;
                    }

                    executionResult = await reader.ReadToEndAsync().ConfigureAwait(false);
                }

                return JsonConvert.DeserializeObject<SeenVideosHistoryModel>(executionResult);
            }
            finally
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }
    }
}
