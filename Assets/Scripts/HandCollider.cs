using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{

    [SerializeField] private InteractionHand _hand;
    [SerializeField] private GameObject colliderPrefab;
    [SerializeField] private GameObject collider;

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
        AddCollider();
    }

    void AddCollider()
    {

        if (collider != null)
        {
            if (!_hand.isTracked)
            {
                Destroy(collider);
            }
            else
            {
                collider.transform.position = _hand.position;
                collider.transform.rotation = _hand.rotation;
            }
        }
        else
        {
            if (_hand.isTracked)
            {
                collider = Instantiate(colliderPrefab, _hand.hoverPoint, Quaternion.identity);
                collider.transform.parent = this.gameObject.transform;
            }

        }
    }

    
}
