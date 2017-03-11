using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Search4Self.DAL;
using Search4Self.DAL.Models;
using Search4Self.Parser.Parsers;

namespace Search4Self.Service
{
    public static class ArchiveService
    {
        public const string YoutubeSearch = @"Takeout\Youtube\history\search-history.html";
        public const string YoutubeVideos = @"Takeout\Youtube\history\watch-history.json";
        public const string Searches = @"Takeout\Searches";

        public const string YoutubePythonExecutablePath = @"Parsers\youtube_watched_hist_parser.py";
        public const string SearchesPythonExecutablePath = @"Parsers\";

        public static async Task UnzipAsync(Stream fileStream, Guid userId)
        {
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                var tasks = new List<ConfiguredTaskAwaitable>();

                var searchHistoryPart = archive.Entries.FirstOrDefault(p => p.FullName == YoutubeSearch);
                if (searchHistoryPart != null)
                {
                    using (var stream = searchHistoryPart.Open())
                        tasks.Add(HandleSearchHistoryAsync(stream, userId).ConfigureAwait(false));
                }

                var seenVideosPart = archive.Entries.FirstOrDefault(p => p.FullName == YoutubeVideos);
                if (seenVideosPart != null)
                {
                    using (var stream = seenVideosPart.Open())
                        tasks.Add(HandleSeenVideosHistoryAsync(stream, userId).ConfigureAwait(false));
                }

                var dirName = Path.GetDirectoryName(Searches);
                var searchesFiles = archive.Entries.Where(p => Path.GetDirectoryName(p.FullName) == dirName).ToList();

                if (searchesFiles.Any())
                {
                    tasks.Add(HandleSearchesAsync(searchesFiles, userId).ConfigureAwait(false));
                }

                // Wait for all the tasks to finish
                foreach (var configuredTaskAwaitable in tasks)
                {
                    await configuredTaskAwaitable;
                }
            }
        }

        private static async Task HandleSearchHistoryAsync(Stream stream, Guid userId)
        {
            var wordResult = await VideoSearchHistoryParser.ParseFile(stream).ConfigureAwait(false);

            var models = wordResult.Select(i => new YoutubeSearchHistoryEntity
            {
                Id = Guid.NewGuid(),
                Count = i.Value,
                Word = i.Key,
                UserId = userId
            }).ToArray();

            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.YoutubeSearchHistoryRepository.InsertAll(models);
            }
        }

        private static async Task HandleSeenVideosHistoryAsync(Stream stream, Guid userId)
        {
            var result = await SeenVideosParser.ParseSeenVideosAsync(stream, YoutubePythonExecutablePath).ConfigureAwait(false);
            if (result == null)
            {
                return;
            }

            using (var unitOfWork = new UnitOfWork())
            {
                foreach (var entry in result.Histogram)
                {
                    var models = entry.Value.Select(i => new MusicGenreEntity
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Date = entry.Key,
                        Genre = i.Key,
                        Hits = i.Value
                    }).ToArray();

                    unitOfWork.MusicGenreRepository.InsertAll(models);
                }
            }
        }

        private static async Task HandleSearchesAsync(IEnumerable<ZipArchiveEntry> searchesFiles, Guid userId)
        {
            var result = await SearchesParser.ParseSeenVideosAsync(searchesFiles, SearchesPythonExecutablePath).ConfigureAwait(false);
            if (result == null)
            {
                return;
            }

            using (var unitOfWork = new UnitOfWork())
            {
                var models = result.Select(i => new SearchEntity
                {
                    UserId = userId,
                    Id = Guid.NewGuid(),
                    Count = i.Value,
                    Query = i.Key
                }).ToArray();

                unitOfWork.SearchesRepository.InsertAll(models);
            }
        }
    }
}
