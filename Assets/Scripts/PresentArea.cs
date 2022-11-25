using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PresentColour
{
    RED = 0,
    BLUE,
    GREEN,
    PURPLE,
    NUM_OF_COLORS
}

public class PresentArea : MonoBehaviour
{
    
    [SerializeField] private PresentColour presentColour = PresentColour.RED;

    public PresentColour PresentColour { get => presentColour;  }
}
