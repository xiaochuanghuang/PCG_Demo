using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public float offsetForObj;
    public static ObjectGenerator instance;

    private void Awake()
    {
        instance = this;
    }

    public void spawnObject(heightTypes[,] data)
    {

        int size = data.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
               
                    float density = data[i, j].typeObj.density;

                    if (density > 1.0f)
                    {
                        generateTree(data[i, j].pos, data[i, j].typeObj.objects);
                        float extraTree = Mathf.RoundToInt(Random.Range(1.0f, density));
                        for (int k = 0; k < extraTree; k++)
                        {
                            generateTree(data[i, j].pos, data[i, j].typeObj.objects);
                        }
                    }
                    else
                    {
                        if (Random.Range(0.0f, 1.0f) < density)
                        {
                            generateTree(data[i, j].pos, data[i, j].typeObj.objects);
                        }
                    }
                
            }
        }
    }

    void Start()
    {
        
    }

    void generateTree(Vector3 pos, GameObject[] prefabs)
    {
        Vector3 canSpawnPosition = new Vector3(pos.x + (Random.value * offsetForObj), pos.y, pos.z + (Random.value * offsetForObj));
        GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)], canSpawnPosition, Quaternion.identity);

        RaycastHit h;
        if (Physics.Raycast(new Ray(go.transform.position, Vector3.down), out h))
        {
            go.transform.position = h.point;
        }
    }
}
