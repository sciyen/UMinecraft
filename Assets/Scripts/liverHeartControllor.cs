using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liverHeartControllor : MonoBehaviour {

    public Image liveHeartProto;
    LiveManager mainActorLive;
    Vector2 liveHeartSize = new Vector2(40, 40);  //Width Height
    Vector3 liveHeartPositionOffset = new Vector3(-320, ToolboxController.toolboxY + 80, 0);
    Vector3[] liveHeartPosition = new Vector3[Const.maxLive];

    Image[] liveHeart = new Image[Const.maxLive];
    // Use this for initialization
    void Start () {
        for (int i = 0; i < Const.maxLive; i++) {
            liveHeartPosition[i] = new Vector3(liveHeartPositionOffset.x + i * liveHeartSize.x, liveHeartPositionOffset.y, 0);
            liveHeart[i] = Instantiate(liveHeartProto);
            liveHeart[i].rectTransform.SetParent(transform);
            liveHeart[i].rectTransform.localPosition = liveHeartPosition[i];
            liveHeart[i].gameObject.SetActive(false);
        }
        updateLiveHeart();
    }
	
	// Update is called once per frame
	void Update () {
        updateLiveHeart();
    }
    public void updateLiveHeart()
    {
        mainActorLive = GameObject.Find("MainActor").GetComponent<LiveManager>();
        Debug.Log("Live=" + GameObject.Find("MainActor").GetComponent<LiveManager>().live);
        for (int i = 0; i < Const.maxLive; i++) {
            if (i < mainActorLive.live) liveHeart[i].gameObject.SetActive(true);
            else liveHeart[i].gameObject.SetActive(false);
        }
    }
}
