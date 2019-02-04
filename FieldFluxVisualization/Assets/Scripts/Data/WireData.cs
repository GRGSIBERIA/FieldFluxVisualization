using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireData : MonoBehaviour, DataInterface
{
    [SerializeField]
    public int currentFrame;

    [SerializeField]
    public float crossSize = 0.001f;

    [SerializeField]
    public Color color = Color.white;

    public Vector3[,] Positions { get; set; }

    public int NumofWires { get; set; }

    public int NumofTimes { get; set; }

    public int CurrentFrame { get { return currentFrame; } set { currentFrame = value; } }

    public float CrossSize { get { return crossSize; } set { crossSize = value; } }

    private Material material;

    private GameObject[] wireObjects;

    private void MigrateNull()
    {
        if (Positions != null) return;

        NumofWires = 200;
        NumofTimes = 5;

        Positions = new Vector3[NumofTimes, NumofWires];

        for (int i = 0; i < NumofWires; ++i)
        {
            for (int j = 0; j < NumofTimes; ++j)
            {
                Positions[j, i] = new Vector3(0.005f * i, 0, 0.05f);
            }
        }
        
        CurrentFrame = 0;
    }

    public GameObject CreateOriginalGameObject()
    {
        var original = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        original.transform.localScale = new Vector3(CrossSize, CrossSize, CrossSize);

        var renderer = original.GetComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Unlit/ResultCubeShader"));
        renderer.material.color = color;

        Destroy(original.GetComponent<SphereCollider>());

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
            wireObjects[i] = Instantiate(original, Positions[CurrentFrame, i], Quaternion.identity, transform);
        }

        Destroy(original);
    }
}
