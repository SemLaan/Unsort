using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionStack : MonoBehaviour
{
    // List of instructions variables
    public List<ListInstruction> instructions;
    [HideInInspector] public List<GameObject> instructionButtons;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Vector2 buttonsAnchorPoint;
    [SerializeField] private float buttonsSpacing;

    // Sequence of instructions variables
    [HideInInspector] public List<ListInstruction> sequence;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private float maxCommands;
    [SerializeField] private float sequenceGridRadius;
    [SerializeField] private float sequenceGridX;
    [SerializeField] private float sequenceGridY;
    [SerializeField] private int sequenceSpacing;
    private List<GameObject> textObjects;
    private List<Vector2> gridPositions;
    private int sequenceIndex;

    private void Awake()
    {
        instructionButtons = new List<GameObject>();
        gridPositions = new List<Vector2>();
        textObjects = new List<GameObject>();
        sequenceIndex = 0;
    }

    private void Start()
    {
        CreateGridPoints();
        CreateButtons();
    }

    private void CreateButtons()
    {
        Debug.Log(instructions.Count);
        for (int i = 0; i < instructions.Count; i++)
        {
            GameObject button = Instantiate<GameObject>(buttonPrefab, new Vector3(0, 0), Quaternion.identity, transform);
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(buttonsAnchorPoint.x, buttonsAnchorPoint.y - (i * buttonsSpacing));
            instructionButtons.Add(button);

            InstructionButton instructionButton = button.GetComponent<InstructionButton>();
            instructionButton.startPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            instructionButton.SetInstruction(instructions[i]);
            instructionButton.SetText(getButtonString(instructions[i]));
        }
    }

    private void CreateGridPoints()
    {
        for (int i = 0; i < maxCommands; i++)
        {
            Vector2 point = new Vector2(sequenceGridX, sequenceGridY - (i * sequenceSpacing));
            gridPositions.Add(point);
        }
    }

    public void AddToSequence(ListInstruction instruction)
    {
        if(sequenceIndex >= maxCommands) { return; }

        sequence.Add(instruction);
        GameObject textObject = Instantiate<GameObject>(textPrefab, new Vector3(0, 0), Quaternion.identity, transform);
        textObject.GetComponent<RectTransform>().anchoredPosition = gridPositions[sequenceIndex];

        TextMeshProUGUI TMPtext = textObject.GetComponent<TextMeshProUGUI>();
        TMPtext.text = getButtonString(instruction);
        textObjects.Add(textObject);

        InstructionSequence instructionSequence = textObject.GetComponent<InstructionSequence>();
        instructionSequence.SetInstruction(instruction);
        instructionSequence.index = sequenceIndex;

        sequenceIndex += 1;
    }

    public void RemoveFromSequence(int i)
    {
        if(sequenceIndex <= 0) { return; }
        Destroy(textObjects[i]);
        textObjects.RemoveAt(i);
        sequence.RemoveAt(i);
        sequenceIndex -= 1;

        UpdateSequence();
    }

    private void UpdateSequence()
    {
        for(int i = 0; i < sequenceIndex; i++)
        {
            textObjects[i].GetComponent<InstructionSequence>().index = i;
            RectTransform rectTransform = textObjects[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = gridPositions[i];
        }
    }

    private string getButtonString(ListInstruction instruction)
    {
        string buttonText = "";

        switch (instruction)
        {
            case ListInstruction.moveLeft:
                buttonText = "move to left";
                break;
            case ListInstruction.moveRight:
                buttonText = "move to right";
                break;
            case ListInstruction.swapLeft:
                buttonText = "swap with left";
                break;
            case ListInstruction.swapRight:
                buttonText = "swap with right";
                break;
        }

        return buttonText;
    }
}
