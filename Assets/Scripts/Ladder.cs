using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект игроком и имеет ли он компонент PlayerMotor
        PlayerMotor motor = other.GetComponent<PlayerMotor>();
        if (motor != null)
        {
            motor.isClimbing = true;  // Включаем режим лазания
            motor.canJump = false;    // Отключаем прыжок
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, является ли объект игроком и имеет ли он компонент PlayerMotor
        PlayerMotor motor = other.GetComponent<PlayerMotor>();
        if (motor != null)
        {
            motor.isClimbing = false; // Выключаем режим лазания
            motor.canJump = true;     // Включаем прыжок
        }
    }
}
