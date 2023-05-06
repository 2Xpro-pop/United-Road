using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIController: MonoBehaviour
{
    public static Vector3 position;
    public static string type;

    public CreateIssueModal modal;

    public void OpenIssuesForCurrentEdge()
    {
        StartCoroutine(OpenIssues());
    }

    public void CreateIssue()
    {
        NativeGallery.GetImageFromGallery(path =>
        {
            if(path == null) return;

            StartCoroutine(CreateIssueRequest(path));
        });
    }

    public static IEnumerator OpenIssues()
    {
        position = Camera.main.transform.position;

        for(var i = 0; i < 100; i++)
        {

            Camera.main.transform.position = Vector3.Lerp(position, new Vector3(position.x, 20, position.z), i / 100f);
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(0.1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Issues");
    }

    public IEnumerator CreateIssueRequest(string path)
    {
        var client = new HttpClient();
        var content = new MultipartFormDataContent();
        var file = new ByteArrayContent(System.IO.File.ReadAllBytes(path));
        content.Add(file, "photo", Path.GetFileName(path));


        IssueVm issue = new();

        modal.Open();
        modal.CreateIssue(i =>
        {
            type = i.type;
            issue = i;
        });

        yield return new WaitUntil( () => !string.IsNullOrWhiteSpace(type));

        type = null;

        content.Add(new StringContent(issue.type), "type");
        content.Add(new StringContent(issue.title), "title");
        content.Add(new StringContent("0.1d"), "longitude");
        content.Add(new StringContent("0.1d"), "latitude");
        content.Add(new StringContent(Edge.selected.EdgeVm.name), "streetName");

        var response = Api.Client.PostAsync("/api/issue", content);
        Task.Run( async () => await response).ConfigureAwait(false);
        yield return new WaitUntil(() => response.IsCompleted);

        Debug.Log("work!");
    }
}