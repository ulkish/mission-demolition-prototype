using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    // A private Singleton.
    static private MissionDemolition S;

    [Header("Set in Inspector")]
    // The UIText_Level Text
    public Text         uitLevel;
    // The UIText_Shots Text
    public Text         uitShots;
    // The Text on UIButton_View
    public Text         uitButton;
    // The place to put castles.
    public Vector3      castlePos;
    // An array of the castles.
    public GameObject[] castles;

    [Header("Set Dynamically")]
    // The current level.
    public int        level;
    // The number of levels.
    public int        levelMax;
    public int        shotsTaken;
    // The current castle.
    public GameObject castle;
    public GameMode   mode = GameMode.idle;
    // FollowCam mode.
    public string     showing = "Show Slingshot";

    // Start is called before the first frame update
    void Start()
    {
        // Define the Singleton.
        S = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // Get rid of the old castle if one exists.
        if (castle != null)
        {
            Destroy(castle);
        }

        // Destroy old projectiles if they exist.
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // Instantiate the new castle.
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // Reset the camera.
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        // Reset the goal.
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        // Show the data in the GUITexts.
        uitLevel.text = "Level: " + (level+1) + "of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        // Check for level end.
        if ( (mode == GameMode.playing) && Goal.goalMet)
        {
            // Change mode to stop checking for level end.
            mode = GameMode.levelEnd;
            // Zoom out.
            SwitchView("Show Both");
            // Start the next level in 2 seconds.
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    // Static method that allows code anywhere to increment shotsTaken.
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
