using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Items {
    public class CollectableItem : MonoBehaviour, ICollectable {
        public Items.CollectableType Type;

        public void DestroyOnCollect()
        {
            Destroy(gameObject);
        }
    }
}
