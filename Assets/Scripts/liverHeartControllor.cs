using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liverHeartControllor : MonoBehaviour {
    public GameObject gameOver;
    public Image liveHeartProto;
    public LiveManager mainActorLive;
    Vector2 liveHeartSize = new Vector2(35, 35);  //Width Height
    Vector3 liveHeartPositionOffset = new Vector3(-340, ToolboxController.toolboxY + 75, 0);
    Vector3[] liveHeartPosition = new Vector3[Const.maxLive];

    Image[] liveHeart = new Image[Const.maxLive];
    // Use this for initialization
    void Start () {
        gameOver.SetActive(false);
        mainActorLive.reset(Const.GameItemID.MainActor);
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
        if(!clickEvent.modestate && mainActorLive.live <= 0) {
            gameOver.SetActive(true);
            Cursor.visible = true;
        }
    }
    public void updateLiveHeart()
    {
        if (!clickEvent.modestate)
            for (int i = 0; i < Const.maxLive; i++) {
                if (i < mainActorLive.live) liveHeart[i].gameObject.SetActive(true);
                else liveHeart[i].gameObject.SetActive(false);
            }
    }
}
