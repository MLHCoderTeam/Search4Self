﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Search4Self.Parser.Parsers
{
    public static class SearchesParser
    {
        public static async Task<IDictionary<string, int>> ParseSeenVideosAsync(IEnumerable<ZipArchiveEntry> searchesFiles,
            string pythonExecutablePath)
        {
            var folderName = Guid.NewGuid().ToString();

            try
            {
                var folder = Directory.CreateDirectory(folderName);

                foreach (var entry in searchesFiles)
                {
                    using (var stream = entry.Open())
                    using (var file = File.Create(Path.Combine(folder.Name, entry.Name)))
                    {
                        await stream.CopyToAsync(file).ConfigureAwait(false);
                    }
                }

                var processInfo = new ProcessStartInfo(pythonExecutablePath)
                {
                    Arguments = $"python {folderName}",
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

                return JsonConvert.DeserializeObject<IDictionary<string, int>>(executionResult);
            }
            finally
            {
                if (Directory.Exists(folderName))
                {
                    Directory.Delete(folderName);
                }
            }
        }
    }
}
