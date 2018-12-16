using System.Collections;
using System.Collections.Generic;
using UnityEngine;
struct EnemyInfo
{
    public Transform enemy;
    public Const.GameItemID id;
}
public class EnemyControllor : MonoBehaviour {
    public Transform mainActor;
    public Transform enemyProto;
    List<Transform> Enemies = new List<Transform>();
    // Use this for initialization
    void Start () {
    }
	void Update () {
        float enemyRatial = Random.Range(0f, 1f);
        if (Enemies.Count < Const.numEnemy)
            pushNewEnemy(getEnemyIdByRatial(enemyRatial));
        foreach(Transform e in Enemies) {
            if (calDistanceWithMainActor(e) > Const.creeperAttackDistance)
                e.GetComponent<AutoMove>().setTarget(mainActor.transform.position);
        }
	}
    void pushNewEnemy(Const.GameItemID id)
    {
        Transform tmp = Instantiate(enemyProto);
        Vector3 rnd;
        do {
            rnd = Ground.getRandomVector(Const.mapOrigin + Const.mapSize, Const.mapOrigin);
            rnd = Ground.getPointOnGround(rnd);
        } while (calDistanceWithMainActor(rnd) < Const.appearRadius);
        tmp.position = rnd;
        tmp.parent = transform;
        Enemies.Add(tmp);
    }
    float calDistanceWithMainActor()
    {
        return Vector3.Distance(mainActor.transform.position, transform.position);
    }
    float calDistanceWithMainActor(Vector3 p)
    {
        return Vector3.Distance(mainActor.transform.position, p);
    }
    float calDistanceWithMainActor(Transform t)
    {
        return Vector3.Distance(mainActor.transform.position, t.position);
    }
    Vector3 getVectorToMainActor(Transform v)
    {
        return mainActor.transform.position - v.position;
    }
    Const.GameItemID getEnemyIdByRatial(float r)
    {
        int kindOfEnemy = 1;
        float slice = 1f / kindOfEnemy;
        float sum = slice;
        if (r > sum) return Const.GameItemID.Creeper;
        return Const.GameItemID.Empty;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
