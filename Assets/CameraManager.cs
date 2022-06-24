using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public GameObject primaryCam;
    public GameObject secondaryCam;
    void Start()
    {
        instance = this;
        enablePrimaryCam();
    }
    private void disableCam()
    {
        primaryCam.SetActive(false);
        secondaryCam.SetActive(false);
    }
    public void enablePrimaryCam()
    {
        disableCam();
        primaryCam.SetActive(true);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void enableSecondaryCam()
    {
        disableCam();
        secondaryCam.SetActive(true);
        Time.timeScale = 0.8f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
