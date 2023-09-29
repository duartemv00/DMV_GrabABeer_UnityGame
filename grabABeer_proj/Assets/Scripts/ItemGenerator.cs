using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duarto.GrabABeer.Items {

    public class ItemGenerator : MonoBehaviour
    {
        public List<GameObject> items;
        //Renderer m_Renderer;

        void Awake()
        {
            foreach (Transform i in GetComponentsInChildren<Transform>()) {
                GameObject obj = Instantiate(items[Random.Range(0, items.Count)]);
                obj.transform.position = i.position;   
            }
        }

        void Start()
        {
            //m_Renderer = GetComponent<Renderer>();
        }
        
        /*void Update()
        {
            if (m_Renderer.isVisible) {
                this.gameObject.SetActive(true);
            }
            else {
                this.gameObject.SetActive(true);
            }
        }*/

        /*void OnBecameInvisible()
        {
            enabled = false;
        }*/

        /*void OnBecameVisible()
        {
            enabled = true;
        }*/
    
    }
}
