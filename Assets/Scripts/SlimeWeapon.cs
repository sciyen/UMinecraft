using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision))]
public class SlimeWeapon : MonoBehaviour
{
    public bool triggered = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (triggered) {
            transform.GetComponent<Renderer>().material.color = Color.red;
            Ground.destroyItem(other.transform.position);
            Destroy(other.transform.gameObject);
        }
    }
}
