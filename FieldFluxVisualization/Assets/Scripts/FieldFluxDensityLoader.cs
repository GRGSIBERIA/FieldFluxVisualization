using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FieldFluxDensityLoader : CSVLoader<FieldFluxDensityData>
{
    public FieldFluxDensityLoader(string path)
        : base(path)
    {
        using (var reader = new StreamReader(path))
        {
            var x = int.Parse(reader.ReadLine());
            var y = int.Parse(reader.ReadLine());
            var z = int.Parse(reader.ReadLine());
            Map.NumofField = new Vector3Int(x, y, z);

            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    for (int k = 0; k < z; ++k)
                    {
                        Map.FluxDensity[x, y, z] = float.Parse(reader.ReadLine());
                    }
                }
            }
        }
    }
}
