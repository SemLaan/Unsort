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
            if (listPointer + i < 0 || listPointer + i >= listSize)
                displayList[i] = -1;
            else
                displayList[i] = sortingList[listPointer + i];
        }
        uiListManager.UpdateList(displayList);

        for (int i = 0; i < listSize; i++)
        {
            visualNumberInstances[i].sizeDelta = new Vector2(0, sortingList[i]);
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
        }
    }

    #region Instruction functions

    private void Move(int direction)
    {
        listPointer += direction;
        CheckIfFinished();
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

    #endregion
}
