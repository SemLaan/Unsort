using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ListInstruction
{
    moveRight,
    moveLeft,
    swapRight,
    swapLeft,
}

public class ListManager : MonoBehaviour
{
    [SerializeField] private int listSize = 100;
    private UIListManager uiListManager;
    private int visibleNumbers = 0;
    private int[] sortingList;
    private int listPointer = 0;
    private bool finishedAlgorithm = false;
    private int left = -1, right = 1;

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
            if (listPointer + i < 0 || listPointer + i >= listSize)
                displayList[i] = -1;
            else
                displayList[i] = sortingList[listPointer + i];
        }
        uiListManager.UpdateList(displayList);

        if (!finishedAlgorithm)
        {
            ExecuteInstruction(ListInstruction.swapRight);
            ExecuteInstruction(ListInstruction.moveRight);
        }
    }

    private void ExecuteInstruction(ListInstruction instruction)
    {
        int temp = 0;

        switch (instruction)
        {
            case ListInstruction.moveRight:
                Move(right); // move right
                break;
            case ListInstruction.moveLeft:
                Move(left); // move left
                break;
            case ListInstruction.swapRight:
                if (listPointer + 1 >= listSize)
                    break;
                temp = sortingList[listPointer];
                sortingList[listPointer] = sortingList[listPointer + 1];
                sortingList[listPointer + 1] = temp;
                break;
            case ListInstruction.swapLeft:
                temp = sortingList[listPointer];
                sortingList[listPointer] = sortingList[listPointer - 1];
                sortingList[listPointer - 1] = temp;
                break;
        }
    }

    #region Instruction functions

    private void Move(int direction)
    {
        listPointer += direction;
        if (listPointer < 0 || listPointer >= listSize)
            finishedAlgorithm = true;
    }

    #endregion
}
