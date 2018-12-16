using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public GameObject a;
    Material[] m1 = new Material[2];
    Material[] m2 = new Material[10];
    int i = 0;
	// Use this for initialization
	void Start () {
        m1[1] = (Material)Resources.Load("dirt");
        m2[0] = (Material)Resources.Load("destroy/Materials/destroy_stage_0");
        m2[1] = (Material)Resources.Load("destroy/Materials/destroy_stage_1");
        m2[2] = (Material)Resources.Load("destroy/Materials/destroy_stage_2");
        m2[3] = (Material)Resources.Load("destroy/Materials/destroy_stage_3");
        m2[4] = (Material)Resources.Load("destroy/Materials/destroy_stage_4");
        m2[5] = (Material)Resources.Load("destroy/Materials/destroy_stage_5");
        m2[6] = (Material)Resources.Load("destroy/Materials/destroy_stage_6");
        m2[7] = (Material)Resources.Load("destroy/Materials/destroy_stage_7");
        m2[8] = (Material)Resources.Load("destroy/Materials/destroy_stage_8");
        m2[9] = (Material)Resources.Load("destroy/Materials/destroy_stage_9");
    }
	
	// Update is called once per frame
	void Update () {
        m1[0] = m2[i/10];
        i++;
        if (i / 10 > 9) i = 0;
        Debug.Log(i);
        a.GetComponent<Renderer>().materials = m1;
    }
}
