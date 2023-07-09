using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionSequence : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public int index;

    private InstructionStack instructionStack;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private ListInstruction instruction;

    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        instructionStack = FindObjectOfType<InstructionStack>();
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            instructionStack.RemoveFromSequence(index);
            return;
        }
    }

    public void SetInstruction(ListInstruction instruction)
    {
        this.instruction = instruction;
    }

    private bool isOverlapping()
    {
        return true;
    }
}
