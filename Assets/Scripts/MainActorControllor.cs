using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collision))]
public class MainActorControllor : MonoBehaviour {
    public float moveSpeed = 5;
    Rigidbody rb;
    bool is_jumping = false;
    float jumpForce = 10000;
    Vector3 mouseInitial;
    void Start () {
        rb = GetComponent<Rigidbody>();
        mouseInitial = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            transform.localPosition += moveSpeed * Time.deltaTime * transform.forward;
        }
        else if (Input.GetKey(KeyCode.S)) {
            transform.localPosition += -1 * moveSpeed * Time.deltaTime * transform.forward;
        }
        if (!is_jumping && Input.GetKey(KeyCode.Space)) {
            rb.AddForce(jumpForce * Time.deltaTime * Vector3.up);
            //transform.localPosition += jumpForce * Time.deltaTime * Vector3.up;
            is_jumping = true;
        }
        Vector3 dis = Input.mousePosition - mouseInitial;
        transform.localEulerAngles = new Vector3(0, dis.x, 0);
        Transform cam = transform.Find("Main Camera");
    }
    void OnCollisionEnter(Collision c)
    {
        is_jumping = false;
    }
}
