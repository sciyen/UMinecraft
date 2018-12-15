using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class singleplayer : MonoBehaviour
{
    public Canvas single;
    public Canvas home;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //get current scene
        Scene cur_scene = SceneManager.GetActiveScene();
        Debug.Log("current scene = " + cur_scene.name);
        Debug.Log("current scene build index = " + cur_scene.buildIndex);

        //load new scene
        SceneManager.LoadScene(1);
    }

    public void OnPointerClick(PointerEventData e)
    {
        single.GetComponent<CanvasGroup>().alpha = 1f;
        home.GetComponent<CanvasGroup>().alpha = 1f;
    }
}
