using UnityEngine;
using YG;

public class GameOver : MonoBehaviour
{
    public PlayerController playerController;
    public ObjectSpawner objectSpawner;
    public StartGame startGamel;

    public GameObject Timer;

    public Canvas GameOverCanvas;

    public void Realization()
    {
        Debug.Log("+");

        playerController.enabled = false;
        objectSpawner.enabled = false;
        Timer.SetActive(false);

        GameOverCanvas.enabled = true;

        startGamel.gameOverProcessed = true;

        GameObject taggedObject = GameObject.FindWithTag("Canvas");
        Animator animator = taggedObject.GetComponent<Animator>();
        animator.SetTrigger("GameOver");

        MySave();
    }

    public void MySave()
    {
        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }
}
