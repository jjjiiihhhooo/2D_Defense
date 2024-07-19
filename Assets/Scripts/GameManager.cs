using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private JoystickManager joystickManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private UIManager uiManager;

    public JoystickManager JoystickManager { get => joystickManager;}
    public StageManager StageManager { get => stageManager;}
    public DataManager DataManager { get => dataManager; }
    public UIManager UIManager { get => uiManager; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            FrameSetting();
            Instance = this;

            joystickManager.Init();
            stageManager.Init();

            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void FrameSetting()
    {
        Application.targetFrameRate = -1;
        Application.targetFrameRate = 60;
    }
}
