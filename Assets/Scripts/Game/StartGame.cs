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

    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то запускаем Ваш метод
            GetData();

            // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    void Start()
    {
        // Здесь мы останавливаем игру в начале
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
            // Здесь мы возобновляем игру после первого нажатия пробела
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
            fullscreenShowTimer = Time.realtimeSinceStartup; // Сбрасываем таймер
        }
    }

    public void MySave()
    {
        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }

    public void GetData()
    {
        BlackWindows.SetActive(false);
    }
}
