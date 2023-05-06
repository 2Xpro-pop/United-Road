using UnityEngine;

public class SwipeCamera : MonoBehaviour
{
    public float moveSpeed = 0.1f; // скорость перемещения камеры
    private bool isDragging = false; // флаг для проверки, нажата ли кнопка мыши
    private Vector3 startPosition; // начальная позиция камеры
    public static bool isWork = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentPosition = Input.mousePosition;
            Vector3 direction = currentPosition - startPosition;
            direction.z = -direction.y;
            direction.x = -direction.x;
            direction.y = 0; // устанавливаем Y в 0, чтобы камера не двигалась вверх или вниз

            direction = Quaternion.Euler(45f, -45f, 0f) * direction;

            if (direction.magnitude > 1f)
            {
                direction.Normalize();
            }
            direction.y = 0;

            transform.position += moveSpeed * Time.deltaTime * direction;

            startPosition = currentPosition;
        }

    }


}
