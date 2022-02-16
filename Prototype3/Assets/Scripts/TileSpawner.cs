using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference https://www.youtube.com/watch?v=64NblGkAabk
//https://www.red-gate.com/simple-talk/development/dotnet-development/procedural-generation-unity-c/

public class TileSpawner : MonoBehaviour
{
    private static bool isfirsttime = true;
    public static int seed1;
    public static int seed2;
    public static int seed3;

    public static TileSpawner instance;

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
    private Types[] heightTerrain;

    public Types waterTerrain;
    public Types grassTerrain;
    public Types dirtTerrain;
    public Types snowTerrain;

    private MeshCollider mapMC;
    private MeshRenderer mapMR;
    private MeshFilter mapMF;
    private heightTypes[,] mapData;


    void Start()
    {
        if(instance = null)
        {
            instance = this;
        }
        if(isfirsttime)
        {
            isfirsttime = false;
        }
        else
        {
            waves[0].seed = seed1;
            waves[1].seed = seed2;
            waves[2].seed = seed3;
        }

        heightTerrain = new Types[4];
        heightTerrain[0] = waterTerrain;
        heightTerrain[1] = grassTerrain;
        heightTerrain[2] = dirtTerrain;
        heightTerrain[3] = snowTerrain;

        mapMR = GetComponent<MeshRenderer>();
        mapMF = GetComponent<MeshFilter>();
        mapMC = GetComponent<MeshCollider>();
        generateTiles();
    }

    void generateTiles()
    {
        float[,] heightMap = PerlinNoise.GenerateNoise(NoiseSize, waves, scale, offset);
        float[,] fixMaps = PerlinNoise.GenerateNoise(NoiseSize-1, waves, scale, offset, newResolution);
        Texture2D heightMapTexture = TextureController.Builder(fixMaps, heightTerrain);

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

        Types[,] heightMapType = TextureController.generateTerrainMap(heightMap, heightTerrain);
        mapMF.mesh.vertices = newVertice;
        mapMF.mesh.RecalculateBounds();
        mapMF.mesh.RecalculateNormals();

        mapMC.sharedMesh = mapMF.mesh;

        generateDataMap(heightMapType);
        ObjectGenerator.instance.spawnObject(mapData);
    }
   
    void generateDataMap(Types[,] heightTerrainMap)
    {
        mapData = new heightTypes[NoiseSize, NoiseSize];
        Vector3[] vertex = mapMF.mesh.vertices;

        for (int i = 0; i < NoiseSize; i++)
        {
            for (int j = 0; j < NoiseSize; j++)
            {
                heightTypes dataSet = new heightTypes();
                dataSet.pos = transform.position + vertex[(i * NoiseSize) + j];
                dataSet.typeObj = heightTerrainMap[i, j];
                mapData[i, j] = dataSet;
            }
        }
    }
}

[System.Serializable]
public class Types
{

    [Range(0.0f, 1.0f)]
    public float threshold;
    public Gradient color;

    public GameObject[] objects;
    [Range(0.0f, 3.0f)]
    public float density = 1.0f;

    public Types(float newThresh, Gradient newColor, float  newDensity)
    {
        threshold = newThresh;
        color = newColor;
        density = newDensity;
    }
}

public class heightTypes
{
    
    public Vector3 pos;
    public Types typeObj;
}