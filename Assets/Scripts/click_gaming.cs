using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class click_gaming : MonoBehaviour {

    public GameObject pauseMode;
    bool state = false;

    public void Start()
    {
        pauseMode.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state)
            {
                pauseMode.gameObject.SetActive(false);
                state = false;
            }
            else
            {
                pauseMode.gameObject.SetActive(true);
                state = true;
            }
        }
    }

    public void pause()
    {
        pauseMode.gameObject.SetActive(false);
        state = false;
    }

    public void save()
    {
        Scene cur_scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }
}
