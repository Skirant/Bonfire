using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [HideInInspector] public float timer;
    [SerializeField] private float timeLimit = 16f;
    [SerializeField] private float acceleration = 0.1f; // ���������� ��� �������� ��������� �������
    [SerializeField] public Slider timerSlider;
    [SerializeField] private ObjectSpawner objectSpawner;

    private void Start()
    {
        timer = timeLimit / 2;
        timerSlider.maxValue = timeLimit;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime * acceleration; // ��������� �������

            if (timer <= 0f)
            {
                timer = 0f;

                objectSpawner.GameOver= true;
            }
            else if (timer >= timeLimit)
            {
                timer = timeLimit;
            }
        }

        UpdateTimerUI();
        acceleration += 0.0001f; // ����������� ���������� ���������
    }

    void UpdateTimerUI()
    {
        timerSlider.value = timer;
    }
}
