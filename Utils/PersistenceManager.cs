using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SchoolApp.Repositories;

namespace SchoolApp.Utils
{
    public class PersistenceManager
    {
        public static void SaveToFile<T>(string filePath, InMemoryRepository<T> repo) where T : class
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var data = repo.GetStore();
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The repository data cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            var json = JsonSerializer.Serialize(data, options);

            File.WriteAllText(filePath, json);
        }

        public static void LoadFromFile<T>(string filePath, InMemoryRepository<T> repo) where T : class
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file at {filePath} does not exist.");
            }
            var json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var data = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json, options);
            if (data == null)
            {
                throw new InvalidOperationException("Deserialized data cannot be null.");
            }
            foreach (var item in data)
            {
                repo.Add(item.Value);
            }
        }


    }
}
