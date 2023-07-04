using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScore : MonoBehaviour
{
    public List<AddScore> holdScore;

    public AddScore GetAddScore()
    {
        foreach(AddScore score in holdScore)
        {
            if (!score.isActiveAndEnabled)
            {               
                return score;
            }
        }
        return null;
    }
}
