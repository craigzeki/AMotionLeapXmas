using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeamHand : MonoBehaviour
{
    public enum TractorDirection
    {
        NOT_SET = 0,
        UP,
        DOWN,
        NUM_OF_DIRECTIONS
    }

    [SerializeField] private InteractionHand _hand;
    [SerializeField] private float _radiusOfTractorCast = 0.05f;
    [SerializeField] private float _distanceOfTractorCast = 4.0f;
    [SerializeField] private float _keepAwayDistance = 0.5f;
    [SerializeField] private float _tractorSpeed = 0.5f;

    private GameObject _objectInBeam;

    private TractorDirection tractorDirection = TractorDirection.NOT_SET;

    private GameObject _hoverPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (_hand == null)
        {
            _hand = GetComponent<InteractionHand>();
        }


    }

    

    // Update is called once per frame
    void Update()
    {

        GetObjectInRange();

        MoveObject();

    }

    private void MoveObject()
    {
        if (_hand == null) return;
        if (_objectInBeam == null) return;

        if(_hand.position.x < -0.15)
        {
            tractorDirection = TractorDirection.UP;
        }
        else if(_hand.position.x > 0.15)
        {
            tractorDirection = TractorDirection.DOWN;
        }

        switch (tractorDirection)
        {
            case TractorDirection.NOT_SET:
                return;
                break;
            case TractorDirection.UP:
                Debug.Log("Tractor Up");
                _objectInBeam.transform.position = Vector3.MoveTowards(_objectInBeam.transform.position, new Vector3(_hand.position.x, _hand.position.y - _keepAwayDistance, _hand.position.z), _tractorSpeed * Time.deltaTime);
                break;
            case TractorDirection.DOWN:
                Debug.Log("Tractor Down");
                break;
            case TractorDirection.NUM_OF_DIRECTIONS:
            default:
                break;
        }
    }


    private void GetObjectInRange()
    {
        //if we already have an object in the beam, don't try and get another
        if (_objectInBeam != null) return;

        //else try and find a new object
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("TractorObject");

        //get the first object within the sphere cast radius
        if(Physics.SphereCast(_hand.position, _radiusOfTractorCast, Vector3.down, out hit, _distanceOfTractorCast, mask))
        {
            _objectInBeam = hit.transform.gameObject;
        }

        
    }


}
