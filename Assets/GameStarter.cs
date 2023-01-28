using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject offerObjectMenu;
    public LayerMask objectsToHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitinfo, Mathf.Infinity, objectsToHit))
            {
                print(hitinfo.collider.name);
                offerObjectMenu.gameObject.SetActive(true);
            }
        }
    }

    
}
