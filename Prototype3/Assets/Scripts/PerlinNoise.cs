using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //    public static float[,] GenerateNoise(int SizeOfNoise, float s,Wave[] w,Vector2 offset,int fixResolution = 1)
    //    {
    //        float[,] noiseSpace = new float[SizeOfNoise*fixResolution, SizeOfNoise*fixResolution];

    //        for(int row = 0; row < SizeOfNoise*fixResolution; row++)
    //        {
    //            for (int col = 0; col < SizeOfNoise * fixResolution; col++)
    //            {
    //                float xPosition = ((float)row / s / (float)fixResolution)+offset.y;
    //                float yPosition = ((float)col / s / (float)fixResolution)+offset.x;
    //                // noiseSpace[row, col] = Mathf.PerlinNoise(xPosition, yPosition); 
    //                float n = 0.0f;
    //                float normal = 0.0f;
    //                foreach(Wave i in w)
    //                {
    //                    n += i.amplitude * Mathf.PerlinNoise(xPosition * i.frequency +i.seed, yPosition * i.frequency + i.seed);
    //                    normal += i.amplitude;
    //                }
    //                n /= normal;

    //                noiseSpace[row, col] = n;
    //            }
    //        }
    //        return noiseSpace;

    //    }

    //    public static float[,] GenerateUniform(int size, float vertexOffset,float maxDistance)
    //    {
    //        float[,] newMap = new float[size, size];
    //        for(int i = 0;i < size;i++)
    //        {
    //            float samplesX = i + vertexOffset;
    //            float value = Mathf.Abs(samplesX) / maxDistance;
    //            for(int j= 0; j< size;j++)
    //            {
    //                newMap[i, size - j - 1] = value;
    //            }
    //        }
    //        return newMap;
    //    }

    //}

    //[System.Serializable]
    //public class Wave
    //{
    //    public float seed;
    //    public float frequency;
    //    public float amplitude;
    //}

}
[System.Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}