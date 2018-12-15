using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clickEvent : MonoBehaviour {

    bool modestate = false;

    public GameObject single;
    public GameObject establish;
    public GameObject develop;
    public GameObject multi;
    public GameObject home;
    public GameObject option;
    public Button enter_butt;
    public Button edit_butt;
    public Button delete_butt;
    public Button rebuild_butt;
    public InputField input;

    public void Start()
    {
        updateAll();
        home.gameObject.SetActive(true);
    }
    public void Update()
    {
        textbox.text = "將被儲存於： " + input.text;
    }

    public void developer()
    {
        develop.gameObject.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
    }


    ////////////////single////////////////////////////
    public void singleHome()
    {
        updateAll();
        single.gameObject.SetActive(true);
        Debug.Log("single");
    }

    public void cancel()
    {
        updateAll();
        Debug.Log("single");
        home.gameObject.SetActive(true);
    }

    public void createNew()
    {
        updateAll();
        Debug.Log("single");
        establish.gameObject.SetActive(true);
    }

    public GameObject mode_butt;
    public Text content;
    public Text textbox;
    public void Mode()
    {
        if(modestate)
        {
            mode_butt.GetComponentInChildren<Text>().text = "遊戲模式：生存";
            content.text = "搜索資源、合成，提升\n等級、生命值和飢餓值";
            modestate = false;
        }
        else
        {
            mode_butt.GetComponentInChildren<Text>().text = "遊戲模式：創造";
            content.text = "無限資源、自由飛行，\n而且能夠瞬間破壞方塊";
            modestate = true;
        }
    }

    public void generate()
    {
        SceneManager.LoadScene(1);
    }

    public void createCancel()
    {
        updateAll();
        single.gameObject.SetActive(true);
        Debug.Log("single");
    }

    public void chooseWorld()
    {

    }

    public void edit()
    {

    }

    public void delete()
    {

    }

    public void rebulid()
    {
        Debug.Log("single");
        updateAll();
        establish.gameObject.SetActive(true);

    }


    ///////////////multi///////////////
    public void visibility()
    {
        updateAll();
        multi.gameObject.SetActive(true);
        Debug.Log("multi");
    }
    
    void updateAll()
    {
        multi.gameObject.SetActive(false);
        home.gameObject.SetActive(false);
        single.gameObject.SetActive(false);
        establish.gameObject.SetActive(false);
        develop.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        enter_butt.interactable = false;
        edit_butt.interactable = false;
        delete_butt.interactable = false;
        rebuild_butt.interactable = false;
    }
}
