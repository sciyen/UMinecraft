using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Ground : MonoBehaviour {
    public GameObject dirt;
    public static Vector3 mapSize = Const.mapSize;//new Vector3Int(Const.mapSize.x, mapSize.y, mapSize.z);
    public static Vector3 mapOrigin = Const.mapOrigin;
    public Vector3Int terrainMaxSize = new Vector3Int(50, 15, 50);
    public Vector3Int terrainMinSize = new Vector3Int(5, 5, 5);
    public static Const.GameItemID[,,] map = new Const.GameItemID[(int)mapSize.x, (int)mapSize.y, (int)mapSize.z];
    public static bool mapReady = false;

    int groundLevel = 3;
    int numOfMountain = 10;

    // Use this for initialization
    void Start()
    {
        // Instantiate ground base
        for (int x = 0; x < mapSize.x; x++)
            for (int z = 0; z < mapSize.z; z++)
                for (int y = 0; y < mapSize.y; y++) {
                    if (y < groundLevel) map[x, y, z] = Const.GameItemID.Dirt;
                    else map[x, y, z] = Const.GameItemID.Empty;
                }
        // Generate Terrain
        for (int n = 0; n < numOfMountain; n++) {
            Vector3 size = getRandomVector(terrainMaxSize, terrainMinSize);
            Vector3 ori = getRandomVector(new Vector3(mapSize.x, groundLevel, mapSize.z));
            CosTerrainGenerator terrain = new CosTerrainGenerator(ori, size);
            for (int x = 0; x < mapSize.x; x++)
                for (int z = 0; z < mapSize.z; z++) {
                    if (terrain.isCovered(new Vector3(x, 0, z))) {
                        int h = Mathf.CeilToInt(terrain.calFunction(new Vector3(x, 0, z)));
                        //Debug.Log("height=" + h);
                        for (int y = h - 1; y >= 0 && (map[x, y, z] == Const.GameItemID.Empty); y--) {
                            map[x, y, z] = Const.GameItemID.Dirt;
                        }
                    }
                }
        }
        // Stone Generator
        for (int x = 0; x < mapSize.x; x++)
            for (int z = 0; z < mapSize.z; z++) {
                int h = getDistanceToGround(new Vector3(x, 0, z));
                map[x, h, z] = Const.GameItemID.DirtGrass;
                for (int y = 0; y < h; y++)
                    map[x, y, z] = getRandomGround(new Vector3(x, y, z), h-y-1);
            }
        // Instantiate
        for (int x = 0; x < mapSize.x; x++)
            for (int z = 0; z < mapSize.z; z++)
                for (int y = 0; y < mapSize.y; y++)
                    if (map[x, y, z] != Const.GameItemID.Empty)
                        instantiateItem(map[x, y, z], mapOrigin + new Vector3(x, y, z));
        mapReady = true;
    }
	void Update () {
		
	}
    Const.GameItemID getRandomGround(Vector3 p, float dis=0)
    {
        float r = 1 - Mathf.Exp(-1*dis / 5); 
        r += Random.Range(-0f, 0.3f);
        if (r < 0.5) return Const.GameItemID.Dirt;
        return Const.GameItemID.Stone;
    }
    public static int getDistanceToGround(Vector3 p)
    {
        int i = (int)p.y;
        while (i < mapSize.y && map[(int)p.x, i, (int)p.z] != Const.GameItemID.Empty) i++;
        return (i - (int)p.y)>0 ? (i - (int)p.y) : 0;
    }
    public static Vector3 getPointOnGround(Vector3 p)
    {
        int height = getDistanceToGround(new Vector3(p.x, 0, p.z));
        return Const.mapOrigin + new Vector3(p.x, height + 1, p.z);
    }
    public static Vector3 getRandomVector(Vector3 p)
    {
        return new Vector3(
            Random.Range(0, p.x),
            Random.Range(0, p.y),
            Random.Range(0, p.z));
    }
    public static Vector3 getRandomVector(Vector3 max, Vector3 min)
    {
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z));
    }
    public void instantiateItem(Const.GameItemID id, Vector3 position)
    {
        Material m1 = (Material)Resources.Load(ItemMap.getTextureName(id));
        GameObject g = Instantiate(dirt);
        g.GetComponent<Renderer>().material = m1;
        g.transform.parent = transform;
        g.transform.position = position;
        g.name = id.ToString();
    }
}
public abstract class TerrainGenerator
{
    protected float alpha1, alpha2;
    protected Vector3 origin;
    protected Vector3 size;
    public TerrainGenerator(Vector3 newOrigin, Vector3 newSize)
    {
        origin = newOrigin;
        size = newSize;
    }
    public abstract float calFunction(Vector3 p);
    public bool isCovered(Vector3 p)
    {
        if (Mathf.Abs(p.x - origin.x) <= size.x * alpha1
         && Mathf.Abs(p.z - origin.z) <= size.z * alpha2) return true;
        return false;
        //if (calFunction(p) < p.y) return true;
        //else return false;
    }
}
public class CosTerrainGenerator:TerrainGenerator
{
    public CosTerrainGenerator(Vector3 newOrigin, Vector3 newSize):base(newOrigin, newSize)
    {
        alpha1 = Random.Range(size.y * 0.4f, size.y * 0.6f);
        alpha2 = size.y - alpha1;
    }
    public override float calFunction(Vector3 p) {
        p = p - origin; // Move to origin
        return alpha1 * (1 + Mathf.Cos(p.x * Mathf.PI / size.x))
             + alpha2 * (1 + Mathf.Cos(p.z * Mathf.PI / size.z));
    }
}