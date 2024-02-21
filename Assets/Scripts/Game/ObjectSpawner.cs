using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    void Awake()
    {
        woodCount = 0;   
        score.text = woodCount.ToString();

        Spawn();
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

    void Spawn()
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
                Debug.Log("�� ��������� ������");
                isEmpty = false;
                timer.timer += 1f; // add one second
                ScoreIncreasing();
                break;
            }
            else if (pickSide != side && obj.name.Contains(bomb.name))
            {
                Debug.Log("�����");
                isEmpty = false;
                timer.timer -= 1f; // subtract one second
                break;
            }
        }

        if (isEmpty)
        {
            Debug.Log("�������");
        }
    }

    void ScoreIncreasing()
    {
        woodCount++;
        score.text = woodCount.ToString();
    }
}
