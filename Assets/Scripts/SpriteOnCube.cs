using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class SpriteOnCube : MonoBehaviour
{
    public Sprite sprite; // Ссылка на спрайт, который вы хотите показать

    void Start()
    {
        var renderer = GetComponent<Renderer>(); // Получаем ссылку на рендерер объекта
        var material = new Material(Shader.Find("Standard")); // Создаем новый материал
        material.mainTexture = sprite.texture; // Назначаем текстуру спрайта в качестве текстуры материала
        renderer.material = material; // Назначаем материал на рендерер объекта
    }
}
