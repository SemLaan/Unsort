using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ListInstruction
{
    moveRight,
    moveLeft,
    swapRight,
    swapLeft,
    invertInstructions,
    markCell,
    swapWithMarkedCell,
    ifIdxGreaterThanNumber,
    ifIdxLessThanNumber,
    ifNumberGreaterThanIdx,
    ifNumberLessThanIdx,
    ifNumberGreaterThanMarked,
    ifNumberLessThanMarked,
    ifIdxGreaterThanMarked,
    ifIdxLessThanMarked,
}
