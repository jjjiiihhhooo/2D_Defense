using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private JoystickManager joystickManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Camera cam;

    public GameObject waitRoom_obj;
    public GameObject player_obj;

    private string filePath;

    public JoystickManager JoystickManager { get => joystickManager; }
    public StageManager StageManager { get => stageManager; }
    public DataManager DataManager { get => dataManager; }
    public UIManager UIManager { get => uiManager; }
    public SoundManager SoundManager { get => soundManager; }
    public ObjectPool ObjectPool { get => objectPool; }

    #region GameSetting
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            FrameSetting();
            Instance = this;

            Init();

            DontDestroyOnLoad(this.gameObject);
        }

    }
    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main") waitRoom_obj.SetActive(true);

        if (soundManager.audioDictionary.ContainsKey(scene.name))
        {
            soundManager.Play(scene.name, true);
        }
    }

    private void Init()
    {
        filePath = Application.persistentDataPath + "/DataText.txt";

        LoadData();
    }



    private void FrameSetting()
    {
        Application.targetFrameRate = -1;
        Application.targetFrameRate = 60;
    }

    #endregion

    private void GameExit()
    {
        SaveData();
        Application.Quit();
    }

    private void SaveData()
    {
        string jsonData = JsonUtility.ToJson(dataManager.data);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(filePath, code);

    }

    private void LoadData()
    {
        Debug.Log(filePath);
        if (!File.Exists(filePath)) { dataManager.DefaultData(); SaveData(); }

        string code = File.ReadAllText(filePath);
        byte[] bytes = System.Convert.FromBase64String(code);
        string jsonData = System.Text.Encoding.UTF8.GetString(bytes);

        Data data = JsonUtility.FromJson<Data>(jsonData);

        dataManager.SetData(data);

    }

    public void SaveMessage()
    {
        SaveData();
    }

    public void LoadMessage()
    {
        LoadData();
    }

    public void TrueStage()
    {
        stageManager.gameObject.SetActive(true);
    }

    public void GetStageManager(StageManager stage)
    {
        stageManager = stage;
    }

    public void GameStart()
    {
        string name = uiManager.StageCountText.text;
        waitRoom_obj.SetActive(false);

        soundManager.StopBGM();

        LoadingSceneManager.LoadScene(name);
    }
}
