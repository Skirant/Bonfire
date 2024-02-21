using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool isGameStarted = false;

    public PlayerController playerController;
    public ObjectSpawner objectSpawner;
    public Timer timer;

    public Canvas StartCanvas;

    //public GameObject timerSlider;    

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
            isGameStarted = true;
            playerController.enabled = true;
            objectSpawner.enabled = true;
            timer.enabled = true;

           // timerSlider.SetActive(true);

            StartCanvas.enabled = false;           
        }
    }   
}
