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
        public static async Task<SeenVideosHistoryModel> ParseSeenVideosAsync(string appDir, Stream stream, string pythonExecutablePath)
        {
            var fileName = Path.Combine(appDir, "temp", Guid.NewGuid().ToString());

            try
            {
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream).ConfigureAwait(false);
                }

                var processInfo = new ProcessStartInfo(ConfigurationManager.AppSettings["PythonPath"])
                {
                    Arguments = $"\"{Path.Combine(appDir, pythonExecutablePath)}\" \"{fileName}\"",
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

                    executionResult = reader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<SeenVideosHistoryModel>(executionResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
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
