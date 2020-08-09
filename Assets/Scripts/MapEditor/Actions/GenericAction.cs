using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAction : MonoBehaviour, IAction
{
    public Sprite actionIcon;
    virtual public string Name { get{ return "NoAction"; } }

    abstract public void Execute(Vector2 index, ActionsArgument argument);

    abstract public void Init(InitArgument args);
}
