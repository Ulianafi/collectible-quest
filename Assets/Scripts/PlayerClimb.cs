using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private PlayerMotor motor;
    private CharacterController controller;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            motor.isClimbing = true;  // Включаем режим лазания
            
            if (controller.isGrounded) motor.canJump = true;
            else motor.canJump = true;// Отключаем прыжок
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            motor.isClimbing = false; // Отключаем режим лазания
            motor.canJump = true;     // Включаем прыжок
        }
    }
}
