using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collision))]
public class MainActorControllor : MonoBehaviour {
    public ToolboxController toolbox;
    public Ground ground;
    public float moveSpeed = 5;
    public float rotateSpeed = 0.1f;

    Rigidbody rb;
    bool is_jumping = false;
    Vector3 mouseInitial;
    ItemCtrl live = new ItemCtrl(Const.GameItemID.Empty);
    void Start () {
        rb = GetComponent<Rigidbody>();
        mouseInitial = Input.mousePosition;
        transform.position = Const.mapOrigin + new Vector3(Const.mapSize.x/2, 10, Const.mapSize.z/2);
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
            //rb.AddForce(jumpForce * Time.deltaTime * Vector3.up, ForceMode.Impulse);
            is_jumping = true;
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            //transform.localPosition += jumpForce * Time.deltaTime * Vector3.up;
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
                //Debug.Log("Id= "+ hitId.ToString());
                int instanceId = rch.transform.gameObject.GetInstanceID();
                int destroyLevel = live.isAlive(hitId, instanceId, Const.attackPower * Time.deltaTime);
                if (destroyLevel <= 0) {
                    toolbox.pushItem(hitId);
                    Destroy(rch.transform.gameObject);        
                }
                else {
                    string name = "destroy/destroy_stage_" + destroyLevel;/*
                    Material[] breakMl = new Material[2];
                    Material breakM2 = new Material();
                    breakM2.mainTexture = (Texture)Resources.Load(name);
                    breakMl[0] = rch.transform.GetComponent<Renderer>().material;
                    breakMl[1] = breakM2;// (Material)Resources.Load(name);*/
                    Material breakM1 = rch.transform.GetComponent<Renderer>().material;
                    breakM1.mainTexture = (Texture)Resources.Load(name);
                    rch.transform.GetComponent<Renderer>().material = breakM1;
                }
            }
        }
        if (Input.GetMouseButton(1)) {
            if (toolbox.isSelected()) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rch;
                if (Physics.Raycast(ray, out rch)) {
                    Vector3 target = rch.point + rch.normal/2;
                    target.x = Mathf.Round(target.x);
                    target.y = Mathf.Round(target.y);
                    target.z = Mathf.Round(target.z);
                    ground.instantiateItem(toolbox.deleteSeletedItem(), target);
                }
            }
        }
    }
    Const.GameItemID getItemsID(string itemname)
    {
        if (itemname == Const.GameItemID.Dirt.ToString()) return Const.GameItemID.Dirt;
        else if (itemname == Const.GameItemID.DirtGrass.ToString()) return Const.GameItemID.DirtGrass;
        else if (itemname == Const.GameItemID.Stone.ToString()) return Const.GameItemID.Stone;
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
