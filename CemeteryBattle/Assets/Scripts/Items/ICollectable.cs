using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Items
{
    public enum CollectableType
    {
        None,
        ArrowPack,
        LifePack,
    }

    interface ICollectable
    {
        void DestroyOnCollect();
    }
}