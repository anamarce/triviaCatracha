using Holoville.HOTween;
using UnityEngine;
using System.Collections;


    public class AudioManager : MonoBehaviour {
	
	
	
        public AudioSource Play(AudioClip clip, Transform emitter)
        {
            return Play(clip, emitter, 1f, 1f);
        }
        public AudioSource Play(AudioClip clip, Vector3 emitter, float duration,bool loop )
        {
            return Play(clip, emitter, 1f, 1f,duration,loop);
        }
        public AudioSource Play(AudioClip clip, Transform emitter, float volume)
        {
            return Play(clip, emitter, volume, 1f);
        }
	
        /// <summary>
        /// Plays a sound by creating an empty game object with an AudioSource
        /// and attaching it to the given transform (so it moves with the transform). Destroys it after it finished playing.
        /// </summary>
    
        public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch,bool loop=false)
        {
		
		
            if (clip!=null)
            {
                //Create an empty game object
                GameObject go = new GameObject ("Audio: " +  clip.name);
                go.transform.position = emitter.position;
                go.transform.parent = emitter;
	 
                //Create the source
                AudioSource source = go.AddComponent<AudioSource>();
                source.clip = clip;
                source.loop = loop;
                source.volume = volume;
                source.pitch = pitch;
                source.Play ();
                Destroy (go, clip.length);
                return source;
            }
            return null;
        }
	
        public AudioSource Play(string clipResource, Transform emitter, float volume, float pitch)
        {
		
		
            AudioClip clip;
            if (clipResource!=string.Empty)
            {
                clip = Resources.Load(clipResource) as AudioClip;
                //Create an empty game object
			
                GameObject go = new GameObject ("Audio: " +  clip.name);
                go.transform.position = emitter.position;
                go.transform.parent = emitter;
	 
                //Create the source
                AudioSource source = go.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play ();
                Destroy (go, clip.length);
                return source;
            }
            return null;
        }


        public AudioSource Play(string clipResource, Vector3 point, float volume, float pitch,bool autoDestroy=true,
            bool Loop=false)
        {
		
	
            AudioClip clip;
            if (clipResource!=string.Empty)
            {
                clip = Resources.Load(clipResource) as AudioClip;
                //Create an empty game object
                GameObject go = new GameObject("Audio: " + clip.name);
                go.transform.position = point;
	 
                //Create the source
                AudioSource source = go.AddComponent<AudioSource>();
			
                source.clip = clip;
                source.loop = Loop;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
                if (autoDestroy)
                    Destroy(go, clip.length);
                return source;
            }
            return null;
        }
        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
        {
		
	
		
            if (clip!=null)
            {
                //Create an empty game object
                GameObject go = new GameObject("Audio: " + clip.name);
                go.transform.position = point;
	 
                //Create the source
                AudioSource source = go.AddComponent<AudioSource>();
			
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
                Destroy(go, clip.length);
                return source;
            }
            return null;
        }
        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch,float duration,bool loop=false)
        {
		
		
            if (clip!=null)
            {
                //Create an empty game object
                GameObject go = new GameObject("Audio: " + clip.name);
                go.transform.position = point;
	 
                //Create the source
                AudioSource source = go.AddComponent<AudioSource>();
			
                source.clip = clip;
                source.volume = volume;
                source.loop = loop;
                source.pitch = pitch;
                source.Play();
			
                Destroy(go, duration);
                return source;
            }
            return null;
        }
        public AudioSource Play(AudioClip clip, Vector3 point)
        {
            return Play(clip, point, 1f, 1f);
        }
 
        public AudioSource Play(AudioClip clip, Vector3 point, float volume)
        {
            return Play(clip, point, volume, 1f);
        }



    }

