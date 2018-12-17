using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Creature
{
    public float maxLive;
    public float attackPower;
    public float trackDistance;
    public float attackDistance;
    public float audioDistance;
    public Creature(float live, float power, float track, float attack, float audio)
    {
        maxLive = live;
        attackPower = power;
        trackDistance = track;
        attackDistance = attack;
        audioDistance = audio;
    }
}
public static class Const
{
    // Input Configure
    public const float rotateSpeed = 0.3f;
    public const float updownSpeed = 0.3f;
    public const float moveSpeed = 5f;
    // Time Configure
    public const int dayRoutine = 60;
    public const float attackTimeInterval = 0.5f;
    // Map Configure
    public static Vector3 mapSize = new Vector3Int(50, 50, 50);
    public static Vector3 mapOrigin = new Vector3Int(0, 0, 0);
    // Item Configure
    public const int numItems = 7;
    public const int dirtMaxLive = 5;
    public const int stoneMaxLive = 10;
    public enum GameItemID { Empty, Dirt, DirtGrass, Stone, Creeper, Slime, MainActor };
    // Main Actor Configure
    public const int attackPower = 10;  // MainActor Attack Power
    public const int maxLive = 13;
    // Enemy Configure
    public const int numEnemy = 20;
    public const float appearRadius = 15;
    // Creeper
    public static Creature Creeper = new Creature(20, 1, 30, 5, 10);
    public static Creature Slime = new Creature(20, 1, 30, 5, 10);
    /*
    public static int creeperMaxLive = 20;
    public static int creeperAttackPower = 1;
    public static int creeperTrackDistance = 30;
    public static int creeperAttackDistance = 5;
    public static int creeperAudioDistance = 10;*/
}
public static class ItemMap
{
    static int[] liveMap = new int[Const.numItems]
    {   0,
        Const.dirtMaxLive,
        Const.dirtMaxLive,
        Const.stoneMaxLive,
        (int)Const.Creeper.maxLive,
        (int)Const.Slime.maxLive,
        Const.maxLive};
    static string[] textureMap = new string[Const.numItems]
    {   "null",
        "dirt",
        "dirtGrass",
        "stone",
        "null",
        "null",
        "null" };

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
    }
    public static Creature getCreatureInfo(Const.GameItemID id)
    {
        if (id == Const.GameItemID.Creeper) return Const.Creeper;
        if (id == Const.GameItemID.Slime) return Const.Slime;
        return Const.Creeper;
    }
}
