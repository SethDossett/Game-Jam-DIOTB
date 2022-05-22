using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
public GameObject[] prefabs;
public Terrain terrain;
public float yOffset = 0.5f;

private float terrainWidth;
private float terrainLength;

private float xTerrainPos;
private float zTerrainPos;

public int prefabCount = 20;

void Start()
{
    //Get terrain size
    terrainWidth = terrain.terrainData.size.x;
    terrainLength = terrain.terrainData.size.z;

    //Get terrain position
    xTerrainPos = terrain.transform.position.x;
    zTerrainPos = terrain.transform.position.z;

    for (int i = 0; i < prefabCount; i++)
    {
        generateObjectOnTerrain();
    }
}

void generateObjectOnTerrain()
{
    //Generate random x,z,y position on the terrain
    float randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
    float randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
    float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));

    //Apply Offset if needed
    yVal = yVal + yOffset;

    //Generate the Prefab on the generated position
    GameObject objInstance = (GameObject)Instantiate(prefabs[Random.Range(0,prefabs.Length)], new Vector3(randX, yVal, randZ), Quaternion.identity);
}
}
