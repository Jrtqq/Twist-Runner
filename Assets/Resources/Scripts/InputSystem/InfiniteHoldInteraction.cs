using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfiniteHoldInteraction : IInputInteraction
{
    [RuntimeInitializeOnLoadMethod]
    private static void Register()
    {
        InputSystem.RegisterInteraction<InfiniteHoldInteraction>();
    }

    public void Process(ref InputInteractionContext context)
    {
        if (context.ControlIsActuated())
        {
            if (context.phase == InputActionPhase.Waiting)
            {
                context.Started();
            }

            context.Performed();
        }
        else
        {
            if (context.phase == InputActionPhase.Performed)
            {
                context.Canceled();
            }
        }
    }

    public void Reset() { }
}