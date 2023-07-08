using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIListManager : MonoBehaviour
{

    private TMP_Text[] uiNumberComponents;
    public int VisibleNumbers { get; private set; }

    private void Awake()
    {
        uiNumberComponents = GetComponentsInChildren<TMP_Text>();
        VisibleNumbers = uiNumberComponents.Length;
        Debug.Log(uiNumberComponents.Length);
    }

    public void UpdateList(int[] data)
    {
        if (data.Length > VisibleNumbers)
            throw new System.Exception("too many numbers");

        for (int i = 0; i < data.Length; i++)
        {
            uiNumberComponents[i].text = data[i].ToString();
        }
    }
}
