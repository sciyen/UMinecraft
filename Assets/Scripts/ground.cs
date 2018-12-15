using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {

    public GameObject dirt;
    static Vector3 mapSize = new Vector3(50, 10, 50);
    Vector3 mapOrigin = -1 * mapSize / 2;
    int mountain = 5;
    float sitex, sitez;
    float sizex, sizez;
    int uplimit;

    // Use this for initialization
    void Start () {
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
        Debug.Log("123");
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
        Debug.Log(uplimit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
