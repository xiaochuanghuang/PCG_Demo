using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference https://www.youtube.com/watch?v=64NblGkAabk
public class PerlinNoise
{
    public static float[,] GenerateNoise(int sizeNoise, Wave[] w, float scale, Vector2 offset, int resolution = 1)
    {

        float[,] noiseSpace = new float[sizeNoise*resolution, sizeNoise * resolution];

        for (int row = 0; row < sizeNoise * resolution; row++)
        {
            for (int col = 0; col < sizeNoise * resolution; col++)
            {
                float xPosition = ((float)row /  scale / (float)resolution) + offset.y ;
                float yPosition = ((float)col / scale/ (float)resolution) + offset.x ;
                float n = 0.0f;
                float normal = 0.0f;
                foreach (Wave i in w)
                {
                    n += i.amplitude * Mathf.PerlinNoise(xPosition * i.frequency + i.seed, yPosition * i.frequency + i.seed);
                    normal += i.amplitude;
                }
                n /= normal;

                noiseSpace[row, col] = n;
            }
        }
        return noiseSpace;

    }

   
}
[System.Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}