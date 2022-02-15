using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public int NoiseSize;
    public float scale;
    public int newResolution = 1;
    public float maxHeight = 1.0f;

    [HideInInspector]
    public Vector2 offset;

    [Header("Curves")]
    public AnimationCurve heightCurve;


    [Header("Waves")]
    public Wave[] waves;


    [Header("Terrain Types")]
    public Types[] heightTerrain;

    private MeshCollider mapMC;
    private MeshRenderer mapMR;
    private MeshFilter mapMF;
    private heightTypes[,] mapData;
    void Start()
    {
        mapMR = GetComponent<MeshRenderer>();
        mapMF = GetComponent<MeshFilter>();
        mapMC = GetComponent<MeshCollider>();
        generateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void generateTiles()
    {
        float[,] heightMap = PerlinNoise.GenerateNoise(NoiseSize, waves, scale, offset);
        float[,] fixMaps = PerlinNoise.GenerateNoise(NoiseSize-1, waves, scale, offset, newResolution);
        Texture2D heightMapTexture = TextureController.Builder(fixMaps, heightTerrain);
        //Texture2D fixheightMapTexture = 
        mapMR.material.mainTexture = heightMapTexture;

        Vector3[] newVertice = mapMF.mesh.vertices;

        for (int i = 0; i < NoiseSize; i++)
        {
            for (int j = 0; j < NoiseSize; j++)
            {
                int index = (i * NoiseSize) + j;
                newVertice[index].y = heightCurve.Evaluate(heightMap[i, j]) * maxHeight;
            }
        }
        mapMF.mesh.vertices = newVertice;
        mapMF.mesh.RecalculateBounds();
        mapMF.mesh.RecalculateNormals();

        mapMC.sharedMesh = mapMF.mesh;

        //generateDataMap();
        //ObjectGenerator.instance.spawnObject(mapData);
    }

    void generateDataMap()
    {
        mapData = new heightTypes[NoiseSize, NoiseSize];
        Vector3[] vertex = mapMF.mesh.vertices;

        for (int i = 0; i < NoiseSize; i++)
        {
            for (int j = 0; j < NoiseSize; j++)
            {
                heightTypes dataSet = new heightTypes();
                dataSet.pos = transform.position + vertex[(i * NoiseSize) + j];
                mapData[i, j] = dataSet;
            }
        }
    }
}


[System.Serializable]
public class Types
{
    //public int index;
    [Range(0.0f, 1.0f)]
    public float threshold;
    //public Gradient colorGrad;
    public Gradient color;

    public GameObject[] objects;
    [Range(0.0f, 3.0f)]
    public float density = 1.0f;
}

public class heightTypes
{
    
    public Vector2 pos;
    public Types typeObj;
}