using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    
    private float depth = 20;

    private int width = 256;
    private int length = 256;
    private float scale = 20;
    private Terrain terrain;
    private float offsetX;
    private float offsetY;

    public bool[,] WaterMap = new bool[257, 257];
    //private ContainInitialVariables container;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        //container = GameObject.Find("Value Container").GetComponent<ContainInitialVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        //newTerrain();
    }

    private void randomise()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }
    public void newTerrain(float waterHeight)
    {
        randomise();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        WaterMap = GenerateWaterMap(WaterMap, waterHeight);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, length);
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width + 1, length + 1];
        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= length; z++)
            {
                heights[x, z] = CalculateHeight(x, z);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / length * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
    
    public void setTerrainVariables(float hillDensity, float hillHeight, int groundTexture)
    {
        scale = hillDensity;
        depth = hillHeight;

        updateAlpha(groundTexture);

    }
    private void updateAlpha(int groundTexture)
    {
        float[,,] alphas = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
        for (int i = 0; i < terrain.terrainData.alphamapWidth; i++)
        {
            for (int j = 0; j < terrain.terrainData.alphamapHeight; j++)
            {
                for (int p = 0; p < 5; p++)
                {
                    alphas[i, j, p] = 0;
                }
                alphas[i, j, groundTexture] = 1;
            }
        }
        terrain.terrainData.SetAlphamaps(0, 0, alphas);
    }

    private bool[,] GenerateWaterMap(bool[,] waterMap, float waterHeight)
    {
        Vector3 temp = new Vector3(0, 0, 0);
        for (int x = 0; x <= 256; x++)
        {
            for (int y = 0; y <= 256; y++)
            {
                temp.x = x;
                temp.z = y;

                if (terrain.SampleHeight(temp) >= waterHeight)
                {
                    waterMap[x, y] = false; // no water at this point
                    //Debug.DrawRay(temp, new Vector3(0, 20, 0), Color.red, 100);
                }
                else
                {
                    waterMap[x, y] = true; // water is at this point
                    //Debug.DrawRay(temp, new Vector3(0, 20, 0), Color.blue, 100);
                }
            }
        }
        
        /*
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                Debug.Log(waterMap[i, j]);
            }
        }
        */



        return waterMap;
    }
}
