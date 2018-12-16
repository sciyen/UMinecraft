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
        if (LightControllor.isNight()) {
            float enemyRatial = Random.Range(0f, 1f);
            if (Enemies.Count < Const.numEnemy && Random.Range(0, 1f)>0.7f)
                pushNewEnemy(getEnemyIdByRatial(enemyRatial));
            for (int i = 0; i < Enemies.Count; i++) {
                // Auto move toward main actor
                float enemyDis = calDistanceWithMainActor(Enemies[i]);
                if (enemyDis < Const.creeperAttackDistance)
                    mainActor.GetComponent<LiveManager>().attack(Const.creeperAttackPower);
                else if (enemyDis < Const.creeperTrackDistance)
                    Enemies[i].GetComponent<AutoMove>().setTarget(mainActor.transform.position);
                /*
                if (enemyDis < Const.creeperAudioDistance) {
                    if(!Enemies[i].GetComponent<AudioSource>().isPlaying)
                        Enemies[i].GetComponent<AudioSource>().Play(0);
                    Enemies[i].GetComponent<AudioSource>().volume = 1 - enemyDis / Const.creeperAudioDistance;
                }
                else 
                    Enemies[i].GetComponent<AudioSource>().Pause();*/
                // Destroy the creature if they died
                LiveManager creeperLive = Enemies[i].GetComponent<LiveManager>();
                if (creeperLive.live <= 0) {
                    Destroy(Enemies[i].gameObject);
                    Enemies.RemoveAt(i);
                }
            }
        }
        else if (Random.Range(0f, 1f) > 0.7){
            int lastIndex = Enemies.Count;
            if (lastIndex > 0) {
                Destroy(Enemies[lastIndex-1].gameObject);
                Enemies.RemoveAt(lastIndex-1);
            }
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
        tmp.name = id.ToString();
        tmp.GetComponent<LiveManager>().reset(id);
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
        if (r < sum) return Const.GameItemID.Creeper;
        return Const.GameItemID.Empty;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
