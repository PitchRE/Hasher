using System;

public class hash
{
	public hash()
	{
         static IEnumerable<(string fileName, string hash)> GetHasList(string path, bool isRelative)
        {
            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string hash;
                using (var md5 = MD5.Create())
                using (var stream = File.OpenRead(file))
                    hash = HttpServerUtility.UrlTokenEncode(md5.ComputeHash(stream));
                if (isRelative)
                    yield return (file.Remove(0, path.TrimEnd('/').Length + 1), hash);
                else
                    yield return (file, hash);
            }
        }
    }
}
