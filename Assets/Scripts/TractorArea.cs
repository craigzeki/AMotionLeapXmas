using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorArea : MonoBehaviour
{
    [SerializeField] private TractorBeamHand.TractorDirection _tractorDirection = TractorBeamHand.TractorDirection.NOT_SET;

    public TractorBeamHand.TractorDirection TractorDirection { get => _tractorDirection; }
    // Start is called before the first frame update

}
