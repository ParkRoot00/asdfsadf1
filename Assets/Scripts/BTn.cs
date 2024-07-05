using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTn : MonoBehaviour
{
    public GameObject obj;
    public void OnBtn()
    {
        obj.SetActive(true);
    }
    public void FallBtn()
    {
        obj.SetActive(false);
    }
}
