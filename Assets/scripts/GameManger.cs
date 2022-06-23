using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManger : MonoBehaviour
{
    [System.Serializable]
    public class UI
    {
        [SerializeField] private GameObject goalUI;
        [SerializeField] private GameObject failUI;

        [SerializeField] private TextMeshProUGUI goalText;
        [SerializeField] private TextMeshProUGUI failText;

        private void disableAll()
        {
            this.goalUI.SetActive(false);
            this.failUI.SetActive(false);
        }
        public void success()
        {
            disableAll();
            this.goalUI.SetActive(true);
        }
        public void fail()
        {
            disableAll();
            this.failUI.SetActive(true);
        }
        public void setGaol(string _goal)
        {
            goalText.text = _goal;
        }
        public void setFail(string _fail)
        {
            failText.text = _fail;
        }
    }
    public static GameManger instance;
    [SerializeField] private UI resultUI;
    private int noOfGoals;
    private int noOfFails;
    public bool ballCaught;
    public bool colliadable;
    void Start()
    {
        instance = this;
        colliadable = true;
    }
    public void Success()
    {
        if (GameManger.instance.colliadable)
        {
            resultUI.success();
            noOfGoals++;
            resultUI.setGaol(noOfGoals.ToString());
        }
        
    }
    public void Fail()
    {
        if (GameManger.instance.colliadable)
        {
            resultUI.fail();
            noOfFails++;
            resultUI.setFail(noOfFails.ToString());
        }
    }
}