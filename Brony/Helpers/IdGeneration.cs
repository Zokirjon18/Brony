using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brony.Helpers;

public class IdGeneration
{
    public static int IdGenerate(string path)
    {
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);

            string[] lines = text.Split('\n');

            int maxId = 0;
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');
                string id = parts[0];

                if (maxId < Convert.ToInt32(id))
                {
                    maxId = Convert.ToInt32(id);
                }
            }

            return ++maxId;
        }
        else
            return 1;
    }

}
