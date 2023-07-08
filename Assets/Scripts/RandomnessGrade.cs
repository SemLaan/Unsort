using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomnessGrade : MonoBehaviour
{
    
    public static int GradeRandomness(int[] numberList)
    {
        int randomness = 0;

        for (int i = 0; i < numberList.Length-1; i++)
        {
            randomness += math.abs(numberList[i] - numberList[i + 1]) - 1;
        }

        return randomness;
    }
}
