using System.Text;

namespace OTUS_FileParallelLoad
{
    public class FileLoader
    {
        public int CheckSpacesIn3Files(string file1, string file2, string file3)
        {
            Task<int> t0 = Task.Run(() => CheckSpacesInFile(file1));
            Task<int> t1 = Task.Run(() => CheckSpacesInFile(file2));
            Task<int> t2 = Task.Run(() => CheckSpacesInFile(file3));
            
            Task.WaitAll();

            return t0.Result + t1.Result + t2.Result;
        }

        public int CheckSpacesInFilesInPath(string filePath)
        {
            int spaceCount = 0;

            try
            {
                var files = Directory.GetFiles(filePath);

                foreach (var file in files)
                {
                    spaceCount += CheckSpacesInFile(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return spaceCount;
        }

        public int CheckSpacesInFile(string filePath)
        {
            int spaceCount = 0;

            try
            {
                using (var reader = new StreamReader(filePath, detectEncodingFromByteOrderMarks: true))
                {
                    while (reader.Peek() > -1)
                    {
                        if ((char)reader.Read() == ' ')
                        {
                            spaceCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return spaceCount;
        }

        public void FileCreate(string filePath, string input)
        {
            try
            {
                using (FileStream fs = File.Create(filePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(input);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
