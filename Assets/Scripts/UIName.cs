using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIName: MonoBehaviour
{
    public TMP_Text text;

    private void Update()
    {
        if(Edge.selected != null)
        {
            text.text = Edge.selected.EdgeVm.name;
        }
        else
        {
            text.text = "";
        }
    }
}