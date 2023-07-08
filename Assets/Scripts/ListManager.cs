using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ListManager : MonoBehaviour
{

    // Instruction execution duration
    [SerializeField] private float instructionDuration = 0f;
    private float timeSinceInstruction = 0f;

    // UI
    [SerializeField] private RectTransform visualList;
    [SerializeField] private GameObject visualNumberPrefab;
    [SerializeField] private TMP_Text randomnessUIElement;
    private RectTransform[] visualNumberInstances = null;
    private UIListManager uiListManager;
    private int visibleNumbers = 0;

    // Sorting list
    [SerializeField] private int listSize = 100;
    private int[] sortingList;
    private int listPointer = 0;

    // Algorithm
    [SerializeField] private ListInstruction[] instructions;
    private int currentInstruction = 0;
    private bool finishedAlgorithm = false;
    private int left = -1, right = 1;
    private int markedCell = -1;

    private void Awake()
    {
        uiListManager = FindObjectOfType<UIListManager>();
        sortingList = new int[listSize];
        visualNumberInstances = new RectTransform[listSize];
        for (int i = 0; i < sortingList.Length; i++)
        {
            sortingList[i] = i;
            visualNumberInstances[i] = Instantiate<GameObject>(visualNumberPrefab, visualList).GetComponent<RectTransform>();
            visualNumberInstances[i].sizeDelta = new Vector2(0, i);
        }
    }

    private void Start()
    {
        visibleNumbers = uiListManager.VisibleNumbers;
    }

    private void Update()
    {
        InstructionUpdate();
        UpdateUI();
    }

    private void UpdateUI()
    {
        int[] displayList = new int[visibleNumbers];
        for (int i = 0; i < visibleNumbers; i++)
        {
            if (listPointer + i - uiListManager.pointerNumber < 0 || listPointer + i - uiListManager.pointerNumber >= listSize)
                displayList[i] = -1;
            else
                displayList[i] = sortingList[listPointer + i - uiListManager.pointerNumber];
        }
        uiListManager.UpdateList(displayList);

        for (int i = 0; i < listSize; i++)
        {
            visualNumberInstances[i].sizeDelta = new Vector2(0, sortingList[i]+1);
        }
    }

    private void CheckIfFinished()
    {
        if (listPointer < 0 || listPointer >= listSize)
            FinishAlgorithm();
    }

    private void FinishAlgorithm()
    {
        finishedAlgorithm = true;
        randomnessUIElement.text = "Randomness: " + RandomnessGrade.GradeRandomness(sortingList);
    }

    private void InstructionUpdate()
    {
        if (instructions.Length == 0)
            return;

        timeSinceInstruction += Time.deltaTime;
        if (timeSinceInstruction > instructionDuration)
        {
            timeSinceInstruction -= instructionDuration;

            if (!finishedAlgorithm)
            {
                NextInstruction();
            }
        }
    }

    private void NextInstruction()
    {
        ExecuteInstruction(instructions[currentInstruction]);
        currentInstruction = (currentInstruction + 1) % instructions.Length;
    }

    private void ExecuteInstruction(ListInstruction instruction)
    {
        switch (instruction)
        {
            case ListInstruction.moveRight:
                Move(right); // move right
                break;
            case ListInstruction.moveLeft:
                Move(left); // move left
                break;
            case ListInstruction.swapRight:
                Swap(right); // swap the current element with the element to the right
                break;
            case ListInstruction.swapLeft:
                Swap(left); // swap the current element with the element to the left
                break;
            case ListInstruction.invertInstructions:
                InvertInstructions(); // Swap left and right
                break;
            case ListInstruction.markCell:
                MarkCell();
                break;
            case ListInstruction.swapWithMarkedCell:
                SwapWithMarkedCell();
                break;
            case ListInstruction.ifIdxGreaterThanNumber:
                if (listPointer <= sortingList[listPointer])
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
            case ListInstruction.ifIdxLessThanNumber:
                if (listPointer >= sortingList[listPointer])
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
            case ListInstruction.ifNumberGreaterThanIdx:
                if (sortingList[listPointer] <= listPointer)
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break; 
            case ListInstruction.ifNumberLessThanIdx:
                if (sortingList[listPointer] >= listPointer)
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
            case ListInstruction.ifNumberGreaterThanMarked:
                if (sortingList[markedCell] >= sortingList[listPointer])
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
            case ListInstruction.ifNumberLessThanMarked:
                if (sortingList[markedCell] <= sortingList[listPointer])
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
            case ListInstruction.ifIdxGreaterThanMarked:
                if (sortingList[markedCell] >= listPointer)
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
            case ListInstruction.ifIdxLessThanMarked:
                if (sortingList[markedCell] <= listPointer)
                    currentInstruction = (currentInstruction + 1) % instructions.Length;
                break;
        }
        CheckIfFinished();
    }

    #region Instruction functions

    private void Move(int direction)
    {
        listPointer += direction;
    }

    private void Swap(int direction)
    {
        // If trying to access array element that is out of bounds return
        if (listPointer + direction >= listSize || listPointer + direction < 0)
            return;
        int temp = sortingList[listPointer];
        sortingList[listPointer] = sortingList[listPointer + direction];
        sortingList[listPointer + direction] = temp;
    }

    private void InvertInstructions()
    {
        right *= -1; 
        left *= -1;
    }

    private void MarkCell()
    {
        markedCell = listPointer;
    }

    private void SwapWithMarkedCell()
    {
        int temp = sortingList[listPointer];
        sortingList[listPointer] = sortingList[markedCell];
        sortingList[markedCell] = temp;
    }
    #endregion
}
