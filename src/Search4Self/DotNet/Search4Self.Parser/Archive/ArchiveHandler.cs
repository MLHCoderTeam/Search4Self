using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using Search4Self.Parser.Parsers;

namespace Search4Self.Parser.Archive
{
    public static class ArchiveHandler
    {
        public const string YoutubeSearch = @"Takeout\Youtube\history\search-history.html";
        public const string YoutubeVideos = @"Takeout\Youtube\history\watch-history.json";

        public const string PythonExecutablePath = @"";

        public static async Task UnzipAsync(Stream fileStream)
        {
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                var tasks = new List<ConfiguredTaskAwaitable>();

                var searchHistoryPart = archive.Entries.FirstOrDefault(p => p.FullName == YoutubeSearch);
                if (searchHistoryPart != null)
                {
                    using (var stream = searchHistoryPart.Open())
                        tasks.Add(HandleSearchHistoryAsync(stream).ConfigureAwait(false));
                }

                var seenVideosPart = archive.Entries.FirstOrDefault(p => p.FullName == YoutubeVideos);
                if (seenVideosPart != null)
                {
                    using (var stream = seenVideosPart.Open())
                        tasks.Add(HandleSeenVideosAsync(stream).ConfigureAwait(false));
                }

                // Wait for all the tasks to finish
                foreach (var configuredTaskAwaitable in tasks)
                {
                    await configuredTaskAwaitable;
                }
            }
        }

        private static async Task HandleSearchHistoryAsync(Stream stream)
        {
            var wordResult = await VideoSearchHistoryParser.ParseFile(stream).ConfigureAwait(false);
        }

        private static async Task HandleSeenVideosAsync(Stream stream)
        {
            var fileName = Guid.NewGuid().ToString();

            try
            {
                using (var file = File.Create(fileName))
                {
                    await stream.CopyToAsync(file).ConfigureAwait(false);
                }

                var processInfo = new ProcessStartInfo(PythonExecutablePath)
                {
                    Arguments = $"python {fileName}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                string executionResult;
                using (var process = Process.Start(processInfo))
                using (var reader = process?.StandardOutput)
                {
                    if (reader == null)
                    {
                        return;
                    }

                    executionResult = await reader.ReadToEndAsync().ConfigureAwait(false);
                }

                await SeenVideosParser.ParseSeenVideosAsync(executionResult).ConfigureAwait(false);
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
