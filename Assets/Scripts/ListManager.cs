using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    [SerializeField] private int listSize = 100;
    private UIListManager uiListManager;
    int visibleNumbers = 0;
    int[] sortingList;
    int listPointer = 0;

    private void Awake()
    {
        uiListManager = FindObjectOfType<UIListManager>();
        sortingList = new int[listSize];
        for (int i = 0; i < sortingList.Length; i++)
        {
            sortingList[i] = i;
        }
    }

    private void Start()
    {
        visibleNumbers = uiListManager.VisibleNumbers;
    }

    private void Update()
    {
        int[] displayList = new int[visibleNumbers];
        for (int i = 0; i < visibleNumbers; i++)
        {
            if (listPointer < 0 || listPointer + i >= listSize)
                displayList[i] = 0;
            else
                displayList[i] = sortingList[listPointer + i];
        }
        uiListManager.UpdateList(displayList);


        int temp = sortingList[listPointer];
        sortingList[listPointer] = sortingList[listPointer+1];
        sortingList[listPointer+1] = temp;
        
        //listPointer++;
        //listPointer %= listSize;
    }
}
