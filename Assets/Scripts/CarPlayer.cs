using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    public bool Fetch(float x) //Проверка, проехала ли машина игрока эту машину на достаточное расстояние
    {
        bool result = false;

        if (x > transform.position.x + 10f)
        {
            result = true; //Если машина проехала на 100f от машини, то возвращается true
        }

        return result;
    }

    public void Delete()
    {
        Destroy(gameObject); //Удаление блока
    }
}
