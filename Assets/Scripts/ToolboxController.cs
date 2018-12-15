﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolboxController : MonoBehaviour {
    public enum GameItems {Empty, Dirt, Stone};
    const int numToolbox = 9;
    static int selectedIndex = 0;
    static GameItems[] toolbox = new GameItems[numToolbox];
    static int[] toolCount = new int[numToolbox];
    Vector3[] toolboxPosition = new Vector3[numToolbox];
    Vector2 itemSize = new Vector2(80, 84); //Width Height
    Vector2 toolSize = new Vector2(66, 66);  //Width Height
    Vector3 toolboxPositionOffset = new Vector3(-320, -300, 0);
    Transform[] toolImages = new Transform[numToolbox];
	// Use this for initialization
	void Start () {
        for (int i = 0; i < numToolbox; i++) {
            toolboxPosition[i] = new Vector3(toolboxPositionOffset.x + i * itemSize.x, toolboxPositionOffset.y, 0);
            toolbox[i] = GameItems.Empty;
            toolCount[i] = 0;
            toolImages[i] = Instantiate(toolImage);
            toolImages[i].parent = transform;
            toolImages[i].localPosition = toolboxPosition[i];
            toolImages[i].gameObject.SetActive(false);
        }
        pushItem(GameItems.Dirt);
        pushItem(GameItems.Stone);
        deleteItem(GameItems.Dirt);
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
    void updateToolbox()
    {
        for (int i = 0; i < numToolbox; i++) {
            if (toolCount[i] > 0) toolImages[i].gameObject.SetActive(true);
            else toolImages[i].gameObject.SetActive(false);
        }
    }
    int getEmptyIndex()
    {
        for(int i = 0; i < numToolbox; i++)
            if (toolCount[i] == 0) return i;
        return -1;
    }
    bool pushItem(GameItems itemname)
    {
        int index = -1;
        for (int i = 0; i < numToolbox; i++)
            if (toolbox[i] == itemname) index = i;
        if (index == -1){  //Get a new column to store new tool
            index = getEmptyIndex();
            if (index != -1) 
                toolbox[index] = itemname;
            else {
                Debug.Log("Error! Toolbox is full!");
                return false;
            }
        }
        toolCount[index]++;
        selectedIndex = index;
        updateToolbox();
        return true;
    }
    bool deleteItem(GameItems itemname)
    {
        int index = -1;
        for (int i = 0; i < numToolbox; i++)
            if (toolbox[i] == itemname) index = i;
        if (index == -1) return false;
        toolCount[index]--;
        if (toolCount[index] <= 0)
            toolbox[index] = GameItems.Empty;
        selectedIndex = index;
        updateToolbox();
        return true;
    }
    public Transform toolImage;
}