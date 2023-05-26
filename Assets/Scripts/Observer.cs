using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que determina las funciones y funcionalidades de los observadores

//Clase con la función obligada de hacer override de la funcion de notificar del Observado
public abstract class Observer 
{
    public abstract void OnNotify();
}


//Clase de tipo observador que determina que hace el mencionado cuando se le notifique de un cambio
public class EndCube : Observer
{
    GameObject winCanvas;
    
    //Aqui hacemos que para poder crear un objeto de tipo EndCube tengas que pasar un gameobject como parametro
    //en este caso el canvas que vamos a activar al ganar
    public EndCube(GameObject canvas)
    {
        this.winCanvas = canvas;
        
    }
    //Funcion de notificar en este observador concreto
    public override void OnNotify()
    {
        ActivateCanvas();
    }
    //Funcion que activa el canvas
    void ActivateCanvas()
    {
        winCanvas.SetActive(true);
    }

}
