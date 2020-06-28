using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveAssigner
{
    SaveGameEntry AssignSaveData(MapData md);
}
