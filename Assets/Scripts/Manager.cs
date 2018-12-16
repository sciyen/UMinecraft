using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Const
{
    // Input Configure
    public const float rotateSpeed = 0.1f;
    public const float updownSpeed = 0.07f;
    public const float moveSpeed = 5f;
    // Time Configure
    public const int dayRoutine = 60;
    // Map Configure
    public static Vector3 mapSize = new Vector3Int(50, 50, 50);
    public static Vector3 mapOrigin = new Vector3Int(0, 0, 0);
    // Item Configure
    public const int numItems = 6;
    public const int dirtMaxLive = 5;
    public const int stoneMaxLive = 10;
    public enum GameItemID { Empty, Dirt, DirtGrass, Stone, Creeper, MainActor };
    // Main Actor Configure
    public const int attackPower = 10;  // MainActor Attack Power
    public const int maxLive = 20;
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
    {0, Const.dirtMaxLive, Const.dirtMaxLive, Const.stoneMaxLive, Const.creeperMaxLive, Const.maxLive};
    static string[] textureMap = new string[Const.numItems]
    {"null", "dirt", "dirtGrass", "stone", "null", "null"};

    public static int getLive(Const.GameItemID id)
    {
        return liveMap[(int)id];
    }
    public static string getTextureName(Const.GameItemID id)
    {
        return textureMap[(int)id];
    }
    public static bool isItem(Const.GameItemID id)
    {
        if (id == Const.GameItemID.Dirt
        || id == Const.GameItemID.DirtGrass
        || id == Const.GameItemID.Stone) return true;
        return false;
    }
    public static Const.GameItemID getItemsID(string itemname)
    {
        for (int i = 0; i < Const.numItems; i++)
            if (itemname == ((Const.GameItemID)i).ToString()) return (Const.GameItemID)i;
        Debug.Log("Error! Unknown item name" + itemname);
        return Const.GameItemID.Empty;
        /*
        if (itemname == Const.GameItemID.Dirt.ToString()) return Const.GameItemID.Dirt;
        else if (itemname == Const.GameItemID.DirtGrass.ToString()) return Const.GameItemID.DirtGrass;
        else if (itemname == Const.GameItemID.Stone.ToString()) return Const.GameItemID.Stone;
        else {
            Debug.Log("Error! Unknown item name" + itemname);
            return Const.GameItemID.Empty;
        }*/
    }
}
