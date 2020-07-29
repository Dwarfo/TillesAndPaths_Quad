using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    string Name { get; }
    void Execute(Vector2 index, ActionsArgument argument);
}
