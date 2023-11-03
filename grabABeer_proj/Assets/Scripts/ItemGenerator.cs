using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Duarto.GrabABeer.Items {

    public class ItemGenerator : MonoBehaviour
    {
        public List<GameObject> items; //Items to fill the shelves with

//**********AWAKE**********//
        void Awake()
        {
            //Create a random item in every spot of the shelf
            foreach (Transform i in GetComponentInChildren<Transform>()) {
                GameObject obj = Instantiate(items[Random.Range(0, items.Count)]);
                obj.transform.position = i.position;
            }
        }
    }
}
