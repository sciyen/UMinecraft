using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collision))]
public class MainActorControllor : MonoBehaviour {
    public ToolboxController toolbox;
    public Ground ground;

    Rigidbody rb;
    bool is_jumping = false;
    Vector3 mouseInitial;
    ItemCtrl live = new ItemCtrl(Const.GameItemID.Empty);
    float lastJumpTime;
    void Start () {
        rb = GetComponent<Rigidbody>();
        mouseInitial = Input.mousePosition;
        lastJumpTime = Time.time;
        while (!Ground.mapReady) StartCoroutine(wait());
        transform.position = Ground.getPointOnGround(new Vector3(Const.mapSize.x/2, 0, Const.mapSize.z/2));
    }
	IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
    }
	// Update is called once per frame
	void Update () {
        // Move
        #region Move
        if (Input.GetKey(KeyCode.W)) {
            transform.localPosition += Const.moveSpeed * Time.deltaTime * transform.forward;
        }
        else if (Input.GetKey(KeyCode.S)) {
            transform.localPosition += -1 * Const.moveSpeed * Time.deltaTime * transform.forward;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.localPosition += Const.moveSpeed * Time.deltaTime * transform.right;
        }
        else if (Input.GetKey(KeyCode.A)) {
            transform.localPosition += -1 * Const.moveSpeed * Time.deltaTime * transform.right;
        }
#endregion
        // Jump
        #region Jump
        if (!is_jumping && Time.time - lastJumpTime > 0.5 && Input.GetKey(KeyCode.Space)) {
            //rb.AddForce(jumpForce * Time.deltaTime * Vector3.up, ForceMode.Impulse);
            is_jumping = true;
            lastJumpTime = Time.time;
            rb.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
            //transform.localPosition += jumpForce * Time.deltaTime * Vector3.up;
        }
#endregion
        // Rotate
        #region Rotate
        Vector3 dis = Input.mousePosition - mouseInitial;
        transform.localEulerAngles = new Vector3(0, Const.rotateSpeed*dis.x, 0);
        Transform cam = transform.Find("Main Camera");
        float rotate = Mathf.Abs(-1*Const.updownSpeed * dis.y)>90 ? (dis.y > 0 ? -90 : 90) : -1 * Const.updownSpeed * dis.y;
        cam.transform.localEulerAngles = new Vector3(rotate, 0, 0);
        #endregion
        // Click
        #region MouseClick
        if (Input.GetMouseButton(0)) {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch)) {
                Const.GameItemID hitId = ItemMap.getItemsID(rch.transform.gameObject.name);
                int instanceId = rch.transform.gameObject.GetInstanceID();
                // Cube
                if (ItemMap.isItem(hitId)) {     
                    int destroyLevel = live.isAlive(hitId, instanceId, Const.attackPower * Time.deltaTime);
                    if (destroyLevel <= 0) {
                        toolbox.pushItem(hitId);
                        Destroy(rch.transform.gameObject);
                    }
                    else {
                        string name = "destroy/destroy_stage_" + destroyLevel;
                        Material breakM1 = rch.transform.GetComponent<Renderer>().material;
                        breakM1.mainTexture = (Texture)Resources.Load(name);
                        rch.transform.GetComponent<Renderer>().material = breakM1;
                    }
                }
                // Creature
                else {
                    rch.transform.GetComponent<LiveManager>().attack(Const.attackPower);
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
#endregion
    }
    void OnCollisionEnter(Collision other)
    {
        if(Vector3.Angle(other.contacts[0].normal, Vector3.up)<10)
            is_jumping = false;
    }
}
