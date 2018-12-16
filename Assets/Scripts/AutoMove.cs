using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collision))]
public class AutoMove : MonoBehaviour {
    Vector3 target;
    bool isJumping = false;
    float lastJumpTime;
    bool task = false;
    Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        lastJumpTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (task) {
            Vector3 dir = target - transform.position;
            Vector3 unitDir = dir.normalized;
            Vector3 fp = transform.position + unitDir - Const.mapOrigin;
            if (Ground.map[(int)fp.x, (int)fp.y - 1, (int)fp.x] != Const.GameItemID.Empty)
                jump();
            transform.position += unitDir * Time.deltaTime * 0.5f;
            if (Vector3.Distance(transform.position, target) < 1)
                task = false;
        }
    }
    public void setTarget(Vector3 p) {
        target = p;
        task = true;
    }
    void jump()
    {
        if (!isJumping && Time.time - lastJumpTime > 0.5) {
            isJumping = true;
            lastJumpTime = Time.time;
            rb.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (Vector3.Angle(other.contacts[0].normal, Vector3.up) < 10)
            isJumping = false;
    }
}
