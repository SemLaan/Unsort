using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InstructionButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ListInstruction instruction;
    private InstructionStack instructionStack;
    private List<Vector2> gridPositions;
    private RectTransform rectTransform;
    [HideInInspector] public Vector2 startPosition;

    private bool isDragging;

    private void Start()
    {
        instructionStack = GameObject.FindObjectOfType<InstructionStack>();
        rectTransform = GetComponent<RectTransform>();
        gridPositions = new List<Vector2>();
    }

    public void SetInstruction(ListInstruction listInstruction)
    {
        instruction = listInstruction;
    }

    public void SetText(string text)
    {
        Transform textObject = transform.GetChild(0);
        TextMeshProUGUI TMPtext = textObject.GetComponent<TextMeshProUGUI>();
        TMPtext.text = text;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        instructionStack.AddToSequence(instruction);
    }
}
