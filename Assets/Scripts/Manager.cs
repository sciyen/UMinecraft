using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Const
{
    public static Vector3 mapSize = new Vector3Int(50, 50, 50);
    public static Vector3 mapOrigin = new Vector3Int(0, 0, 0);
    public const int numItems = 3;
    public const int dirtMaxLive = 5;
    public const int stoneMaxLive = 10;
    public const int attackPower = 10;
    public enum GameItemID { Empty, Dirt, Stone };
}
public static class ItemMap
{
    static int[] liveMap = new int[Const.numItems]
    {0, Const.dirtMaxLive, Const.stoneMaxLive};
    static string[] textureMap = new string[Const.numItems]
    {"null", "dirt", "stone"};
    public static int getLive(Const.GameItemID id)
    {
        return liveMap[(int)id];
    }
    public static string getTextureName(Const.GameItemID id)
    {
        return textureMap[(int)id];
    }
}
public class ItemCtrl {
    float live;
    int instanceId;
    Const.GameItemID itemId;
    public ItemCtrl(Const.GameItemID newId, int newInstanceId=0)
    {
        itemId = newId;
        live = ItemMap.getLive(newId);
    }
    public bool isAlive(Const.GameItemID newId, int newInstanceId, float attack = 1)
    {
        if(newInstanceId != instanceId) {
            instanceId = newInstanceId;
            live = ItemMap.getLive(newId);
        }
        else
            live -= attack;
        if (live <= 0)
            return false;
        return true;
    }
    public void reset()
    {
        live = ItemMap.getLive(itemId);
    }
}
public class Manager : MonoBehaviour {
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
