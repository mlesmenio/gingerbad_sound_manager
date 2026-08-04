using UnityEngine;

public class Sphere : MonoBehaviour
{
    public SoundObject ost;

    public SoundObject doubleKill;

    public SoundObject moskitoDeath;
    // Start is called before the first frame update
    void Start(){

        if(ost) {ost.Play(transform);} 
        
        if(doubleKill) {GameObject go2 = doubleKill.Play();} 
        
    }

    void FixedUpdate(){

        transform.position += new Vector3(10f * Time.fixedDeltaTime,0f,0f);

        if(transform.position.x > 50f){

            if(moskitoDeath) moskitoDeath.Play(null, transform.position);
            Destroy(gameObject);
        } 
    }
}
