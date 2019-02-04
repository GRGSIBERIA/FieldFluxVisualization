using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireData : MonoBehaviour
{
    [SerializeField]
    public Vector3[,] Wires { get; set; }

    [SerializeField]
    public int NumofWires { get; set; }

    [SerializeField]
    public int NumofTimes { get; set; }

    [SerializeField]
    public int CurrentFrame { get; set; }

    [SerializeField]
    public float crossSize = 0.001f;

    [SerializeField]
    public Color color = Color.white;

    public float CrossSize { get { return crossSize; } set { crossSize = value; } }

    private Material material;

    private GameObject[] wireObjects;

    private void MigrateNull()
    {
        if (Wires != null) return;

        NumofWires = 200;
        NumofTimes = 5;

        Wires = new Vector3[NumofTimes, NumofWires];

        for (int i = 0; i < NumofWires; ++i)
        {
            for (int j = 0; j < NumofTimes; ++j)
            {
                Wires[j, i] = new Vector3(0.005f * i, 0, 0.05f);
            }
        }
        
        CurrentFrame = 0;
    }

    GameObject CreateOriginalGameObject()
    {
        var original = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        original.transform.localScale = new Vector3(CrossSize, CrossSize, CrossSize);

        var renderer = original.GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Unlit/ResultCubeShader"));
        renderer.material.color = color;

        Destroy(original.GetComponent<Collider>());

        return original;
    }

    private void Start()
    {
        MigrateNull();

        material = new Material(Shader.Find("Unlit/ResultCubeShader"));
        material.enableInstancing = true;

        var original = CreateOriginalGameObject();
        wireObjects = new GameObject[NumofWires];

        for (int i = 0; i < NumofWires; ++i)
        {
            Instantiate(original, Wires[CurrentFrame, i], Quaternion.identity, transform);
        }

        Destroy(original);
    }

    private void Update()
    {
        material.color = color;
    }
}
