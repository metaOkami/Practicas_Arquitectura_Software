using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//En este script determinamos las funciones por defecto que van a tener cada command
//y los distintos commands que hay.

public abstract class Command 
{
    //Variable protegida de la distancia que ha de moverse el cubo
    protected float moveDistance = 1f;
    //Funcion con obligacion de hacer override en cada command de ejecutar
    public abstract void Execute(Transform objectToMove, Command commandDone);
    //Funcion para deshacer el ejecutar
    public virtual void Undo(Transform objectToMove) { }
    //Funcion auxiliar para mover el cubo en una direccion concreta
    public virtual void Move(Transform objectToMove) { }

}

//Command de mover hacia alante
public class MoveForward : Command
{
    //Funcion específica dentro de este comando que se produce al ejecutar el mismo
    public override void Execute(Transform boxTransform, Command command)
    {
        //Funcion aun en pruebas, antes de moverlo comprobamos que no haya límites en la direccion a la que vamos a moverlo
        if(!Physics.Raycast(boxTransform.position,boxTransform.forward+new Vector3(0,1,0),out RaycastHit hit, 1.05f)){
            Move(boxTransform);
            CubeBehaviour.OldActions.Add(command);
            Debug.Log("NO Detecto limites");
        }
        else
        {
            Debug.Log("Detecto limites");
        }
        
    }

    //Funcion específica dentro de este comando que deshace la función de ejecutar del mismo
    public override void Undo(Transform objectToMove)
    {
        //Para deshacer, simplemente hacemos la función inversa a la hecha en el execute, aqui no comprobamos
        //limites debido a que ya se ha mirando anteriormente.
        objectToMove.Translate(-objectToMove.forward * moveDistance,Space.World);
    }

    //Funcion específica de este comando que dice como ha de moverse el objeto al que queremos aplicar este patrón
    public override void Move(Transform objectToMove)
    {
        objectToMove.Translate(objectToMove.forward * moveDistance, Space.World);
    }
}
//Command de mover hacia la izquierda
public class MoveLeft : Command
{
    //Funcion específica dentro de este comando que se produce al ejecutar el mismo
    public override void Execute(Transform boxTransform, Command command)
    {
        if(!Physics.Raycast(boxTransform.position, -boxTransform.right + new Vector3(0, 1, 0), out RaycastHit hit, 1.05f))
        {
            Move(boxTransform);
            CubeBehaviour.OldActions.Add(command);
        }
    }

    //Funcion específica dentro de este comando que deshace la función de ejecutar del mismo
    public override void Undo(Transform objectToMove)
    {
        objectToMove.Translate(objectToMove.right * moveDistance, Space.World);
    }

    //Funcion específica de este comando que dice como ha de moverse el objeto al que queremos aplicar este patrón
    public override void Move(Transform objectToMove)
    {
        objectToMove.Translate(-objectToMove.right * moveDistance,Space.World);
    }
}
//Command de mover a la derecha
public class MoveRight : Command
{
    //Funcion específica dentro de este comando que se produce al ejecutar el mismo
    public override void Execute(Transform boxTransform, Command command)
    {
        if(!Physics.Raycast(boxTransform.position, boxTransform.right + new Vector3(0, 1, 0), out RaycastHit hit, 1.05f))
        {
            Move(boxTransform);
            CubeBehaviour.OldActions.Add(command);
        }
        else
        {
            Debug.Log("Detecto limites");
        }
        
    }

    //Funcion específica dentro de este comando que deshace la función de ejecutar del mismo
    public override void Undo(Transform objectToMove)
    {
        objectToMove.Translate(-objectToMove.right * moveDistance, Space.World);
    }

    //Funcion específica de este comando que dice como ha de moverse el objeto al que queremos aplicar este patrón
    public override void Move(Transform objectToMove)
    {
        objectToMove.Translate(objectToMove.right * moveDistance, Space.World);
    }
}

//Command que deshace la ultima accion hecha
public class UndoCommand : Command
{
    public override void Execute(Transform objectToMove, Command commandDone)
    {
        List<Command> oldCommands = CubeBehaviour.OldActions;
        if (oldCommands.Count > 0)
        {
            Command lastCommand = oldCommands[oldCommands.Count - 1];
            lastCommand.Undo(objectToMove);
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }
}


