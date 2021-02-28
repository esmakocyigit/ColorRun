using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSomeoneNoices : MonoBehaviour
{
    public float power = 3;
    public float scale = 1;
    public float timeScale = 1;


    private float xOffset;
    private float yOffset;
    private MeshFilter mf;

    void Start()
    {
        mf = GetComponent<MeshFilter>();
        MakeNoise();
    }

    void Update()
    {
        MakeNoise();
        xOffset += Time.deltaTime * timeScale;
        xOffset += Time.deltaTime * timeScale;
    }

    void MakeNoise()
    {
        Vector3[] verticles = mf.mesh.vertices;

        for (int i = 0; i<verticles.Length;i++)
        {
            verticles[i].y = CalculateHeight(verticles[i].x, verticles[i].z) * power;
        }

        mf.mesh.vertices = verticles;

    }

    float CalculateHeight(float x, float y)
    {
        float xCord = x * scale + xOffset;
        float yCord = y * scale + yOffset;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
