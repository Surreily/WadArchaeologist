using System;
using System.IO;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Search;

namespace Surreily.WadArchaeologist.ConsoleClient {
    public class Program {
        public static void Main(string[] args) {
            // TODO: Get user arguments.

            string wadArchiveFilePath = @"E:\Doom\IWADs\DOOM2.WAD";
            WadContext wad = new WadContext();

            byte[] data;

            using (Stream stream = new FileStream(wadArchiveFilePath, FileMode.Open)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    data = reader.ReadBytes((int)stream.Length);
                }
            }

            SearchContext search = new SearchContext {
                MinimumNumberOfSidesPerMap = 10,
            };

            SideSearcher searcher = new SideSearcher();
            searcher.Search(search, wad, data);

            Console.WriteLine("Test");
        }
    }
}
