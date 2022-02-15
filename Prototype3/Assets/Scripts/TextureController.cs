using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureController 
{
    public static Texture2D Builder(float[,] map, Types[] typesForTerrain)
    {
        Texture2D t;
        Color[] pix = new Color[map.Length];
        int pixLen = map.GetLength(0);
        for (int i = 0; i < pixLen; i++)
        {
            for (int j = 0; j < pixLen; j++)
            {
                int index = (i * pixLen) + j;

                for (int k = 0; k < typesForTerrain.Length; k++)
                {
                    if (map[i, j] < typesForTerrain[k].threshold)
                    {
                        float minimumValue = k == 0 ? 0 : typesForTerrain[k - 1].threshold;
                        float maximumValue = typesForTerrain[k].threshold;
                        pix[index] = typesForTerrain[k].color.Evaluate(1.0f - (maximumValue - map[i, j]) / (maximumValue - minimumValue));
                        break;
                    }
                }
            }
        }

        t = new Texture2D(pixLen, pixLen);
        t.wrapMode = TextureWrapMode.Clamp;
        t.filterMode = FilterMode.Bilinear;
        t.SetPixels(pix);
        t.Apply();
        return t;

    }
}
