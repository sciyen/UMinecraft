using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {
    public GameObject dirt;
    static Vector3Int mapSize = new Vector3Int(50, 50, 50);
    public Vector3Int mapOrigin = new Vector3Int(0, 0, 0);
    public Vector3Int terrainMaxSize = new Vector3Int(30, 10, 30);
    public Const.GameItemID[,,] map = new Const.GameItemID[mapSize.x, mapSize.y, mapSize.z];

    int groundLevel = 5;
    int maxMountainHeight = 5;
    int numOfMountain = 5;

    float sitex, sitez;
    float sizex, sizez;
    int uplimit;

    // Use this for initialization
    void Start () {
        // Instantiate ground base
        for(int x=0;x<mapSize.x; x++)
            for (int z = 0; z < mapSize.z; z++)
                for (int y = 0; y < mapSize.y; y++) {
                    if (y < groundLevel) map[x, y, z] = getRandomGround(y);
                    else map[x, y, z] = Const.GameItemID.Empty;
                }
        // Generate Terrain
        for(int n = 0; n < numOfMountain; n++) {
            Vector3 size = getRandomVector(terrainMaxSize);
            Vector3 ori = getRandomVector(new Vector3(mapSize.x, groundLevel, mapSize.z));
            CosTerrainGenerator terrain = new CosTerrainGenerator(ori, size);
            for (int x = 0; x < mapSize.x; x++) 
                for (int z = 0; z < mapSize.z; z++) {
                    if(terrain.isCovered(new Vector3(x, 0, z))) {
                        int h = Mathf.CeilToInt(terrain.calFunction(new Vector3(x, 0, z)));
                        Debug.Log("height=" + h);
                        for (int y = h - 1; y >= 0 && (map[x, y, z] != Const.GameItemID.Empty); y--) {
                            map[x, y, z] = getRandomGround(y);
                            //Debug.Log("Update"+ new Vector3(x, y, z));
                        }
                    }
                }
        }
        // Instantiate
        for (int x = 0; x < mapSize.x; x++)
            for (int z = 0; z < mapSize.z; z++)
                for (int y = 0; y < mapSize.y; y++)
                    if (map[x, y, z] != Const.GameItemID.Empty)
                        instantiateItem(map[x, y, z], mapOrigin + new Vector3(x, y, z));
        /*
        for (float x = mapOrigin.x; x < mapSize.x; x++)
        {
            for (float z = mapOrigin.z; z < mapSize.z; z++)
            {
                GameObject g = Instantiate(dirt);
                g.transform.parent = transform;
                g.transform.position = new Vector3(x, 0, z);
            }
        }
        uplimit = 240 * 240;
        for (int i = 0; i < mountain; i++)
        {
            sitex = Random.Range(mapOrigin.x, mapOrigin.x + mapSize.x);
            sitez = Random.Range(mapOrigin.z, mapOrigin.z + mapSize.z);
            sizex = Random.Range(5, 20);
            sizez = Random.Range(5, 20);
            float height = Random.Range(5, mapSize.y);
            for (float y = 1; y < height; y++)
            {
                for (float x = sitex -sizex; x < sizex + sitex; x++)
                {
                    for (float z = sitez - sizez; z < sizez + sitez; z++)
                    {
                        GameObject g = Instantiate(dirt);
                        g.transform.parent = transform;
                        g.transform.position = new Vector3(x, y, z);
                        g.name = x + "," + y + "," + z;
                    }
                }
                sizex -= 1;
                sizez -= 1;
            }
        }
        Debug.Log(this.gameObject.transform.GetChild(1).name);
        Debug.Log(uplimit);*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    Const.GameItemID getRandomGround(int level)
    {
        return Const.GameItemID.Dirt;
    }
    Vector3 getRandomVector(Vector3 p)
    {
        return new Vector3(
            Random.Range(0, p.x),
            Random.Range(0, p.y),
            Random.Range(0, p.z));
    }
    public void instantiateItem(Const.GameItemID id, Vector3 position)
    {
        GameObject g = Instantiate(dirt);
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
        if (Mathf.Abs(p.x - origin.x) > size.x) return false;
        if (Mathf.Abs(p.z - origin.z) > size.z) return false;
        return true;
        //if (calFunction(p) < p.y) return true;
        //else return false;
    }
}
public class CosTerrainGenerator:TerrainGenerator
{
    public CosTerrainGenerator(Vector3 newOrigin, Vector3 newSize):base(newOrigin, newSize)
    {
        alpha1 = Random.Range(size.y * 0.3f, size.y * 0.7f);
        alpha2 = size.y - alpha1;
    }
    public override float calFunction(Vector3 p) {
        p = p - origin; // Move to origin
        return alpha1 * (1 + Mathf.Cos(p.x * Mathf.PI / size.x))
             + alpha2 * (1 + Mathf.Cos(p.z * Mathf.PI / size.z));
    }
}