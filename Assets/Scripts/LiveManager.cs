using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveManager : MonoBehaviour {
    public float live;
    public Const.GameItemID itemId;
    float lastAttackTime;
    public void reset(Const.GameItemID newId)
    {
        itemId = newId;
        live = ItemMap.getLive(newId);
    }
    public void attack(float power)
    {
        if(Time.time - lastAttackTime > Const.attackTimeInterval) {
            live -= power;
            lastAttackTime = Time.time;
        }
    }
    public void relive()
    {
        live = ItemMap.getLive(itemId);
    }
    // Use this for initialization
    void Start () {
        lastAttackTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
public class ItemCtrl:LiveManager
{
    int instanceId;
    public Const.GameItemID itemId;
    public ItemCtrl(Const.GameItemID newId, int newInstanceId = 0)
    {
        base.reset(newId);
    }
    public int isAlive(Const.GameItemID newId, int newInstanceId, float attack = 1)
    {
        if (newInstanceId != instanceId) {
            instanceId = newInstanceId;
            live = ItemMap.getLive(newId);
        }
        else
            live -= attack;
        return Mathf.CeilToInt(10f * live / ItemMap.getLive(newId));
    }
}