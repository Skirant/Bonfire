using UnityEngine;
using YG;

public class StartGame : MonoBehaviour
{
    [HideInInspector] public bool isGameStarted = false;

    public PlayerController playerController;
    public ObjectSpawner objectSpawner;
    public Timer timer;

    public Canvas StartCanvas;
    public Canvas GameOverCanvas;
    public GameObject Timer;

    private bool gameOverProcessed = false;

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
        Time.timeScale = 0;
        playerController.enabled = false;
        objectSpawner.enabled = false;
        timer.enabled = false;
        // timerSlider.SetActive(false);
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            // ����� �� ������������ ���� ����� ������� ������� �������
            Time.timeScale = 1;
            playerController.enabled = true;
            objectSpawner.enabled = true;
            timer.enabled = true;

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
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("+");

        playerController.enabled = false;
        objectSpawner.enabled = false;
        Timer.SetActive(false);

        GameOverCanvas.enabled = true;

        gameOverProcessed = true;

        GameObject taggedObject = GameObject.FindWithTag("Canvas");
        Animator animator = taggedObject.GetComponent<Animator>();
        animator.SetTrigger("GameOver");

        MySave();
    }

    void SnowADD()
    {
        if (Time.realtimeSinceStartup - fullscreenShowTimer >= 20)
        {
            YandexGame.FullscreenShow();
            fullscreenShowTimer = Time.realtimeSinceStartup; // ���������� ������
        }
    }

    public void MySave()
    {
        // ������ ������� ��������� ������
        YandexGame.SaveProgress();
    }

    public void GetData()
    {
        BlackWindows.SetActive(false);
    }
}
