using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public TileSpawner prefabForTiles;
    public int xNumber = 2;
    public int yNumber = 2;
    void Start()
    {
        TilesGenerator();
    }
    void TilesGenerator()
    {
        float size = prefabForTiles.GetComponent<NewMeshGenerator>().x;
        for (int i = 0; i < xNumber; i++)
        {
            for (int j = 0; j < yNumber; j++)
            {
                GameObject o = Instantiate(prefabForTiles.gameObject, transform);
                o.transform.position = new Vector3((i - ((float)xNumber / 2)) * size, 0, (j - ((float)yNumber / 2)) * size);
                float rate = (prefabForTiles.NoiseSize - 1) / prefabForTiles.scale;
                o.GetComponent<TileSpawner>().offset = new Vector2(i * rate, j * rate);
            }
        }
    }
}
