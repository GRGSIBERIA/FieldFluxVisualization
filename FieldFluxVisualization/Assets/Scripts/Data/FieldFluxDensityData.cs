using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFluxDensityData : MonoBehaviour
{
    [SerializeField]
    public Vector3 FieldSize { get; set; }

    [SerializeField]
    public Vector3Int NumofField { get; set; }

    [SerializeField]
    public float[,,] FluxDensity { get; set; }

    GameObject[,,] objects;

    MaterialPropertyBlock props;
    public Mesh cube;

    private GameObject CreateOriginalGameObject()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        var render = go.GetComponent<MeshRenderer>();
        render.material = new Material(Shader.Find("Unlit/ResultCubeShader"));
        render.material.enableInstancing = true;

        transform.localScale = Difference;
        go.transform.parent = transform;

        Destroy(go.GetComponent<BoxCollider>());
        return go;
    }

    Vector3 Difference { get { return new Vector3(FieldSize.x / NumofField.x, FieldSize.y / NumofField.y, FieldSize.z / NumofField.z); } }

    private void MigrateNull()
    {
        if (FieldSize == new Vector3())
            FieldSize = new Vector3(10, 10, 10);
        if (NumofField == new Vector3Int())
            NumofField = new Vector3Int(3, 3, 3);
        if (FluxDensity == null)
        {
            FluxDensity = new float[NumofField.x, NumofField.y, NumofField.z];
            var size = 1f / FluxDensity.Length;
            for (int x = 0; x < NumofField.x; ++x)
            {
                for (int y = 0; y < NumofField.y; ++y)
                {
                    for (int z = 0; z < NumofField.z; ++z)
                    {
                        FluxDensity[x, y, z] = (x * y * NumofField.x + y * NumofField.x + z) / size;
                    }
                }
            }
        }
    }

    private void Start()
    {
        MigrateNull();

        props = new MaterialPropertyBlock();
        objects = new GameObject[NumofField.x, NumofField.y, NumofField.z];
        var original = CreateOriginalGameObject();
        
        for (int x = 0; x < NumofField.x; ++x)
        {
            for (int y = 0; y < NumofField.y; ++y)
            {
                for (int z = 0; z < NumofField.z; ++z)
                {
                    var position = new Vector3(x, y, z);
                    var go = GameObject.Instantiate(original, position, Quaternion.identity, transform);
                    objects[x, y, z] = go;        // 必要かどうか検討したほうがいい

                    var color = Color.HSVToRGB(FluxDensity[x, y, z], 1, 1);
                    color.a = FluxDensity[x, y, z];
                    props.SetColor("_Color", color);    // GPU Instancing 側に色を設定

                    var render = go.GetComponent<MeshRenderer>();
                    render.SetPropertyBlock(props);     // レンダラに転送
                }
            }
        }
    }

    private void Update()
    {
        Debug.Log(objects[2, 0, 0].transform.position);
    }
}
