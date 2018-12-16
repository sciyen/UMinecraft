using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolboxController : MonoBehaviour
{
    public Image toolImage;

    const int numToolbox = 9;
    static int selectedIndex = 0;
    static Const.GameItemID[] toolbox = new Const.GameItemID[numToolbox];
    static int[] toolCount = new int[numToolbox];
    public static float toolboxY = -1 * Screen.height * 0.4f;
    Vector3[] toolboxPosition = new Vector3[numToolbox];
    Vector2 itemSize = new Vector2(80, 84); //Width Height
    Vector2 toolSize = new Vector2(66, 66);  //Width Height
    Vector3 toolboxPositionOffset = new Vector3(-320, toolboxY, 0);
    
    //Transform[] toolImages = new Transform[numToolbox];
    Image[] toolImages = new Image[numToolbox];
    // Use this for initialization
    void Start () {
        GameObject.Find("toolbox").transform.localPosition = new Vector3(0, toolboxY, 0);
        for (int i = 0; i < numToolbox; i++) {
            toolboxPosition[i] = new Vector3(toolboxPositionOffset.x + i * itemSize.x, toolboxPositionOffset.y, 0);
            toolbox[i] = Const.GameItemID.Empty;
            toolCount[i] = 0;
            toolImages[i] = Instantiate(toolImage);
            //toolImages[i].SetParent(transform);
            //toolImages[i].localPosition = toolboxPosition[i];
            toolImages[i].rectTransform.SetParent(transform);
            toolImages[i].rectTransform.localPosition = toolboxPosition[i];
            toolImages[i].gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f)
            toolboxIndexOffset((int)Input.mouseScrollDelta.y);
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
    public bool pushItem(Const.GameItemID itemname)
    {
        if (itemname == Const.GameItemID.DirtGrass) itemname = Const.GameItemID.Dirt;
        int index = -1;
        for (int i = 0; i < numToolbox; i++)
            if (toolbox[i] == itemname) index = i;
        if (index == -1){  //Get a new column to store new tool
            index = getEmptyIndex();
            if (index != -1) {
                toolbox[index] = itemname;
                //Sprite texture = (Sprite)Resources.Load(ItemMap.getTextureName(itemname));
                //toolImages[index].overrideSprite = texture;
                //toolImages[index].GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture>("sprite/" + ItemMap.getTextureName(itemname));
                toolImages[index].GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("sprite/" + ItemMap.getTextureName(itemname));
                Debug.Log("Source loaded");
            }
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
    public bool delete(int index)
    {
        if (index == -1) return false;
        toolCount[index]--;
        if (toolCount[index] <= 0)
            toolbox[index] = Const.GameItemID.Empty;
        updateToolbox();
        return true;
    }
    public bool deleteItemById(Const.GameItemID itemname)
    {
        int index = -1;
        for (int i = 0; i < numToolbox; i++)
            if (toolbox[i] == itemname) index = i;
        return delete(index);
    }
    public bool isSelected()
    {
        if (getSeletedItem() == Const.GameItemID.Empty) return false;
        return true;
    }
    public Const.GameItemID getSeletedItem()
    {
        return toolbox[selectedIndex];
    }
    public Const.GameItemID deleteSeletedItem()
    {
        Const.GameItemID tmp = getSeletedItem();
        delete(selectedIndex);
        return tmp;
    }
}
