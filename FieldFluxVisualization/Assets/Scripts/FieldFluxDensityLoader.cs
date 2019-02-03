using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FieldFluxDensityLoader : CSVLoader<FieldFluxDensityData>
{
    void LoadCSV(StreamReader reader, int x, int y, int z)
    {
        float max = 0.0f;
        float min = 0.0f;
        float val = 0.0f;

        for (int i = 0; i < x; ++i)
        {
            for (int j = 0; j < y; ++j)
            {
                for (int k = 0; k < z; ++k)
                {
                    val = float.Parse(reader.ReadLine());
                    Map.FluxDensity[x, y, z] = val;

                    if (max < val) max = val;
                    if (min > val) min = val;
                }
            }
        }

        float div = (max - min) / max;
        for (int i = 0; i < x; ++i)
        {
            for (int j = 0; j < y; ++j)
            {
                for (int k = 0; k < z; ++k)
                {
                    Map.FluxDensity[x, y, z] = (Map.FluxDensity[x, y, z] - min) * div;
                }
            }
        }
    }

    public FieldFluxDensityLoader(string path)
        : base(path)
    {
        using (var reader = new StreamReader(path))
        {
            var x = int.Parse(reader.ReadLine());
            var y = int.Parse(reader.ReadLine());
            var z = int.Parse(reader.ReadLine());
            Map.NumofField = new Vector3Int(x, y, z);

            LoadCSV(reader, x, y, z);   
        }
    }
}
