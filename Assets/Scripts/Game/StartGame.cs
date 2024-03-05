using UnityEngine;
using YG;

public class StartGame : MonoBehaviour
{
    [HideInInspector] public bool isGameStarted = false;
    [HideInInspector] public bool gameOverProcessed = false;

    public PlayerController playerController;
    public ObjectSpawner objectSpawner;
    public Timer timer;
    public GameOver gameOver;

    public Canvas StartCanvas;

    private float fullscreenShowTimer = 0;

    //public GameObject timerSlider;    

    public GameObject BlackWindows;

    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        // ��������� ���������� �� ������
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� �����
            GetData();
            // ���� ������ ��� �� �����������, �� ����� �� ����������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    void Start()
    {
        // ����� �� ������������� ���� � ������
        // Time.timeScale = 0;
        playerController.enabled = false;
        objectSpawner.enabled = false;
        timer.enabled = false;
        // timerSlider.SetActive(false);

        YandexGame.FullscreenShow();
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            // ����� �� ������������ ���� ����� ������� ������� �������
            //  Time.timeScale = 1;
            if (Time.timeScale >= 1)
            {
                playerController.enabled = true;
                objectSpawner.enabled = true;
                timer.enabled = true;
            }               

            // timerSlider.SetActive(true);

            StartCanvas.enabled = false;

            objectSpawner.Spawn();

            isGameStarted = true;
        }

        if (!isGameStarted)
        {
            SnowADD();
        }

        if (objectSpawner.GameOver && !gameOverProcessed)
        {
            gameOver.Realization();
        }
    }

    void SnowADD()
    {
        if (Time.realtimeSinceStartup - fullscreenShowTimer >= 20)
        {
            YandexGame.FullscreenShow();
            fullscreenShowTimer = Time.realtimeSinceStartup; // ���������� ������
        }
    }



    public void GetData()
    {
        BlackWindows.SetActive(false);
    }
}
