using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    [System.Serializable]
    struct Layer
    {
        public Sprite tile;
        public int min;
    }

    [System.Serializable]
    struct Prop
    {
        public GameObject propgo;
        public float threshold;
    }

    [SerializeField] GameObject Tile;

    [Header("Top layer tiles")]
    [SerializeField] Sprite GrassFlat;

    [Header("Gen Layers")]
    [SerializeField] Layer[] layers;

    [Header("Gen Parameters")]
    [SerializeField] int width;
    [SerializeField] int heigthMultiplier = 1;
    [SerializeField] int heightAddition;
    [SerializeField] int seed;
    [SerializeField] float smoothness = 1f;

    [Header("Props Parameters")]
    [SerializeField] Prop[] props;
    [SerializeField] float offset;

    public int[] heights;

    private void Start()
    {
        generateTerrain();

        generateProps(heights);
    }

    void generateTerrain()
    {
        heights = new int[width];

        for (int x = 0; x < width; x++)
        {
            int height = Mathf.CeilToInt(Mathf.PerlinNoise((float)seed, x / smoothness) * heigthMultiplier) + heightAddition;

            heights[x] = height;

            for (int y = 0; y < height; y++)
            {
                GameObject tile = Instantiate(Tile, new Vector3(x, y), Quaternion.identity);
            }
        }
    }

    void generateProps(int[] _heights) 
    {
        for (int x = 0; x < heights.Length; x++)
        {
            float perlin = Mathf.PerlinNoise(seed + offset, x);

            for (int i = 0; i < props.Length; i++)
            {
                if (perlin <= props[i].threshold)
                    Instantiate(props[i].propgo, new Vector3(x, heights[x]), Quaternion.identity);
            }

            Debug.Log(x + ": " + perlin);
        }
    }
}
