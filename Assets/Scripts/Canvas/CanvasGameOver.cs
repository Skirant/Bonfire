using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameOver : MonoBehaviour
{    public void OnClick()
    {
        SceneManager.LoadScene("Game"); // Или замените "YourSceneName" на индекс сцены
    }
}
