using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayBlockWallControl : MonoBehaviour
{
    public GameObject OneWayWall;
    public GameObject TwoWayWall;
    public GameObject BackWall;

    public void EnableTheWalls(int outgoing)
    {
        switch (outgoing)
        {
            case 1:
                OneWayWall.SetActive(true);
                break;
            case 2:
                TwoWayWall.SetActive(true);
                break;
            default:
                break;

        }
    }

    private void OnDisable()
    {
        OneWayWall.SetActive(false);
        TwoWayWall.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        BackWall.SetActive(true); 
    }

}
