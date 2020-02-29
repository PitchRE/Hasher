using System;
using System.IO;
using System.Security.Cryptography;

public class HashDirectory
{
   
    public static void Main(String[] args)
    {
        Console.WriteLine("Please wait, checking files...");

        string MBPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));







        // Determine whether the directory exists.
        if (Directory.Exists(@"debug"))
        {
     
      
        } else
        {
           Directory.CreateDirectory(@"debug");
        }

 
        if (File.Exists(@"debug/checksum.txt"))
        {
            File.Delete(@"debug/checksum.txt");
        }


        if (Directory.Exists(MBPath))
        {
            // Create a DirectoryInfo object representing the specified directory.
            var dir = new DirectoryInfo(MBPath);
            // Get the FileInfo objects for every file in the directory.
         
            string[] filesNum = Directory.GetFiles(MBPath, "*.*", SearchOption.AllDirectories);
            int curNum = 0;
            DirSearch(MBPath, filesNum);

          
       
                foreach (string fileName in Directory.GetFiles(MBPath))
                {
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    Console.Write("\r  " + curNum + " /~ " + filesNum.Length);
                    var fi1 = new FileInfo(fileName);
                    // Create a fileStream for the file.
                    FileStream fileStream = fi1.Open(FileMode.Open);
                    // Be sure it's positioned to the beginning of the stream.
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    // Write the name and hash value of the file to the console.
                    /*    Console.Write($"{fi1.Name}: ");*/
                    string StringByte = BitConverter.ToString(hashValue);
                    //  string val = System.Text.Encoding.Default.GetString(hashValue);
                    var line = fi1.FullName + ">>>" + StringByte;
                    curNum++;
                    using (StreamWriter sw = File.AppendText(@"debug/checksum.txt"))
                    {
                        sw.WriteLine(line);

                    }


                    fileStream.Close();
                }


            }


            void DirSearch(string sDir, string[] filesNum)
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
             


                startupPath = Environment.CurrentDirectory;
                using (SHA256 mySHA256 = SHA256.Create())
                {
               
                    try
                    {
                        foreach (string d in Directory.GetDirectories(sDir))
                        {
                           if(d == Directory.GetCurrentDirectory()) continue;
                            string dirx = new DirectoryInfo(d).Name;
                           if (dirx == "Updater") continue;
                            curNum++;
                            foreach (string f in Directory.GetFiles(d))
                            {
                                Console.Write("\r  " + curNum + " /~ " + filesNum.Length);
                                var fi1 = new FileInfo(f);
                                // Create a fileStream for the file.
                                FileStream fileStream = fi1.Open(FileMode.Open);
                                // Be sure it's positioned to the beginning of the stream.
                                fileStream.Position = 0;
                                // Compute the hash of the fileStream.
                                byte[] hashValue = mySHA256.ComputeHash(fileStream);
                                // Write the name and hash value of the file to the console.
                                /*    Console.Write($"{fi1.Name}: ");*/
                                string StringByte = BitConverter.ToString(hashValue);
                                //  string val = System.Text.Encoding.Default.GetString(hashValue);
                                var line = fi1.FullName + ">>>" + StringByte;
                                curNum++;
                                using (StreamWriter sw = File.AppendText(@"debug/checksum.txt"))
                                {
                                    sw.WriteLine(line);

                                }

                     
                                fileStream.Close();
                          
                   
                            }
                            DirSearch(d, filesNum);
                        }
                    }
                    catch (System.Exception excpt)
                    {
                        Console.WriteLine(excpt.Message);
                    }
                }
            }
            Console.WriteLine(" ");
            Console.WriteLine("Done \n");
            Console.ReadLine();
        }
    }

    // Display the byte array in a readable format.
    public static void PrintByteArray(byte[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{array[i]:X2}");
            if ((i % 4) == 3) Console.Write(" ");
        }
        Console.WriteLine();
    }

    public static string MakeRelative(string filePath, string referencePath)
    {
        var fileUri = new Uri(filePath);
        var referenceUri = new Uri(referencePath);
        return referenceUri.MakeRelativeUri(fileUri).ToString();
    }

}