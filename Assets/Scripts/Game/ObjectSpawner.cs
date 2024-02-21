using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnPoint; // Объект, от которого будут спавниться все остальные
    public GameObject wood; // Объект древесины
    public GameObject bomb; // Объект бомбы
    public float distance = 1f; // Расстояние для спавна

    public Timer timer;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Список для хранения спавненных объектов
    private int side; // Сторона, с которой появится древесина (0 - слева, 1 - справа)

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
            Debug.Log("Пропуск");
            return;
        }*/

        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("Количество объектов в списке: " + spawnedObjects.Count);
            //Debug.Log("Значение side: " + side);
            PickUpObject(0);
            ClearSpawnedObjects();
            Spawn();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("Количество объектов в списке: " + spawnedObjects.Count);
            //Debug.Log("Значение side: " + side);
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
        // Определите, с какой стороны появится древесина
        side = Random.Range(0, 2);
        Vector3 woodPosition = spawnPoint.transform.position + ((side == 0) ? new Vector3(-distance, 0, 0) : new Vector3(distance, 0, 0));
        spawnedObjects.Add(Instantiate(wood, woodPosition, Quaternion.identity));

        // С вероятностью 20% создайте бомбу с противоположной стороны
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
                Debug.Log("Вы подобрали дерево");
                isEmpty = false;
                timer.timer += 1f; // add one second
                ScoreIncreasing();
                break;
            }
            else if (pickSide != side && obj.name.Contains(bomb.name))
            {
                Debug.Log("Взрыв");
                isEmpty = false;
                timer.timer -= 1f; // subtract one second
                break;
            }
        }

        if (isEmpty)
        {
            Debug.Log("Пропуск");
        }
    }

    void ScoreIncreasing()
    {
        woodCount++;
        score.text = woodCount.ToString();
    }
}
