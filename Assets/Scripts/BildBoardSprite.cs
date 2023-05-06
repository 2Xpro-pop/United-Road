using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BildBoardSprite: MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}