using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialTypes{Scrap,Bolt,SiliconBase,Wire}
public class MaterialItem : Item
{
    public MaterialTypes MaterialType;
}
