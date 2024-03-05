using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnPoint; // ������, �� �������� ����� ���������� ��� ���������
    public GameObject wood; // ������ ���������
    public GameObject bomb; // ������ �����
    public float distance = 1f; // ���������� ��� ������

    public Timer timer;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // ������ ��� �������� ���������� ��������
    private int side; // �������, � ������� �������� ��������� (0 - �����, 1 - ������)

    public int woodCount = 0;
    public TextMeshProUGUI score;
    public TextMeshProUGUI MaxScore;    

    public UIManager uiManager;
    public ButtonUI buttonUI;

    private int RecordScore;

    [HideInInspector]public bool GameOver = false;

    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Awake()
    {
        woodCount = 0;
        uiManager.UpdateScoreText(woodCount);
    }

    private void Start()
    {
        // ��������� ���������� �� ������
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� ����� ��� ��������
            GetLoad();

            // ���� ������ ��� �� �����������, �� ����� �� ���������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    void Update()
    {
        /*if (spawnedObjects.Count == 0)
        {
            Debug.Log("�������");
            return;
        }*/

        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("���������� �������� � ������: " + spawnedObjects.Count);
            //Debug.Log("�������� side: " + side);
            PickUpObject(0);
            ClearSpawnedObjects();
            Spawn();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("���������� �������� � ������: " + spawnedObjects.Count);
            //Debug.Log("�������� side: " + side);
            PickUpObject(1);
            ClearSpawnedObjects();
            Spawn();
        }
    }

    void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

    public void Spawn()
    {
        // ����������, � ����� ������� �������� ���������
        side = Random.Range(0, 2);
        Vector3 woodPosition = spawnPoint.transform.position + ((side == 0) ? new Vector3(-distance, 0, 0) : new Vector3(distance, 0, 0));
        spawnedObjects.Add(Instantiate(wood, woodPosition, Quaternion.identity));

        // � ������������ 20% �������� ����� � ��������������� �������
        if (Random.value < 0.2f)
        {
            Vector3 bombPosition = spawnPoint.transform.position + ((side == 0) ? new Vector3(distance, 0, 0) : new Vector3(-distance, 0, 0));
            spawnedObjects.Add(Instantiate(bomb, bombPosition, Quaternion.identity));
        }
    }

    void PickUpObject(int pickSide)
    {
        bool isEmpty = true;

        foreach (GameObject obj in spawnedObjects)
        {
            if (pickSide == side && obj.name.Contains(wood.name))
            {
                //Debug.Log("�� ��������� ������");
                isEmpty = false;
                timer.timer += 1f; // add one second
                ScoreIncreasing();

                FindObjectOfType<AudioManager>().Play("WoodOn");

                /*GameObject taggedObject = GameObject.FindWithTag("Wood");
                Animator animator = taggedObject.GetComponent<Animator>();
                animator.SetBool("Click", true);*/

                break;
            }
            else if (pickSide != side && obj.name.Contains(bomb.name))
            {
                Debug.Log("Game Over");
                isEmpty = false;
                timer.timer -= timer.timer; // subtract one second

                FindObjectOfType<AudioManager>().Play("Explosion");

                GameOver = true;

                if (RecordScore < woodCount)
                {
                    // ���������� ������ � ������
                    // ��������, �� ����� ��������� ���������� ����� ������:
                    YandexGame.savesData.woodCount = woodCount;
                    MaxScore.text = woodCount.ToString();
                    RecordScore = woodCount;
                    buttonUI.addition(RecordScore);
                }

                break;
            }
        }

        if (isEmpty)
        {
            //Debug.Log("�������");
        }
    }

    void ScoreIncreasing()
    {
        woodCount++;
        uiManager.UpdateScoreText(woodCount);
    }

    public void GetLoad()
    {
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� � ������ �����:
        MaxScore.text = YandexGame.savesData.woodCount.ToString();
        RecordScore = YandexGame.savesData.woodCount;
        buttonUI.addition(RecordScore);
    }
}
