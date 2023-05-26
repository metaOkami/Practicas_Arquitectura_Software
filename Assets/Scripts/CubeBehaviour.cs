using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    //Variable de tipo cubeBehaviour a la que acceder desde otras clases en caso de ser necesario
    public static CubeBehaviour cubeInstance { get; private set; }
    //Variable que determina si el cubo se puede mover o no
    public bool canMove = false;
    //Variables tipo command para especificar que ejecuta cada botón especifico
    private Command ButtonW, ButtonD, ButtonA, ButtonZ;
    //Numero de oportunidades que tiene el jugador para deshacer
    private int undoChances = 3;
    //Variable para no poder mover el cubo mas de una vez de seguido
    public int moveChances = 1;
    //Lista de acciones que se hace para la hora de poder hacer una replay de cara a futuro y la funcion de deshacer
    public static List<Command> OldActions = new List<Command>();
    //Canvas de victoria
    public GameObject winCanvas;
    //Variable que determina el objeto que va a ser observado
    Observable cubeObservable = new Observable();
    //Para mantener la instancia del cubo intacta nos aseguramos preguntando si es nula y diferente a este script
    //Este metodo de instanciar clases es el conocido como Singleton
    public void Awake()
    {
        //Nos aseguramos de que el canvas de victoria se inicie estando desactivado
        winCanvas.SetActive(false);
        //Hago un singleton para asegurarme que no se instancie esta clase mas de una vez.
        if (cubeInstance!=null && cubeInstance != this)
        {
            Destroy(this);

        }
        else
        {
            cubeInstance = this;
        }
    }

    private void Start()
    {
        //Declaro la funcionalidad de cada boton
        ButtonW = new MoveForward();
        ButtonA = new MoveLeft();
        ButtonD = new MoveRight();
        ButtonZ = new UndoCommand();

        //Objeto de tipo EndCube a la que pasamos como parámetro el canvas que ha de activarse cuando se le lance el evento
        EndCube endCube = new EndCube(winCanvas);
        //Añadimos como observador al cubo para que observe este objeto y reaccione a los cambios oportunos
        cubeObservable.AddObserver(endCube);
    }

    private void Update()
    {
        //Si el cubo se puede mover, recibimos los inputs para moverlo.
        if (canMove)
        {
            HandleInput();
        }
    }

    public void HandleInput()
    {
        //En esta funcion comprobamos si se está presionando una tecla de las que acciona un cambio, accedemos a su command y lo ejecutamos
        //en caso de que la variable moveChances sea 0.
        //Cuando pulsamos el boton, se restan los moveChances, ejecuta la funcion deseada, pone en false el canMove y reestablece el movimiento
        //al jugador.
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            if (moveChances > 0)
            {
                moveChances--;
                ButtonW.Execute(transform, ButtonW);
                canMove = false;
                PlayerController.instance.gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (moveChances > 0)
            {
                moveChances--;
                ButtonD.Execute(transform, ButtonD);
                canMove = false;
                PlayerController.instance.gameObject.GetComponent<CharacterController>().enabled = true;
            }        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (moveChances > 0)
            {
                moveChances--;
                ButtonA.Execute(transform, ButtonA);
                canMove = false;
                PlayerController.instance.gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (undoChances > 0)
            {
                undoChances--;
                ButtonZ.Execute(transform, ButtonZ);
                canMove = false;
                PlayerController.instance.gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //En caso de que se entre en la posición deseada (con un cubo como preview para el jugador y referencia para nosotros) notificamos a nuestro
        //observador de que estamos donde tenemos que estar.
        if (other.tag == "FinalCube")
        {
            if (transform.position == other.transform.position)
            {
                cubeObservable.Notify();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        try
        {

            Gizmos.DrawLine(transform.position, (transform.forward * 1.05f) + new Vector3(0, 1, 0) );
        }
        catch (System.Exception)
        {

            
        }
    }

}


