using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxController : MonoBehaviour {
    enum GameItems {Dirt, Stone};
    const int numToolbox = 9;
    static int[] toolbox = new int[numToolbox];
    Vector3[] toolboxPosition = new Vector3[numToolbox];
    Vector2 itemSize = new Vector2(80, 100); //Width Height
    Vector3 itemPositionOffset = new Vector3(-320, -300, 0);
    static int selectedIndex = 0;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < numToolbox; i++)
            toolboxPosition[i] = new Vector3(itemPositionOffset.x + i*itemSize.x, itemPositionOffset.y, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f) {
            toolboxIndexOffset((int)Input.mouseScrollDelta.y);
            Debug.Log(selectedIndex);
        }
        transform.Find("toolbox_selected").transform.localPosition = toolboxPosition[selectedIndex];
	}
    void toolboxIndexOffset(int offset)
    {
        selectedIndex += offset;
        if (selectedIndex < 0) selectedIndex += numToolbox;
        selectedIndex %= numToolbox;
    }
}
