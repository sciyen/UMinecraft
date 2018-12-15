using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collision))]
public class MainActorControllor : MonoBehaviour {
    public ToolboxController toolbox;
    public float moveSpeed = 5;
    public float rotateSpeed = 0.1f;

    Rigidbody rb;
    bool is_jumping = false;
    float jumpForce = 10000;
    Vector3 mouseInitial;
    LiveCtrl live = new LiveCtrl(Const.GameItemID.Empty);
    void Start () {
        rb = GetComponent<Rigidbody>();
        mouseInitial = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update () {
        // Move
        if (Input.GetKey(KeyCode.W)) {
            transform.localPosition += moveSpeed * Time.deltaTime * transform.forward;
        }
        else if (Input.GetKey(KeyCode.S)) {
            transform.localPosition += -1 * moveSpeed * Time.deltaTime * transform.forward;
        }
        // Jump
        if (!is_jumping && Input.GetKey(KeyCode.Space)) {
            rb.AddForce(jumpForce * Time.deltaTime * Vector3.up);
            //transform.localPosition += jumpForce * Time.deltaTime * Vector3.up;
            is_jumping = true;
        }
        // Rotate
        Vector3 dis = Input.mousePosition - mouseInitial;
        transform.localEulerAngles = new Vector3(0, rotateSpeed*dis.x, 0);
        Transform cam = transform.Find("Main Camera");
        float rotate = Mathf.Abs(-0.3f * dis.y)>90 ? (dis.y > 0 ? -90 : 90) : - 0.3f * dis.y;
        cam.transform.localEulerAngles = new Vector3(rotate, 0, 0);
        // Click
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
                Const.GameItemID hitId = getItemsID(rch.transform.gameObject.name);
                int instanceId = rch.transform.gameObject.GetInstanceID();
                if(!live.isAlive(hitId, instanceId, 1)) {
                    toolbox.pushItem(hitId);
                    Destroy(rch.transform.gameObject);
                }
            }
        }
        if (Input.GetMouseButton(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
            }
        }
    }
    Const.GameItemID getItemsID(string itemname)
    {
        if (itemname == "dirtGrass(Clone)") return Const.GameItemID.Dirt;
        else {
            Debug.Log("Error! Unknown item name" + itemname);
            return Const.GameItemID.Empty;
        }
    }
    void OnCollisionEnter(Collision c)
    {
        is_jumping = false;
    }
}
