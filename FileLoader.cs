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
            try
            {
                var files = Directory.GetFiles(filePath);

                Task<int> t0 = Task.Run(() => CheckSpacesInFileInPathAsync(files, 0));
                Task<int> t1 = Task.Run(() => CheckSpacesInFileInPathAsync(files, 1));
                Task<int> t2 = Task.Run(() => CheckSpacesInFileInPathAsync(files, 2));

                Task.WaitAll();

                return t0.Result + t1.Result + t2.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public int CheckSpacesInFileInPathAsync(string[] files, int threadNum)
        {
            int spaceCount = 0;

            for (int i = threadNum; i < files.Length; i += 3)
            {
                spaceCount += CheckSpacesInFile(files[i]);
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
