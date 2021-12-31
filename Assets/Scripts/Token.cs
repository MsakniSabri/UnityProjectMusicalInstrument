using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{    
    bool on=true;
    public Material red;
    public Material yellow;
    public Material green;
     Vector3 vec ;
     Quaternion qua;
     float speed=3.0f;
     float time=1.0f; //~1s 
    // Start is called before the first frame update
    void Start()
    {
        
       
        vec = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        qua= new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z,transform.rotation.w);
        
    }
void OnCollisionEnter(Collision col) 
    {
      

        Debug.Log(Time.realtimeSinceStartup);
        transform.position=vec;
        transform.rotation=qua; 
       
        
    }
    // Update is called once per frame

    void Update()
    {
        if(transform.position.y>vec.y-1){
gameObject.GetComponent<MeshRenderer>().material = red;
        }
        else if (transform.position.y > vec.y - 2)
        {
gameObject.GetComponent<MeshRenderer>().material = yellow;

        }
        else if (transform.position.y > vec.y - 3)
        {
gameObject.GetComponent<MeshRenderer>().material = green;
        }

        if (!on) { gameObject.GetComponent<Renderer>().enabled = false; }
        if (on) { gameObject.GetComponent<Renderer>().enabled = true; }
        falling();
        
    }
   void falling(){
        if (on)
        {
transform.Translate(-Vector3.forward * (speed/time) * Time.deltaTime);}
   }
}
