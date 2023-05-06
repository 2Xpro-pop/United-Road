using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Issue: MonoBehaviour
{
    private IssueVm vm;

    public TMP_Text title;
    public TMP_Text date;
    public TMP_Text tags;
    public RawImage image;

    public IssueVm Vm {
        get => vm;
        set 
        {
            vm = value;
            Render();
        }
    }

    private void Render()
    {
        title.text = Vm.title;
        date.text = Vm.issuedAt;
        tags.text = Vm.GetNormalType();

        StartCoroutine(LoadImage());
    }

    private IEnumerator LoadImage()
    {
        var request = new WWW($"{Api.Host}/image/{Vm.photoUrl}");
        yield return request;

        // Получаем размеры текстуры изображения
        Texture2D texture = request.texture;
        RawImage rawImage = image;

        float aspectRatio = (float)texture.width / (float)texture.height;
        float requiredWidth = aspectRatio * rawImage.rectTransform.rect.width;
        float requiredHeight = aspectRatio * rawImage.rectTransform.rect.height;

        rawImage.texture = texture;
        rawImage.uvRect = new Rect(0, 0, requiredWidth / rawImage.rectTransform.rect.width, 1);
    }
}
