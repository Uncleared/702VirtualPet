using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControls : MonoBehaviour
{
    public CatBehaviour catBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            catBehaviour.GoToOwner();
        }

        if (Input.GetMouseButtonDown(1))
        {
            catBehaviour.Bounce();
        }
    }
}
