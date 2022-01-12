using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{

    public Material red;
    public Material yellow;
    public Material green;

    public Vector3 vec;
    public Quaternion qua;

    // Start is called before the first frame update
    void Start()
    {

   
        vec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        qua = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
      
    }
 
    void OnCollisionEnter(Collision col)
    {


        /*  Debug.Log(Time.realtimeSinceStartup); */
     Debug.Log(transform.position.y);
       
      transform.position = vec;
        transform.rotation = qua; 


    }
    // Update is called once per frame

    void FixedUpdate()
    {
        
        if (transform.position.y >= vec.y - 1)
        {
            gameObject.GetComponent<MeshRenderer>().material = red;
        }
        else if (transform.position.y >= vec.y - 2)
        {
            gameObject.GetComponent<MeshRenderer>().material = yellow;

        }
        else if (transform.position.y >= vec.y - 3)
        {
            gameObject.GetComponent<MeshRenderer>().material = green;

        }



    }


}
