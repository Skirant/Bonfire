using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText1; // Первый текстовый компонент
    public TextMeshProUGUI scoreText2; // Второй текстовый компонент

    public void UpdateScoreText(int newScore)
    {
        string newScoreText = newScore.ToString();


        scoreText1.text = newScoreText;
        scoreText2.text = newScoreText;
    }
}
