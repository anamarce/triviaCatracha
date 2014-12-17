using UnityEngine;
using System.Collections;


    public class EffectsManager : MonoBehaviour {
	

	

        public GameObject PlayConstantEffect(GameObject particleResource, Vector3 pos)
        {
       

            if (particleResource != null)
            {


                var goP = (GameObject)Instantiate(particleResource, pos, Quaternion.identity);

                var ps = goP.GetComponent<ParticleSystem>();

                ps.Play();

                return goP;

            }
            return null;
        }

  
        public GameObject PlayConstantEffect(string particleResource,Vector3 pos)
        {
		
		
            if(particleResource!=string.Empty)
            {
		    
			
                var goP = (GameObject)Instantiate(Resources.Load(particleResource),pos,Quaternion.identity);
		  	
                var  ps= goP.GetComponent<ParticleSystem>();
	  
                ps.Play();
		  
                return goP;
		
            }
            return null;
        }

        public void  PlayEffect(GameObject particleResource, Vector3 pos)
        {
      

            if (particleResource != null)
            {


                var goP = (GameObject)Instantiate(particleResource, pos, Quaternion.identity);

                var ps = goP.GetComponent<ParticleSystem>();

                ps.Play();

         
                Destroy(goP, ps.duration);

            }
       
        }
        //This is for a particle resource with no looping
       
    }

