using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalGenerator : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public int maxDepth;
    private int depth;

    // Начальный размер куба
    public float scale = 1.0f;
    // Множитель уменьшения для дочерних кубов
    public float childScaleFactor = 0.5f;

    private static readonly Vector3[] childDirections = {
        Vector3.up, Vector3.right, Vector3.left, Vector3.forward, Vector3.back, Vector3.down
    };

    private static readonly Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0, 0, -90f),
        Quaternion.Euler(0, 0, 90f),
        Quaternion.Euler(90f, 0, 0),
        Quaternion.Euler(-90f, 0, 0),
        Quaternion.Euler(180f, 0, 0)
    };

    private void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        // Создаем детей сразу без задержки
        CreateChildren();
    }

    private void CreateChildren()
    {
        for (int i = 0; i < childDirections.Length; i++)
        {
            new GameObject("Fractal Child").AddComponent<FractalGenerator>().Initialize(this, i);
        }
    }

    private void Initialize(FractalGenerator parent, int childIndex)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;

        transform.parent = parent.transform;

        // Уменьшаем масштаб по всем осям
        transform.localScale = new Vector3(parent.scale * childScaleFactor, parent.scale * childScaleFactor, parent.scale * childScaleFactor);

        // Вращение с случайным углом
        float randomRotation = Random.Range(0f, 360f);
        transform.localRotation = Quaternion.Euler(randomRotation, randomRotation, randomRotation);

        // Корректировка позиции
        float positionOffset = 0.5f * parent.scale + 0.5f * parent.scale * childScaleFactor;
        if (childDirections[childIndex] == Vector3.down)
        {
            positionOffset *= -1;
        }
        transform.localPosition = childDirections[childIndex] * positionOffset;

        // Изменение цвета по глубине
        float colorValue = Mathf.Lerp(0.1f, 1.0f, (float)depth / maxDepth);
        material.color = new Color(colorValue, 0.5f, 1.0f - colorValue);

        // Если не достигнут максимальный уровень глубины, создаем детей
        if (depth < maxDepth)
        {
            CreateChildren();
        }
    }
}
