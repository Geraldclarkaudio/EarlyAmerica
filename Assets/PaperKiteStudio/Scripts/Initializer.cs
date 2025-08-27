using System.Collections;
using UnityEngine;
using System.IO;
using LoLSDK;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using PaperKiteStudio.Dangers;

[System.Serializable]
public class PlayerData // what do we want to save?
{
    public int gamePhase; // main progression phase. Phase 1 = level 1 Phase 2  = level 2 etc etc 
    public int phaseStep; // sub phase for main progression.. example : GamePhase 1 PhaseStep 4
}

public class Initializer : MonoBehaviour
{
    private static Initializer _instance;
    public static Initializer Instance
    {
        get
        {
            if (_instance == null)
            {

            }
            return _instance;
        }
    }
    public bool _init = false;
    WaitForSeconds _feedbackTimer = new WaitForSeconds(2);
    Coroutine _feedbackMethod;
    [SerializeField, Header("State Data")]
    public PlayerData playerData;
    JSONNode _langNode;
    string _langCode = "en";
    [SerializeField] Button continueButton, newGameButton;
    [SerializeField] TextMeshProUGUI newGameText, continueText;
    public int _dataCounter = 0;
    int _totalDataCount = 2;

    [SerializeField]
    private GamePhaseManager _gamePhaseManager;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }


#if UNITY_EDITOR
        ILOLSDK sdk = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
		    ILOLSDK sdk = new LoLSDK.WebGL();
#elif UNITY_IOS || UNITY_ANDROID
            ILOLSDK sdk = null; // TODO COMING SOON IN V6
#endif
        LOLSDK.Init(sdk, "com.PaperKiteStudio.EcoQuest");

        LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(StartGame);
        LOLSDK.Instance.GameStateChanged += new GameStateChangedHandler(gameState => Debug.Log(gameState));
        LOLSDK.Instance.QuestionsReceived += new QuestionListReceivedHandler(questionList => Debug.Log(questionList));
        LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(LanguageUpdate);
        LOLSDK.Instance.SaveResultReceived += OnSaveResult;
        LOLSDK.Instance.GameIsReady();

#if UNITY_EDITOR
        UnityEditor.EditorGUIUtility.PingObject(this);
        LoadMockData();
#endif
    }

    private void Start()
    {
        StartCoroutine(WaitToLoad());
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitUntil(() => _dataCounter >= _totalDataCount);
        HelperPKS.StateButtonInitialize<PlayerData>(newGameButton, continueButton, onload);
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            return;
#endif
        LOLSDK.Instance.SaveResultReceived -= OnSaveResult;
    }

    public void Save()
    {
        LOLSDK.Instance.SaveState(playerData);
    }

    void OnSaveResult(bool success)
    {
        if (!success)
        {
            return;
        }
    }


    void StartGame(string startGameJSON)
    {
        if (string.IsNullOrEmpty(startGameJSON))
            return;

        JSONNode startGamePayload = JSON.Parse(startGameJSON);
        // Debug.Log("StartGame()Called");

        // Capture the language code from the start payload. Use this to switch fonts
        _langCode = startGamePayload["languageCode"];

        _dataCounter++;
    }

    public void LanguageUpdate(string langJSON)
    {
        if (string.IsNullOrEmpty(langJSON))
            return;

        _langNode = JSON.Parse(langJSON);

        TextDisplayUpdate();
        _dataCounter++;
    }

    public string GetText(string key)
    {
        string value = _langNode?[key];
        return value ?? "--missing--";
    }

    void TextDisplayUpdate()
    {
        newGameText.text = GetText("newGame");
        continueText.text = GetText("continue");
    }

    public void onload(PlayerData loadedPlayerData)
    {
        //// Overrides serialized state data or continues with editor serialized values.
        if (loadedPlayerData != null)
        {
            playerData = loadedPlayerData;
            _gamePhaseManager._gamePhase = loadedPlayerData.gamePhase;
            _gamePhaseManager._phaseStep = loadedPlayerData.phaseStep;


            //app submission of progress...
            //if (loadedPlayerData.something to save)
            //{
            //    LOLSDK.Instance.SubmitProgress(1, 1, 9); submit progress to teacher app. 
            //}

            Save();
        }

        _init = true;



    }
#if UNITY_EDITOR
    private void LoadMockData()
    {
        // Load Dev Language File from StreamingAssets

        string startDataFilePath = Path.Combine(Application.streamingAssetsPath, "startGame.json");

        if (File.Exists(startDataFilePath))
        {
            string startDataAsJSON = File.ReadAllText(startDataFilePath);
            StartGame(startDataAsJSON);
        }

        // Load Dev Language File from StreamingAssets
        string langFilePath = Path.Combine(Application.streamingAssetsPath, "language.json");
        if (File.Exists(langFilePath))
        {
            string langDataAsJson = File.ReadAllText(langFilePath);
            var lang = JSON.Parse(langDataAsJson)[_langCode];
            LanguageUpdate(lang.ToString());
        }
    }
#endif


    public void CompletedGame()
    {
        LOLSDK.Instance.CompleteGame();
    }
}