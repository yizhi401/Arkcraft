using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CubeWorld.Gameplay;
using CubeWorld.Configuration;

public class MainMenu
{
    private GameManagerUnity gameManagerUnity;

    private GameObject gameCanvas;
    private GameObject mainMenu;
    private GameObject pauseMenu;
    private GameObject aboutMenu;
    private GameObject generateMenu;
    private GameObject optionsMenu;
    private GameObject loadSaveMenu;

    public MainMenu(GameManagerUnity gameManagerUnity)
    {
        this.gameManagerUnity = gameManagerUnity;
        gameCanvas = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/GameCanvas"));
        gameCanvas.transform.SetParent(gameManagerUnity.gameObject.transform.parent);

        mainMenu = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/MainMenu"));
        mainMenu.transform.SetParent(gameCanvas.transform);
        mainMenu.transform.localPosition = Vector3.zero;

        pauseMenu = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/PauseMenu"));
        pauseMenu.transform.SetParent(gameCanvas.transform);
        pauseMenu.transform.localPosition = Vector3.zero;


        generateMenu= Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/GeneratorMenu"));
        generateMenu.transform.SetParent(gameCanvas.transform);
        generateMenu.transform.localPosition = Vector3.zero;


        optionsMenu = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/OptionsMenu"));
        optionsMenu.transform.SetParent(gameCanvas.transform);
        optionsMenu.transform.localPosition = Vector3.zero;


        loadSaveMenu= Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/LoadSaveMenu"));
        loadSaveMenu.transform.SetParent(gameCanvas.transform);
        loadSaveMenu.transform.localPosition = Vector3.zero;


        aboutMenu = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Menus/AboutMenu"));
        aboutMenu.transform.SetParent(gameCanvas.transform);
        aboutMenu.transform.localPosition = Vector3.zero;

        DeactivateAll();

    }
    public void ShowMenu(bool isPause)
    {
        gameCanvas.SetActive(true);
        if (isPause)
        {
            DrawMenuPause();
        }else
        {
            Draw();
        }
    }

    public void HideMenu()
    {
        gameCanvas.SetActive(false);
    }

    public enum MainMenuState
    {
        NORMAL,
        GENERATOR,
        OPTIONS,
        //JOIN_MULTIPLAYER,
        LOAD,
        SAVE,
        ABOUT
    }

    public MainMenuState state = MainMenuState.NORMAL;

    public void Draw()
    {
        MenuSystem.useKeyboard = false;

        switch (state)
        {
            case MainMenuState.NORMAL:
                DrawMenuNormal();
                break;

            case MainMenuState.GENERATOR:
                DrawGenerator();
                break;

            case MainMenuState.OPTIONS:
                DrawOptions();
                break;

            case MainMenuState.ABOUT:
                DrawMenuAbout();
                break;

            //case MainMenuState.JOIN_MULTIPLAYER:
            //    DrawJoinMultiplayer();
            //    break;

            case MainMenuState.LOAD:
                DrawMenuLoadSave(true);
                break;

            case MainMenuState.SAVE:
                DrawMenuLoadSave(false);
                break;
        }
    }

    private void DeactivateAll()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        aboutMenu.SetActive(false);
        generateMenu.SetActive(false);
        optionsMenu.SetActive(false);
        loadSaveMenu.SetActive(false);
    }

    public void DrawPause()
    {

        MenuSystem.useKeyboard = false;

        switch (state)
        {
            case MainMenuState.NORMAL:
                DrawMenuPause();
                break;

            case MainMenuState.OPTIONS:
                DrawOptions();
                break;

            case MainMenuState.LOAD:
                DrawMenuLoadSave(true);
                break;

            case MainMenuState.SAVE:
                DrawMenuLoadSave(false);
                break;
        }
    }


    void DrawMenuLoadSave(bool load)
    {
        DeactivateAll();
        loadSaveMenu.SetActive(true);

        if (load)
        {
            loadSaveMenu.transform.FindChild("Title").GetComponent<Text>().text = "Load Menu";
        }else
        {
            loadSaveMenu.transform.FindChild("Title").GetComponent<Text>().text = "Save Menu";
        }

        Button[] btns = loadSaveMenu.GetComponentsInChildren<Button>();

        for (int i = 0; i < 5; i++)
        {
            System.DateTime fileDateTime = WorldManagerUnity.GetWorldFileInfo(i);

            if (fileDateTime != System.DateTime.MinValue)
            {
                string prefix;
                if (load)
                    prefix = "Load World ";
                else
                    prefix = "Overwrite World";

                btns[i].GetComponentInChildren<Text>().text = prefix + (i + 1).ToString() + " [ " + fileDateTime.ToString() + " ]";
                btns[i].onClick.RemoveAllListeners();
                btns[i].onClick.AddListener(delegate()
                {
                    if (load)
                    {
                        gameManagerUnity.worldManagerUnity.LoadWorld(i);
                        state = MainMenuState.NORMAL;
                    }
                    else
                    {
                        gameManagerUnity.worldManagerUnity.SaveWorld(i);
                        state = MainMenuState.NORMAL;
                    }
                }
                );
            }
            else
            {
                btns[i].GetComponentInChildren<Text>().text = "-- Empty Slot --";
                btns[i].onClick.RemoveAllListeners();
                btns[i].onClick.AddListener(delegate ()
                {
                    if (load == false)
                    {
                        gameManagerUnity.worldManagerUnity.SaveWorld(i);
                        state = MainMenuState.NORMAL;
                        Draw();
                    }
                }
                );
            }
        }
        btns[5].GetComponentInChildren<Text>().text = "Back";
        btns[5].onClick.RemoveAllListeners();
        btns[5].onClick.AddListener(delegate()
        {
            state = MainMenuState.NORMAL;
            Draw();
        }
        );
    }

    private CubeWorld.Configuration.Config lastConfig; 

    void DrawMenuPause()
    {
        DeactivateAll();
        pauseMenu.SetActive(true);

        if (lastConfig != null)
        {
            pauseMenu.transform.FindChild("Recreate").gameObject.SetActive(true);
        }else
        {
            pauseMenu.transform.FindChild("Recreate").gameObject.SetActive(false);
        }

        Button b = pauseMenu.transform.FindChild("Recreate").
            GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(delegate (){
                gameManagerUnity.worldManagerUnity.CreateRandomWorld(lastConfig);
            });

        b = pauseMenu.transform.FindChild("Save").
            GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(delegate (){
                state = MainMenuState.SAVE;
                Draw();
            });

        b = pauseMenu.transform.FindChild("Options").
            GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(delegate (){
                state = MainMenuState.OPTIONS;
                Draw();
            });

        b = pauseMenu.transform.FindChild("Options").
            GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(delegate (){
                gameManagerUnity.ReturnToMainMenu();
            });

        b = pauseMenu.transform.FindChild("Options").
            GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(delegate (){
                gameManagerUnity.Unpause();
            });
    }

    void DrawMenuNormal()
    {
        DeactivateAll();
        mainMenu.SetActive(true);

        Button createBtn = mainMenu.transform.FindChild("Create").GetComponent<Button>();
        createBtn.onClick.RemoveAllListeners();
        createBtn.onClick.AddListener(delegate ()
        {
            state = MainMenuState.GENERATOR;
            Draw();
        });

        Button loadBtn = mainMenu.transform.FindChild("Load").GetComponent<Button>();
        loadBtn.onClick.RemoveAllListeners();
        loadBtn.onClick.AddListener(delegate()
        {
            state = MainMenuState.LOAD;
            Draw();
        });

        Button optionBtn = mainMenu.transform.FindChild("Options").GetComponent<Button>();
        optionBtn.onClick.RemoveAllListeners();
        optionBtn.onClick.AddListener(delegate() 
        {
            state = MainMenuState.OPTIONS;
            Draw();
        });

        Button aboutBtn = mainMenu.transform.FindChild("About").GetComponent<Button>();
        aboutBtn.onClick.RemoveAllListeners();
        aboutBtn.onClick.AddListener(delegate() 
        {
            state = MainMenuState.ABOUT;
            Draw();
        });

        Button quitBtn = mainMenu.transform.FindChild("Quit").GetComponent<Button>();
        quitBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(delegate() 
        {
            Debug.Log("Quit Game");
            Application.Quit();
        });

    }

    public const string CubeworldWebServerServerList = "http://cubeworldweb.appspot.com/list";
    public const string CubeworldWebServerServerRegister = "http://cubeworldweb.appspot.com/register?owner={owner}&description={description}&port={port}";

    private WWW wwwRequest;
    private string[] servers;

    /**
    void DrawJoinMultiplayer()
    {
        MenuSystem.BeginMenu("Join Multiplayer");

        if (wwwRequest == null && servers == null)
            wwwRequest = new WWW(CubeworldWebServerServerList);

        if (servers == null && wwwRequest != null && wwwRequest.isDone)
            servers = wwwRequest.text.Split(';');

        if (wwwRequest != null && wwwRequest.isDone)
        {
            foreach (string s in servers)
            {
                string[] ss = s.Split(',');

                if (ss.Length >= 2)
                {
                    MenuSystem.Button("Join [" + ss[0] + ":" + ss[1] + "]", delegate()
                    {
                        gameManagerUnity.worldManagerUnity.JoinMultiplayerGame(ss[0], System.Int32.Parse(ss[1]));

                        availableConfigurations = null;

                        wwwRequest = null;
                        servers = null;

                        state = MainMenuState.NORMAL;
                    }
                    );
                }
            }

            MenuSystem.Button("Refresh List", delegate()
            {
                wwwRequest = null;
                servers = null;
            }
            );
        }
        else
        {
            MenuSystem.TextField("Waiting data from server..");
        }

        MenuSystem.LastButton("Back", delegate()
        {
            wwwRequest = null;
            servers = null;
            state = MainMenuState.NORMAL;
        });

        MenuSystem.EndMenu();
    }
    **/

    void DrawOptions()
    {
        DeactivateAll();
        optionsMenu.SetActive(true);

        Button distance = optionsMenu.transform.FindChild("Distance").GetComponent<Button>();
        distance.GetComponentInChildren<Text>().text = "Draw Distance: " + CubeWorldPlayerPreferences.farClipPlanes[CubeWorldPlayerPreferences.viewDistance];
        distance.onClick.RemoveAllListeners();
        distance.onClick.AddListener(delegate()
        {
            CubeWorldPlayerPreferences.viewDistance = (CubeWorldPlayerPreferences.viewDistance + 1) % CubeWorldPlayerPreferences.farClipPlanes.Length;

            if (gameManagerUnity.playerUnity)
                gameManagerUnity.playerUnity.mainCamera.farClipPlane = CubeWorldPlayerPreferences.farClipPlanes[CubeWorldPlayerPreferences.viewDistance];
            
            distance.GetComponentInChildren<Text>().text = "Draw Distance: " + CubeWorldPlayerPreferences.farClipPlanes[CubeWorldPlayerPreferences.viewDistance];
        });

        Button help = optionsMenu.transform.FindChild("Help").GetComponent<Button>();
        help.onClick.RemoveAllListeners();
        help.GetComponentInChildren<Text>().text = "Show Help: " + CubeWorldPlayerPreferences.showHelp;
        help.onClick.AddListener(delegate()
        {
            CubeWorldPlayerPreferences.showHelp = !CubeWorldPlayerPreferences.showHelp;
            help.GetComponentInChildren<Text>().text = "Show Help: " + CubeWorldPlayerPreferences.showHelp;
        });

        Button fps = optionsMenu.transform.FindChild("FPS").GetComponent<Button>();
        fps.GetComponentInChildren<Text>().text = "Show FPS: " + CubeWorldPlayerPreferences.showFPS;
        fps.onClick.RemoveAllListeners();
        fps.onClick.AddListener(delegate()
        {
            CubeWorldPlayerPreferences.showFPS = !CubeWorldPlayerPreferences.showFPS;
            fps.GetComponentInChildren<Text>().text = "Show FPS: " + CubeWorldPlayerPreferences.showFPS;
 
        });

        Button engineState = optionsMenu.transform.FindChild("EngineState").GetComponent<Button>();
        engineState.GetComponentInChildren<Text>().text = "Show Engine Stats: " + CubeWorldPlayerPreferences.showEngineStats;
        engineState.onClick.RemoveAllListeners();
        engineState.onClick.AddListener(delegate()
        {
            CubeWorldPlayerPreferences.showEngineStats = !CubeWorldPlayerPreferences.showEngineStats;
            engineState.GetComponentInChildren<Text>().text = "Show Engine Stats: " + CubeWorldPlayerPreferences.showEngineStats;
        });

        Button strategy = optionsMenu.transform.FindChild("Strategy").GetComponent<Button>();
        strategy.GetComponentInChildren<Text>().text = "Visible Strategy: " + System.Enum.GetName(typeof(SectorManagerUnity.VisibleStrategy), CubeWorldPlayerPreferences.visibleStrategy);
        strategy.onClick.RemoveAllListeners();
        strategy.onClick.AddListener(delegate()
        {
            if (System.Enum.IsDefined(typeof(SectorManagerUnity.VisibleStrategy), (int)CubeWorldPlayerPreferences.visibleStrategy + 1))
            {
                CubeWorldPlayerPreferences.visibleStrategy = CubeWorldPlayerPreferences.visibleStrategy + 1;
            }
            else
            {
                CubeWorldPlayerPreferences.visibleStrategy = 0;
            }
            strategy.GetComponentInChildren<Text>().text = "Visible Strategy: " + System.Enum.GetName(typeof(SectorManagerUnity.VisibleStrategy), CubeWorldPlayerPreferences.visibleStrategy);
        });

        Button back = optionsMenu.transform.FindChild("Back").GetComponent<Button>();
        back.onClick.RemoveAllListeners();
        back.onClick.AddListener(delegate()
        {
            CubeWorldPlayerPreferences.StorePreferences();

            gameManagerUnity.PreferencesUpdated();

            state = MainMenuState.NORMAL;
            Draw();
        });

    }

    private AvailableConfigurations availableConfigurations;
    private int currentSizeOffset = 0;
    private int currentGeneratorOffset = 0;
    private int currentDayInfoOffset = 0;
    private int currentGameplayOffset = 0;
    //private bool multiplayer = false;

    void DrawGenerator()
    {
        DeactivateAll();
        generateMenu.SetActive(true);
        if (availableConfigurations == null)
        {
            availableConfigurations = GameManagerUnity.LoadConfiguration();
            currentDayInfoOffset = 0;
            currentGeneratorOffset = 0;
            currentSizeOffset = 0;
            currentGameplayOffset = 0;
        }

        Button gamePlayBtn = generateMenu.transform.FindChild("GamePlay").GetComponent<Button>();
        gamePlayBtn.GetComponentInChildren<Text>().text = "Gameplay: " + GameplayFactory.AvailableGameplays[currentGameplayOffset].name;
        gamePlayBtn.onClick.RemoveAllListeners();
        gamePlayBtn.onClick.AddListener(delegate()
        {
            currentGameplayOffset = (currentGameplayOffset + 1) % GameplayFactory.AvailableGameplays.Length;
            gamePlayBtn.GetComponentInChildren<Text>().text = "Gameplay: " + GameplayFactory.AvailableGameplays[currentGameplayOffset].name;
        });

        Button worldSize = generateMenu.transform.FindChild("WorldSize").GetComponent<Button>();
        worldSize.GetComponentInChildren<Text>().text = "World Size: " + availableConfigurations.worldSizes[currentSizeOffset].name;
        worldSize.onClick.RemoveAllListeners();
        worldSize.onClick.AddListener(delegate()
        {
            currentSizeOffset = (currentSizeOffset + 1) % availableConfigurations.worldSizes.Length;
            worldSize.GetComponentInChildren<Text>().text = "World Size: " + availableConfigurations.worldSizes[currentSizeOffset].name;
        });

        Button dayLen = generateMenu.transform.FindChild("DayLength").GetComponent<Button>();
        dayLen.GetComponentInChildren<Text>().text = "Day Length: " + availableConfigurations.dayInfos[currentDayInfoOffset].name;
        dayLen.onClick.RemoveAllListeners();
        dayLen.onClick.AddListener(delegate()
        {
            currentDayInfoOffset = (currentDayInfoOffset + 1) % availableConfigurations.dayInfos.Length;
            dayLen.GetComponentInChildren<Text>().text = "Day Length: " + availableConfigurations.dayInfos[currentDayInfoOffset].name;
        });

        Button generatorBtn = generateMenu.transform.FindChild("Generator").GetComponent<Button>();
        generatorBtn.GetComponentInChildren<Text>().text = "Generator: " + availableConfigurations.worldGenerators[currentGeneratorOffset].name;
        generatorBtn.onClick.RemoveAllListeners();
        generatorBtn.onClick.AddListener(delegate()
        {
            currentGeneratorOffset = (currentGeneratorOffset + 1) % availableConfigurations.worldGenerators.Length;
            generatorBtn.GetComponentInChildren<Text>().text = "Generator: " + availableConfigurations.worldGenerators[currentGeneratorOffset].name;
        });
        GameObject generator = generateMenu.transform.FindChild("Generator").gameObject;
        if (GameplayFactory.AvailableGameplays[currentGameplayOffset].hasCustomGenerator == false)
        {
            generator.SetActive(true);
        }else
        {
            generator.SetActive(false);
        }

        //MenuSystem.Button("Host Multiplayer: " + (multiplayer ? "Yes" : "No") , delegate()
        //{
        //    multiplayer = !multiplayer;
        //}
        //);

        Button goBtn = generateMenu.transform.FindChild("Generate").GetComponent<Button>();
        goBtn.GetComponentInChildren<Text>().text = "Generate!";
        goBtn.onClick.RemoveAllListeners();
        goBtn.onClick.AddListener(delegate ()
        {
            lastConfig = new CubeWorld.Configuration.Config();
            lastConfig.tileDefinitions = availableConfigurations.tileDefinitions;
            lastConfig.itemDefinitions = availableConfigurations.itemDefinitions;
            lastConfig.avatarDefinitions = availableConfigurations.avatarDefinitions;
            lastConfig.dayInfo = availableConfigurations.dayInfos[currentDayInfoOffset];
            lastConfig.worldGenerator = availableConfigurations.worldGenerators[currentGeneratorOffset];
            lastConfig.worldSize = availableConfigurations.worldSizes[currentSizeOffset];
            lastConfig.extraMaterials = availableConfigurations.extraMaterials;
            lastConfig.gameplay = GameplayFactory.AvailableGameplays[currentGameplayOffset];

            //if (multiplayer)
            //{
            //    MultiplayerServerGameplay multiplayerServerGameplay = new MultiplayerServerGameplay(lastConfig.gameplay.gameplay, true);

            //    GameplayDefinition g = new GameplayDefinition("", "", multiplayerServerGameplay, false);

            //    lastConfig.gameplay = g;

            //    gameManagerUnity.RegisterInWebServer();
            //}

            gameManagerUnity.worldManagerUnity.CreateRandomWorld(lastConfig);

            availableConfigurations = null;

            state = MainMenuState.NORMAL;
            //Draw();
            HideMenu();
        });
        Button backBtn = generateMenu.transform.FindChild("Back").GetComponent<Button>();
        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(delegate () {
            state = MainMenuState.NORMAL;
            Draw();
        });

    }

    void DrawMenuAbout()
    {
        DeactivateAll();
        aboutMenu.SetActive(true);

        Button backBtn = aboutMenu.transform.FindChild("Back").GetComponent<Button>();
        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(delegate() 
        {
            state = MainMenuState.NORMAL;
            Draw();
        });
    }

    public void DrawGeneratingProgress(string description, int progress)
    {
        Rect sbPosition = new Rect(40,
                                    Screen.height / 2 - 20,
                                    Screen.width - 80,
                                    40);

        GUI.HorizontalScrollbar(sbPosition, 0, progress, 0, 100);

        Rect dPosition = new Rect(Screen.width / 2 - 200, sbPosition.yMax + 10, 400, 25);
        GUI.Box(dPosition, description);
    }
}
