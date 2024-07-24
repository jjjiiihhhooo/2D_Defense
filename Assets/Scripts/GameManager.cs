using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private JoystickManager joystickManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Camera cam;

    public GameObject player_obj;

    private string filePath;

    public JoystickManager JoystickManager { get => joystickManager;}
    public StageManager StageManager { get => stageManager;}
    public DataManager DataManager { get => dataManager; }
    public UIManager UIManager { get => uiManager; }
    public ObjectPool ObjectPool { get => objectPool;  }

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

    private void Update()
    {
        if (cam == null) { cam = Camera.main; CameraResolution(); Debug.Log("CamSet"); }
    }

    private void Init()
    {
        CameraResolution();

        filePath = Application.persistentDataPath + "/DataText.txt";

        LoadData();
        joystickManager.Init();
    }

    private void CameraResolution()
    {
        Rect rect = cam.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9); //(가로 / 세로)
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        cam.rect = rect;
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
        if(!File.Exists(filePath)) { dataManager.DefaultData(); SaveData(); }

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
}
