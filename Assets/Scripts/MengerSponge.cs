using UnityEngine;
using UnityEngine.UI; // Для работы с UI
using System.Collections.Generic;

public class MengerSpongeGPU : MonoBehaviour
{
    public Mesh cubeMesh;       // Модель куба
    public Material cubeMaterial; // Материал куба
    public int recursionDepth = 4; // Глубина рекурсии
    public float cubeSize = 1.0f; // Начальный размер

    private List<Matrix4x4> matrices = new List<Matrix4x4>(); // Список для GPU

    public Slider recursionSlider; // Ссылка на ползунок

    void Start()
    {
        // Установим начальное значение ползунка
        if (recursionSlider != null)
        {
            recursionSlider.value = recursionDepth;
            recursionSlider.onValueChanged.AddListener(UpdateRecursionDepth);
        }

        GenerateMengerSponge(transform.position, cubeSize, recursionDepth);
    }

    void Update()
    {
        int batchSize = 1023; // Максимально допустимое число за раз
        for (int i = 0; i < matrices.Count; i += batchSize)
        {
            int count = Mathf.Min(batchSize, matrices.Count - i);
            Graphics.DrawMeshInstanced(cubeMesh, 0, cubeMaterial, matrices.GetRange(i, count));
        }
    }

    void GenerateMengerSponge(Vector3 position, float size, int depth)
    {
        if (depth == 0)
        {
            // Вместо создания объекта — сохраняем его в список
            Matrix4x4 matrix = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one * size);
            matrices.Add(matrix);
            return;
        }

        float newSize = size / 3;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    int sum = Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z);
                    if (sum > 1)  // Пропускаем центральный куб
                    {
                        Vector3 newPosition = position + new Vector3(x * newSize, y * newSize, z * newSize);
                        GenerateMengerSponge(newPosition, newSize, depth - 1);
                    }
                }
            }
        }
    }

    // Метод для обновления глубины рекурсии, вызывается при изменении ползунка
    void UpdateRecursionDepth(float value)
    {
        recursionDepth = Mathf.RoundToInt(value); // Устанавливаем новое значение глубины
        matrices.Clear(); // Очищаем текущие матрицы
        GenerateMengerSponge(transform.position, cubeSize, recursionDepth); // Перегенерируем фрактал с новой глубиной
    }
}
