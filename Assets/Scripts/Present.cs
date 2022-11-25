using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    [SerializeField] private PresentColour _presentColour = PresentColour.BLUE;
    [SerializeField] private GameObject _confettiPrefab;
    [SerializeField] private uint _points = 10;

    private bool _fudge = false;

    private void OnTriggerEnter(Collider other)
    {
        PresentArea area;
        if(other.gameObject.TryGetComponent<PresentArea>(out area))
        {
            if(area.PresentColour == _presentColour)
            {
                if(!_fudge)
                {
                    _fudge = true;
                    //add points + trigger spawn of new present
                    GameManager.Instance.AddPoints(_points);
                    GameManager.Instance.SpawnPresent();
                    //spawn confetti
                    Instantiate(_confettiPrefab, transform.position, Quaternion.identity);


                    //destroy present
                    Destroy(this.gameObject);
                }
                
            }
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsInPlayArea(transform.position))
        {
            GameManager.Instance.SpawnPresent();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!_fudge)
        //{
        //    _fudge = true;
        //    if (other.transform.tag == "TheVoid")
        //    {
        //        GameManager.Instance.SpawnPresent();
        //        Destroy(this.gameObject);
        //    }
        //}
    }

}
