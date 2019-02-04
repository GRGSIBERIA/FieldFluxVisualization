using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CSVLoader<MAPPER>
    where MAPPER : DataMapper, new()
{
    public string Path { get; private set; }

    public MAPPER Map { get; private set; }

    public CSVLoader(string path)
    {
        Path = path;
        Map = new MAPPER();
    }
}
