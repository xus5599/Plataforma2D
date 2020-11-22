using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject misil;
    float timeAux;
    public float speed = 5f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        timeAux = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
       
            float timeDif = Time.time - timeAux;

            if ( timeDif > 3f)
            {
                Vector3 pos = new Vector3(transform.position.x - 0.738f, transform.position.y+ 0.224f , 0f);
                GameObject clone = Instantiate(misil, pos, Quaternion.identity) as GameObject;
                clone.SetActive(true);

                timeAux = Time.time;

            }

          
        
    }
   
}
