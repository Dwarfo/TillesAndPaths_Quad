﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { };
public class PathEvent : UnityEvent<Path>{ };
public class BoolEvent : UnityEvent<bool> { };
public class EmptyEvent : UnityEvent { };
public class TileInfoEvent : UnityEvent<SO_Tile> { };
public class SaveGameEvent : UnityEvent<MapData> { };
