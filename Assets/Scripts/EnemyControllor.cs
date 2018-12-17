using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyControllor : MonoBehaviour {
    public Transform mainActor;
    public Transform creeperProto;
    public Transform slimeProto;
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
                Const.GameItemID monsterId = ItemMap.getItemsID(Enemies[i].name);
                Creature creatureInfo = ItemMap.getCreatureInfo(monsterId);
                if (enemyDis < creatureInfo.attackDistance) {
                    mainActor.GetComponent<LiveManager>().attack(creatureInfo.attackPower);
                    if(monsterId == Const.GameItemID.Slime) {
                        Enemies[i].GetComponent<Collider>().isTrigger = true;
                        Enemies[i].GetComponent<Rigidbody>().useGravity = false;
                        Enemies[i].localScale = new Vector3(10, 10, 10);
                    }
                }
                else if (enemyDis < creatureInfo.trackDistance)
                    Enemies[i].GetComponent<AutoMove>().setTarget(mainActor.transform.position);
                // Play audio
                AudioSource audio = Enemies[i].GetComponent<AudioSource>();
                if (enemyDis < creatureInfo.audioDistance) {
                    if(!audio.isPlaying)    audio.Play(0);
                    audio.volume = 1 - enemyDis / creatureInfo.audioDistance;
                }
                else
                    audio.Pause();
                // Destroy the creature if they died
                LiveManager monster = Enemies[i].GetComponent<LiveManager>();
                if (monster.live <= 0) {
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
        Transform tmp;
        if(id == Const.GameItemID.Creeper) tmp = Instantiate(creeperProto);
        else tmp = Instantiate(slimeProto);
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
        int kindOfEnemy = 2;
        float slice = 1f / kindOfEnemy;
        float sum = slice;
        if (r < sum) return Const.GameItemID.Creeper;
        return Const.GameItemID.Slime;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
