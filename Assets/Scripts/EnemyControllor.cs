using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllor : MonoBehaviour {
    public Transform mainActor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    float calDistanceWithMainActor()
    {
        return Vector3.Distance(mainActor.transform.position, transform.position);
    }
}
