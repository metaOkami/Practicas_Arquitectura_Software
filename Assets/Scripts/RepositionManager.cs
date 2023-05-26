using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionManager : MonoBehaviour
{
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private Transform _oppositeCollider;
    [SerializeField] private Transform _box;



    private void Awake()
    {
        controlsCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CubeBehaviour.cubeInstance.moveChances = 1;
            controlsCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<CharacterController>().enabled = false;
                CubeBehaviour.cubeInstance.canMove = true;
            }
            _box.transform.LookAt(_oppositeCollider);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                controlsCanvas.SetActive(false);
                other.GetComponent<CharacterController>().enabled = false;
                CubeBehaviour.cubeInstance.canMove = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            controlsCanvas.SetActive(false);
        }
    }


}