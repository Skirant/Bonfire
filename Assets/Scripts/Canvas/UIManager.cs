using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText1; // ������ ��������� ���������
    public TextMeshProUGUI scoreText2; // ������ ��������� ���������

    public void UpdateScoreText(int newScore)
    {
        string newScoreText = newScore.ToString();


        scoreText1.text = newScoreText;
        scoreText2.text = newScoreText;
    }
}
