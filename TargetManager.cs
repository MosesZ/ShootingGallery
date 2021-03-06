using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour {

    public Text info;
    public Text runTimeText;
	public Text scoreText;
    public Text scoreTable;
    public int maxTargetsCount;
    private static int hittedTargets;
    private static TargetManager gpMan = null;
    private float timePassed = 0;

    private GameObject targets;
    public GameObject button;

    public int difficulty;
    public float runTime;
    public Transform targetTransform;
    public GameObject targetNormal;
    public GameObject targetEazy;

    public delegate void EndAction();
    public static event EndAction OnEnded;

    public static Player currentPlayer;

    /*public TargetManager ()
    {
    
    }*/

    /*public static TargetManager getInstance()
    {
        if (gpMan == null)
        {
            gpMan = new TargetManager();
        }
        return gpMan;
    }*/

    public static void plusHittedTarget()
    {
        hittedTargets += 1;
    }

	// Use this for initialization
	void Start () {
        string dataPath = Application.dataPath + "/gamedata/settings.json";
        if (File.Exists(dataPath))
        {
            string dataAsJson = File.ReadAllText(dataPath);
            currentPlayer = JsonUtility.FromJson<Player>(dataAsJson);
        }
        else
        {
            currentPlayer = new Player("Player", 0, 1);
        }
        difficulty = currentPlayer.difficulty;
        /*TargetManager.getInstance();
        TargetManager.hittedTargets = 0;*/
        switch (difficulty)
        {
            case 1:
                var targets1 = (GameObject)Instantiate(
                        targetEazy,
                        targetTransform.position,
                        targetTransform.rotation);
                runTime = 30; //2 * 60;
                maxTargetsCount = 15;
                targets = targets1;
                break;
            case 2:
                var targets2 = (GameObject)Instantiate(
                        targetNormal,
                        targetTransform.position,
                        targetTransform.rotation);
                targets = targets2;
                maxTargetsCount = 28;
                runTime = 4 * 60;
                break;
            case 3:
                var targets3 = (GameObject)Instantiate(
                        targetNormal,
                        targetTransform.position,
                        targetTransform.rotation);
                targets = targets3;
                maxTargetsCount = 28;
                runTime = 2 * 60;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        info.text = "Targets are hitted: " + hittedTargets;
        if (maxTargetsCount == hittedTargets)
        {
            info.text = "All Targets Are Hitted!";
            gameOver();
        }
        runTime -= Time.deltaTime;
        runTimeText.text = "Time left: " + (Mathf.Ceil(Mathf.Round(runTime)/60) - 1) + " : " + (Mathf.Round(runTime) - 1 - (Mathf.Ceil(Mathf.Round(runTime) / 60) - 1) * 60);
        if (runTime <= 0)
        {
            gameOver();
        }
		scoreText.text = "Score: " + getScore ();
	}

    private int TimePassed()
    {
        timePassed += Time.deltaTime;
        return Mathf.FloorToInt(timePassed);
    }

	public int getScore() {
		return hittedTargets*100 + (int)Mathf.Round(runTime)*10;
	}

    public void startRun()
    {
        TargetScript[] s = targets.GetComponentsInChildren<TargetScript>();
        for (int i = 0; i < s.Length; i++)
        {
            s[i].Trigger();
        }
    }

    void gameOver ()
    {
        if (OnEnded != null)
        {
            OnEnded();
        }
        //SceneManager.LoadScene("endofgame", LoadSceneMode.Single);
        SceneManager.LoadScene("Main_menu", LoadSceneMode.Single);

    }

    void OnEnable()
    {
        ButtonScript.OnClicked += startRun;
    }


    void OnDisable()
    {
        ButtonScript.OnClicked -= startRun;
    }
}
