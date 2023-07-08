using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIListManager : MonoBehaviour
{
    public int pointerNumber = 0;
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
            if (data[i] == -1)
                uiNumberComponents[i].text = "";
            else
                uiNumberComponents[i].text = data[i].ToString();
        }
    }
}
