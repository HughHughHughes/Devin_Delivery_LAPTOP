using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("Player Status")]
    public bool pauseScreenActive;

    public GameObject UI_Ingame_pause_overlay;
    
    //
    public GameObject player;

    [Header("Player Status")]

    [Header("YellowQuestvars")]
    public bool rockLauncherEquiped; // YELLOW ROOM MISSION COMPLETED
    public bool yellowAmmoCollected; //YELLOW ROOM MISSION COMPLETED

    [Header("RedQuestVars")]
    public bool redAmmoCollected;
    public bool redQuestActive;

    [Header("PlayerWinVars")]
    public bool playerWins; //open the last door to leave
    public GameObject UI_PlayerWins;

    public bool firstTimeHittingRed;
    public int playerMaxHP; //
    public int playerHP; // link this to the canvas displaying HP.
    public float playerMaxAmmo;
    public float playerAmmo; // link this to the canvas displaying ammo.
    //
    [Header("Core Game loop UI")]
    public Canvas Player_UI_Canvas;
    //public Image UI_Crosshair;
    public GameObject UI_AmmoDisplay;
    public GameObject UI_HpBar;
    //
    [Header("YellowQuestVars")]
    public bool YellowQuestActive = true; // set to true from start of game - flash light to lead them to yellow room. once they hit yellowroom door, turn this off and lock them in room.
    public bool Yellow_Room_Door_Closed = false;
    public int yelFlash;
    public float yelTimer = 0;

    public string whatHap = "";



    public bool Red_Room_Door_Closed = false;
    public int redFlash;
    public float redTimer = 0;


    [Header("Checkpoints")]
    public GameObject PlayerRespawnLocation;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;
    public GameObject checkpoint5;



    public GameObject UI_EndGame_Overlay;
    //public Button m_Button1, m_Button2, m_Button3;
    public GameObject UI_Loading; //begining of each reset.

    public GameObject UI_youdied;

    public float loadingBar;

    [Header("Show Read Vars")]
    public bool resetGame;
    float maxIdleCounter = 2000;
    public float currentIdleCounter; //if this hits 0, then return the player back to title screen!

    private void Awake()
    {
        //loadingBar = 100; //enable this when testing shtuff
        currentIdleCounter = maxIdleCounter;

        player = GameObject.Find("Player");
        PlayerRespawnLocation = GameObject.Find("--Respawn_Location_Manager--");
        checkpoint1 = GameObject.Find("checkpoint1");
        checkpoint2 = GameObject.Find("checkpoint2");
        checkpoint3 = GameObject.Find("checkpoint3");
        checkpoint4 = GameObject.Find("checkpoint4");
        checkpoint5 = GameObject.Find("checkpoint5");
        //rockLauncherEquiped = true;
        //firstTimeHittingRed = true;
        playerHP = 4;
        playerMaxHP = 4;
        //playerMaxAmmo = 20;
        playerAmmo = playerMaxAmmo;


        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 240;

        pauseScreenActive = false;
    }
    void Start()
    {
        //m_Button1.onClick.AddListener(TaskOnClickButton1);
       // m_Button2.onClick.AddListener(TaskOnClickButton2);
        //m_Button3.onClick.AddListener(TaskOnClickButton3);
        if (!rockLauncherEquiped)
        {
            //UI_Crosshair.enabled = false;
            UI_AmmoDisplay.SetActive(false);
        }
        if (!firstTimeHittingRed)
        {
           // UI_HpBar.SetActive(false);
        }
    }


    public void ResetScene() //INNITIAL OVERLAY FOR THE START/RESET OF SCENE.
    {
        loadingBar += 50 * Time.deltaTime;
        UI_Loading.SetActive(true);
    }
    public void GameIdleReset()
    {
        if (loadingBar >= 100)
        {
            currentIdleCounter -= Time.deltaTime; //DISABLE THIS WHEN TESTING GAME OTHERWISE IT WILL RESTART

            if (currentIdleCounter <= 0)
            {
                //SceneManager.LoadScene("0.TitleScreen");
            }

            if (Input.anyKey)
            {
                currentIdleCounter = maxIdleCounter;
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentIdleCounter = maxIdleCounter;
            }
            if (Input.GetMouseButtonDown(1))
            {
                currentIdleCounter = maxIdleCounter;
            }
            if (Input.GetMouseButtonDown(2))
            {
                currentIdleCounter = maxIdleCounter;
            }
            if (Input.GetMouseButtonDown(3))
            {
                currentIdleCounter = maxIdleCounter;
            }

        }
    }
    void Update()
    {
        GameIdleReset();

       // UIEnable();

        //if (loadingBar < 100f)
        //{
        //    player.transform.position = PlayerRespawnLocation.GetComponent<PlayerRespawnLocation>().Latest_Checkpoint.transform.position;
        //    ResetScene();
        //}
        //if (loadingBar >= 100f)
        //{
        //    UI_Loading.SetActive(false);
        //    UI_Ingame_pause_overlay.SetActive(false);
        //}

        //if (pauseScreenActive!)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //    UI_Ingame_pause_overlay.SetActive(false);
        //}
        //if (pauseScreenActive == true)
        //{

        //    UI_Ingame_pause_overlay.SetActive(true);
        //}

        if (playerHP <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                //UI_youdied.SetActive(false);
            }
           // UI_End_Game();

        }






        if (playerHP > 0 && playerWins == false)
        {


            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    pauseScreenActive = true;

            //}

            //if (pauseScreenActive == true)
            //{
            //    OptionScreen();
            //}

            //UI_EndGame_Overlay.SetActive(false);
            if (rockLauncherEquiped == false)
            {
                YellowQuest();
            }

            if (rockLauncherEquiped == true && redAmmoCollected == false)
            {
                RedQuest();
                YellowQuestActive = false;
                redQuestActive = true;
            }
            if (rockLauncherEquiped == true && redAmmoCollected == true)
            {
                RedQuest();
                YellowQuestActive = false;
                redQuestActive = false;
            }
        }


        //END OF GAME UI

        if (playerWins == true)
        {
            //UI_End_Game();

        }

    }

    void UI_End_Game()
    {
        return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (playerWins == true)
        {
            UI_PlayerWins.SetActive(true);
            UI_youdied.SetActive(false);

        }
        if (playerWins == false)
        {
            UI_PlayerWins.SetActive(false);
            UI_youdied.SetActive(true);
        }


        //enable you died or u won text
        if (Input.GetMouseButtonDown(0))
        {
            UI_PlayerWins.SetActive(false);
            UI_youdied.SetActive(false);
            UI_EndGame_Overlay.SetActive(true);

        }




        // ResetScene();
    }
    void TaskOnClickButton1()
    {
        return;
        //UI_End_Game - Button 1
        Debug.Log("You have clicked the Restart Game Button!");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //SceneManager.LoadScene("2.Main_Game_LEVEL_1");
    }
    void TaskOnClickButton2()
    {
        return;
        //UI_End_Game - Button 2
        Debug.Log("You have clicked to Continue from your last Checkpoint!");

        if (playerWins == false)
        {


            playerWins = false;
            playerHP = playerMaxHP;
            UI_youdied.SetActive(false);
            UI_Ingame_pause_overlay.SetActive(false);
            player.transform.parent = null;
            player.GetComponent<PlayerMovementFP>().velx = 0;
            player.GetComponent<PlayerMovementFP>().vely = 0;
            player.GetComponent<PlayerMovementFP>().velz = 0;
           // player.GetComponent<PlayerMovementFP>().flashScreen = 0;
           /// //player.GetComponent<PlayerMovementFP>().flashed = false;
           // player.GetComponent<PlayerMovementFP>().redFlashIMG.SetActive(false);
            //player.transform.position = PlayerRespawnLocation.GetComponent<PlayerRespawnLocation>().Latest_Checkpoint.transform.position;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UI_EndGame_Overlay.SetActive(false);
            playerHP = playerMaxHP;
            loadingBar = 0;
        }

        if (playerWins == true)
        {

           // SceneManager.LoadScene("2.Main_Game_LEVEL_1");
        }

    }
    void TaskOnClickButton3()
    {
        return;
        //UI_End_Game - Button 3
        Debug.Log("You have clicked the button!");
        //SceneManager.LoadScene("1.MainMenu");
    }
    void OptionScreen()
    {
        return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseScreenActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;

        UI_Ingame_pause_overlay.SetActive(true);


    }
    void UIEnable()
    {
        return;
        if (rockLauncherEquiped)
        {
            //UI_Crosshair.enabled = true;
            //UI_AmmoDisplay.SetActive(true);
        }


        if (firstTimeHittingRed)
        {
            //UI_HpBar.SetActive(true);
        }
    }
    void YellowQuest()
    {
        
        if (YellowQuestActive == true)
        {

            if (yelTimer > 1f)
            {
                yelTimer = 0;
            }


            yelTimer += Time.deltaTime;

            if (yelFlash == 0)
            {
                whatHap = "noFLASH";
                if (yelTimer >= 1)
                {
                    yelFlash += 1;
                    yelTimer = 0;
                }
            }
            if (yelFlash == 1)
            {
                whatHap = "FLASHING!!!!!!!!!!!!!";
                if (yelTimer >= 1)
                {
                    yelFlash += 1;
                    yelTimer = 0;
                }
            }

            if (yelFlash > 1)
            {
                yelFlash = 0;
            }

        }

        if (YellowQuestActive == false)
        {
            yelFlash = 0;
            yelTimer = 0;
        }

    }

    void RedQuest()
    {
        if (redQuestActive == true)
        {

            if (redTimer > 1f)
            {
                redTimer = 0;
            }


            redTimer += Time.deltaTime;

            if (redFlash == 0)
            {
                whatHap = "noFLASH";
                if (redTimer >= 1)
                {
                    redFlash += 1;
                    redTimer = 0;
                }
            }
            if (redFlash == 1)
            {
                whatHap = "FLASHING!!!!!!!!!!!!!";
                if (redTimer >= 1)
                {
                    redFlash += 1;
                    redTimer = 0;
                }
            }

            if (redFlash > 1)
            {
                redFlash = 0;
            }

        }

        if (redQuestActive == false)
        {
            redFlash = 0;
            redTimer = 0;
        }
    }
}
