using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightControllor : MonoBehaviour {
    // Use this for initialization
    public static float day = 0;
    public static float min = 0;
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timeUpdate();
        sunCtrl();
    }
    void timeUpdate()
    {
        min = Time.time - day * Const.dayRoutine;
        if (min > Const.dayRoutine) day += 1;
    }
    void sunCtrl()
    {
        Light light = transform.GetComponent<Light>();
        if (min < Const.dayRoutine / 2) light.intensity = 1;
        else light.intensity = 0.1f;
        transform.localEulerAngles = new Vector3(min * 360 / Const.dayRoutine, 0, 0);
        //light.spotAngle = min * 360 / Const.dayRoutine;
    }
    public static bool isNight()
    {
        return min > Const.dayRoutine / 2;
    }
}
