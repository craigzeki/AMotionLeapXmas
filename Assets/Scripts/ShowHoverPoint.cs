using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHoverPoint : MonoBehaviour
{
    [SerializeField] private GameObject _hoverPointPrefab;
    [SerializeField] private InteractionHand _hand;

    private GameObject hoverPoint;
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
        Test();
    }

    void Test()
    {

        Debug.Log("Rotation: " + _hand.rotation);
        Debug.Log("Position: " + _hand.position);

        if (hoverPoint != null)
        {
            if (!_hand.isTracked)
            {
                Destroy(hoverPoint);
            }
            else
            {
                hoverPoint.transform.position = _hand.position;
                hoverPoint.transform.rotation = _hand.rotation;
            }
        }
        else
        {
            if (_hand.isTracked)
            {
                hoverPoint = Instantiate(_hoverPointPrefab, _hand.hoverPoint, Quaternion.identity);
                hoverPoint.transform.parent = this.gameObject.transform;
            }

        }
    }
}
