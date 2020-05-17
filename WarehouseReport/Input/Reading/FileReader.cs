using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WarehouseReport.Input.Reading
{
    public class FileReader : IInputReader
    {
        private readonly string _filename;

        public FileReader(string fileName)
        {
            _filename = fileName;
        }

        public IEnumerable<string> Read()
        {
            if (File.Exists(_filename))
            {
                return File.ReadAllLines(_filename);
            }

            Console.WriteLine($"[WARN] File {_filename} was not found. Returning empty collection...");
            return Enumerable.Empty<string>();
        }
    }
}
