using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour {

    public GameObject dirt;
    int mountain = 5;
    int sitex, sitez;
    int height = 10;
    int sizex, sizez;
    int uplimit;

    // Use this for initialization
    void Start () {
        int x = 0, z = 0, y = 0;
        for (x = -120; x < 120; x++)
        {
            for (z = -120; z < 120; z++)
            {
                GameObject g = Instantiate(dirt);
                g.transform.parent = transform;
                g.transform.position = new Vector3(x, 0, z);
            }
        }
        uplimit = 240 * 240;
        Debug.Log("123");
        for (int i = 0; i < 30; i++)
        {
            sitex = Random.Range(-100, 100); sitez = Random.Range(-100, 100);
            sizex = Random.Range(5, 20); sizez = Random.Range(5, 20);
            height = Random.Range(5, sizex);
            for (y = 1; y < height; y++)
            {
                x = sitex;
                for (x -= sizex; x < sizex + sitex; x++)
                {
                    z = sitez;
                    for (z -= sizez; z < sizez + sitez; z++)
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
