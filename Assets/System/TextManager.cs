using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    public TextMeshProUGUI[] textMeshes;
    public static TextMeshProUGUI[] TextMeshes
    {
        get => Instance.textMeshes;
        set => Instance.textMeshes = value;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ChangeColor(Color color)
    {
        foreach(var t in TextMeshes)
        {
            t.color = color;
        }
    }
}
