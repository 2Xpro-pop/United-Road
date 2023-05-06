using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public GameObject maskot;
    public GameObject form;

    public TMP_InputField login;
    public TMP_InputField password;
    public Button button;

    public async void Login()
    {
        button.interactable = false;
        
        try
        {
            var vm = new LoginVm
            {
                username = login.text,
                password = password.text
            };
            var content = new StringContent(JsonUtility.ToJson(vm), Encoding.UTF8, "application/json");
            var response = await Api.Client.PostAsync("/api/authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = JsonUtility.FromJson<Authtoken>(json);
                Api.Token = token.accessToken;

                form.SetActive(false);
                StartCoroutine(ShowAndPush());

                Api.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Api.Token);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            button.interactable = true;
        }
        
    }

    IEnumerator ShowAndPush()
    {
        for(var i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.005f);
            maskot.transform.position += new Vector3(0.046f, 0, 0);

            if(i == 40)
            {
                StartCoroutine(RotateMaskot());
            }
        }

        yield return new WaitForSeconds(1.2f);

        for (var i = 0; i < 75; i++)
        {
            yield return new WaitForSeconds(0.005f);
            maskot.transform.position -= new Vector3(0.093f, 0, 0);

            if (i == 40)
            {
                StartCoroutine(RotateMaskot());
            }
        }

        yield return new WaitForSeconds(0.74f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    IEnumerator RotateMaskot(int x = -1)
    {
        var startRotation = maskot.transform.rotation;

        // Вычисляем конечную позицию объекта, повернутого на 45 градусов по оси Z
        var endRotation = Quaternion.Euler(0, 0, x*45) * startRotation;

        // Длительность анимации в секундах
        float duration = 1.0f;

        // Начальное время анимации
        float startTime = Time.time;

        // Выполняем анимацию поворота объекта
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            maskot.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        // Устанавливаем конечную позицию объекта
        maskot.transform.rotation = endRotation;
    }

}

[Serializable]
public class LoginVm
{
    public string username;
    public string password;
}
[Serializable]
public class Authtoken
{
    public string accessToken;
}