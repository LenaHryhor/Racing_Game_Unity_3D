using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Road : MonoBehaviour
{
    public List<GameObject> blocks; //Коллекция всех дорожных блоков
    public List<GameObject> cars; //Коллекция всех машин
    
    public GameObject player; //Игрок
    public GameObject roadPrefab; //Префаб дорожного блока
    public GameObject carPrefab; //Префаб машины
    public GameObject coinPrefab; //Префаб монеты

    private System.Random rand = new System.Random(); //Генератор случайных чисел

    void Update()
    {
        float x = player.GetComponent<Moving>().rb.position.x; //Получение положения игрока

        var blockLength = 119f; //24.69f;
        var last = blocks[blocks.Count - 1]; //Номер дорожного блока, который дальше всех от игрока

        if (x > last.transform.position.x - blockLength * 10f) //Если игрок подъехал к последнему блоку ближе, чем на 10 блоков
        {
            //Инстанцирование нового блока
            var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x + blockLength, last.transform.position.y, last.transform.position.z), Quaternion.identity);
            block.transform.SetParent(gameObject.transform); //Перемещение блока в объект Road
            blocks.Add(block); 

        }

        float side = rand.Next(1, 3) == 1 ? -1f : 1f; //Случайное определение стороны появления машины

        var lastCar = cars[cars.Count - 1];
        if (cars.Count < 20)
        {
            var car = Instantiate(carPrefab, new Vector3(lastCar.transform.position.x + 60f, carPrefab.transform.position.y, carPrefab.transform.position.z + 3f * side), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            car.transform.SetParent(gameObject.transform); //Добавление машины в объект Road
            cars.Add(car);

            if (rand.Next(0, 100) > 60) //Добавление монеты с вероятностью 40%
            {
                var coin = Instantiate(coinPrefab, new Vector3(lastCar.transform.position.x + 20f, coinPrefab.transform.position.y, coinPrefab.transform.position.z + 3f * side * -1f), Quaternion.identity);
                coin.transform.SetParent(gameObject.transform);
            }
        }

        foreach (GameObject carItem in cars)
        {
            bool fetched = carItem.GetComponent<CarPlayer>().Fetch(x); //Проверка, проехал ли игрок этот блок

            if (fetched) //Если проехал
            {
                cars.Remove(carItem); 
                carItem.GetComponent<CarPlayer>().Delete(); 
            }
        }

        foreach (GameObject block in blocks)
        {
            bool fetched = block.GetComponent<RoadBlock>().Fetch(x); //Проверка, проехал ли игрок этот блок

            if (fetched) //Если проехал
            {
                blocks.Remove(block); 
                block.GetComponent<RoadBlock>().Delete(); 
            }
        }
    }

}
