using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Const
{
    // Input Configure
    public const float rotateSpeed = 0.1f;
    public const float updownSpeed = 0.07f;
    // Time Configure
    public const int dayRoutine = 60;
    // Map Configure
    public static Vector3 mapSize = new Vector3Int(50, 50, 50);
    public static Vector3 mapOrigin = new Vector3Int(0, 0, 0);
    // Item Configure
    public const int attackPower = 10;  // MainActor Attack Power
    public const int numItems = 5;
    public const int dirtMaxLive = 5;
    public const int stoneMaxLive = 10;
    public enum GameItemID { Empty, Dirt, DirtGrass, Stone, Creeper };
    // Enemy Configure
    public const int numEnemy = 10;
    public const float appearRadius = 10;
    // Creeper
    //public static Creature creeper = new Creature(20, 30, 5);
    public static int creeperMaxLive = 20;
    public static int creeperTrackDistance = 30;
    public static int creeperAttackDistance = 5;
}
/*public static class Creature
{
    public static int maxLive;
    public static int trackDistance;
    public static int attackDistance;
    public Creature(int live, int track, int attack):base maxLive(live), trackDistance = track,attackDistance = attack;
}*/
public static class ItemMap
{
    static int[] liveMap = new int[Const.numItems]
    {0, Const.dirtMaxLive, Const.dirtMaxLive, Const.stoneMaxLive, Const.creeperMaxLive};
    static string[] textureMap = new string[Const.numItems]
    {"null", "dirt", "dirtGrass", "stone", "null"};
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
    public int isAlive(Const.GameItemID newId, int newInstanceId, float attack = 1)
    {
        if(newInstanceId != instanceId) {
            instanceId = newInstanceId;
            live = ItemMap.getLive(newId);
        }
        else
            live -= attack;
        return Mathf.CeilToInt( 10f * live / ItemMap.getLive(newId) );
        //if (live <= 0)
        //    return false;
        //return true;
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
