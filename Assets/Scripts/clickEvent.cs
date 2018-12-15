using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickEvent : MonoBehaviour {

    public GameObject single;
    public GameObject establish;
    public GameObject develop;
    public GameObject multi;
    public GameObject home;

    // Use this for initialization
    void Start () {
        single.gameObject.SetActive(false);
        establish.gameObject.SetActive(false);
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
        single.gameObject.SetActive(true);
        Debug.Log("single");
        home.gameObject.SetActive(false);
    }

    public void cancel()
    {
        single.gameObject.SetActive(false);
        Debug.Log("single");
        home.gameObject.SetActive(true);
    }

    public void createNew()
    {
        single.gameObject.SetActive(false);
        Debug.Log("single");
        establish.gameObject.SetActive(true);
    }

    public void Mode()
    {

    }

    public void generate()
    {
        SceneManager.LoadScene(1);
    }

    public void createCancel()
    {
        single.gameObject.SetActive(true);
        Debug.Log("single");
        establish.gameObject.SetActive(false);
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
        single.gameObject.SetActive(false);
        Debug.Log("single");
        establish.gameObject.SetActive(true);

    }


    ///////////////multi///////////////
    public void visibility()
    {
        multi.gameObject.SetActive(true);
        Debug.Log("multi");
        home.gameObject.SetActive(false);
    }
    
}
