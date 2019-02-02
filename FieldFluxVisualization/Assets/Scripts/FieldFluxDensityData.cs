using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFluxDensityData : DataMapper
{
    [SerializeField]
    public Vector3Int NumofField { get; set; }

    [SerializeField]
    public float[,,] FluxDensity { get; set; }

    public FieldFluxDensityData()
    {

    }
}
