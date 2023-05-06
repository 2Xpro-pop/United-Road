using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateIssueModal: MonoBehaviour
{
    public Canvas canvas;

    public TMP_InputField title;
    public TMP_Dropdown type;
    public Button button;

    private void Start()
    {
        type.ClearOptions();
        type.AddOptions(
            Enum.GetValues(typeof(IssueTypes))
                .Cast<IssueTypes>()
                .Select(n => IssueVm.GetNormalTypeByEnum(n))
                .ToList()
        );
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void CreateIssue(Action<IssueVm> action)
    {
        button.onClick.AddListener(() =>
        {
            action(new IssueVm()
            {
                title = title.text,
                type = IssueVm.GetTypeFromNormal(type.options[type.value].text)
            });
            Close();
            button.onClick.RemoveAllListeners();
        });
        
    }
}
