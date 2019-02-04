using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoilData : MonoBehaviour, DataInterface
{
    [SerializeField]
    public Color color = Color.yellow;

    [SerializeField]
    public int currentFrame;

    public Vector3[,] Positions { get; set; }

    public Vector3[,] Forwards { get; set; }

    public Vector3[,] Rights { get; set; }

    public float[] Heights { get; set; }

    public float[] Radius { get; set; }

    public int NumofCoils { get; set; }

    public int NumofTimes { get; set; }

    public int CurrentFrame { get { return currentFrame; } set { currentFrame = value; } }

    private Material material;

    public GameObject CreateOriginalGameObject()
    {
        var original = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        return original;
    }

    public void MigrateNull()
    {
        if (Positions != null) return;

        NumofTimes = 5;
        NumofCoils = 2;

        Positions = new Vector3[NumofTimes, NumofCoils];
        Forwards = new Vector3[NumofTimes, NumofCoils];
        Rights = new Vector3[NumofTimes, NumofCoils];
        Heights = new float[NumofCoils];
        Radius = new float[NumofCoils];

        for (int i = 0; i < NumofTimes; ++i)
        {
            Positions[i, 0] = new Vector3(0f, 0f, 0.003f);
            Positions[i, 1] = new Vector3(0f, 0f, -0.003f);

            for (int j = 0; j < NumofCoils; ++j)
            {
                Forwards[i, j] = new Vector3(0f, 0f, 1f);
                Rights[i, j] = new Vector3(1f, 0f, 0f);
            }
        }

        for (int i = 0; i < NumofCoils; ++i)
        {
            Heights[i] = 1f;
            Radius[i] = 1f;
        }
    }

    private void Start()
    {
        MigrateNull();

        material = new Material(Shader.Find("Unlit/ResultCubeShader"));
        material.color = color;

        var original = CreateOriginalGameObject();

        for (int i = 0; i < NumofCoils; ++i)
        {
            // コイルはZ軸を磁極として見ている
            var upwards = Vector3.Cross(Forwards[CurrentFrame, i], Rights[CurrentFrame, i]);
            var rotation = Quaternion.LookRotation(Rights[CurrentFrame, i], upwards);
            Instantiate(original, Positions[CurrentFrame, i], rotation, transform);
        }

        Destroy(original);
    }
}
