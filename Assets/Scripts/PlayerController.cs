using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; set; }

    private CharacterController _controller;
    [SerializeField] private float _speed;

    

    //Obtenemos el character controller para aplicar el movimiento despues
    private void Start()
    {
        if(instance!=null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
        _controller = GetComponent<CharacterController>();
    }
    //Metemos en el update la funcion del movimiento y la rotacion para que se actualice en cuanto las pulsamos
    private void Update()
    {
        MovementAndRotation();
    }
    //Juntamos nuestras dos funciones de rotacion y movimiento
    private void MovementAndRotation()
    {
        _controller.Move(MoveVector());
        Rotation();
    }
    //Detectamos los inputs del teclado para que el personaje se mueva y devolvemos un vector con sus valores junto con la velocidad
    private Vector3 MoveVector()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        return movement* _speed* Time.deltaTime;
    }
    //Hacemos que el forward del personaje mire a la posicion a la que nos estamos desplazando 
    private void Rotation()
    {
        transform.LookAt(transform.position+MoveVector());
    }

    
}
