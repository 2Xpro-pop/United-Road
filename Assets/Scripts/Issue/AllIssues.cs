using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AllIssues: MonoBehaviour
{
    public TMP_Text title;
    public GameObject listView;

    public GameObject issuePrefab;
    public IssuesVm issuesVm;

    async void Start()
    {
        var edgeVm = Edge.selected.EdgeVm;
        title.text = edgeVm.name;

        var parameters = await new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "streetName", edgeVm.name }
        }).ReadAsStringAsync();
        Debug.Log(parameters);
        var response = await Api.Client.GetAsync($"/api/street?{parameters}");
        var json = await response.Content.ReadAsStringAsync();

        issuesVm = JsonUtility.FromJson<IssuesVm>(json);

        foreach (var issueVm in issuesVm.issues)
        {
            var issue = Instantiate(issuePrefab, listView.transform);
            issue.GetComponent<Issue>().Vm = issueVm;
        }

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }
}
[Serializable]
public class IssuesVm
{
    public IssueVm[] issues;
}

[Serializable]
public class IssueVm
{
    public int id;
    public User user;
    public string title;
    public string photoUrl;
    public string issuedAt;
    public string issueStatus;
    public string type;

    public string GetNormalType() => type switch
    {
        "ЯМА" => "Яма",
        "СВЕТОФОР" => "Светофор",
        "НЕПРАВИЛЬНЫЙ_ЗНАК" => "Неправильный знак",
        "ОТСУТСВИЕ_ЛЮКА" => "Отсутсвие люка",
        "ПОВРЕЖДЕНИЕ_АСФАЛЬТА" => "Повреждение асфальта",
        "НЕДОСТАТОЧНОЕ_ОСВЕЩЕНИЕ" => "Недостаточное освещение",
        "ДОРОЖНАЯ_МАРКИРОВКА" => "Дорожная маркировка",
        _ => type,
    };

    public static string GetNormalTypeByEnum(IssueTypes issue) => issue switch
    {
        IssueTypes.ЯМА => "Яма",
        IssueTypes.НЕПРАВИЛЬНЫЙ_ЗНАК => "Неправильный знак",
        IssueTypes.ОТСУТСТВИЕ_ЛЮКА => "Отсутсвие люка",
        IssueTypes.НЕДОСТАТОЧНОЕ_ОСВЕЩЕНИЕ => "Недостаточное освещение",
        IssueTypes.ДОРОЖНАЯ_МАРКИРОВКА => "Дорожная маркировка",
        _ => issue.ToString(),
    };

    public static string GetTypeFromNormal(string normalType) => normalType switch
    {
        "Яма" => "ЯМА",
        "Светофор" => "СВЕТОФОР",
        "Неправильный знак" => "НЕПРАВЕЛЬНЫЙ_ЗНАК",
        "Отсутсвие люка" => "ОТСУТСВИЕ_ЛЮКА",
        "Повреждение асфальта" => "ПОВРЕЖДЕНИЕ_АСФАЛЬТА",
        "Недостаточное освещение" => "НЕДОСТАТОЧНОЕ_ОСВЕЩЕНИЕ",
        "Дорожная маркировка" => "ДОРОЖНАЯ_МАРКИРОВКА",
        _ => normalType,
    };
}

[Serializable]
public class User
{
    public string name;
}