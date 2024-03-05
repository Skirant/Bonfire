using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

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
    public TextMeshProUGUI MaxScore;    

    public UIManager uiManager;
    public ButtonUI buttonUI;

    private int RecordScore;

    [HideInInspector]public bool GameOver = false;

    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Awake()
    {
        woodCount = 0;
        uiManager.UpdateScoreText(woodCount);
    }

    private void Start()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то выполняем Ваш метод для загрузки
            GetLoad();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
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

    public void Spawn()
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
                //Debug.Log("Вы подобрали дерево");
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
                    // Записываем данные в плагин
                    // Например, мы хотил сохранить количество монет игрока:
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
            //Debug.Log("Пропуск");
        }
    }

    void ScoreIncreasing()
    {
        woodCount++;
        uiManager.UpdateScoreText(woodCount);
    }

    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        MaxScore.text = YandexGame.savesData.woodCount.ToString();
        RecordScore = YandexGame.savesData.woodCount;
        buttonUI.addition(RecordScore);
    }
}
