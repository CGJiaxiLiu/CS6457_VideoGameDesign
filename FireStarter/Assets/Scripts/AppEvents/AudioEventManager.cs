using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;

    public AudioClip[] boxAudio = null;
    public AudioClip playerLandsAudio;
    public AudioClip explosionAudio;
    public AudioClip deathAudio;
    public AudioClip bombBounceAudio;
    public AudioClip jumpAudio;
    public AudioClip gruntAudio;
    public AudioClip punchAudio;

    private UnityAction<Vector3,float> boxCollisionEventListener;

    private UnityAction<Vector3, float> playerLandsEventListener;

    private UnityAction<Vector3> explosionEventListener;

    private UnityAction<Vector3> bombBounceEventListener;

    private UnityAction<Vector3> jumpEventListener;

    private UnityAction<GameObject> deathEventListener;


    void Awake()
    {

        boxCollisionEventListener = new UnityAction<Vector3,float>(boxCollisionEventHandler);

        playerLandsEventListener = new UnityAction<Vector3, float>(playerLandsEventHandler);

        explosionEventListener = new UnityAction<Vector3>(explosionEventHandler);

        bombBounceEventListener = new UnityAction<Vector3>(bombBounceEventHandler);

        jumpEventListener = new UnityAction<Vector3>(jumpEventHandler);

        deathEventListener = new UnityAction<GameObject>(deathEventHandler);
    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {

        EventManager.StartListening<BoxCollisionEvent, Vector3,float>(boxCollisionEventListener);
        EventManager.StartListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StartListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StartListening<BombBounceEvent, Vector3>(bombBounceEventListener);
        EventManager.StartListening<JumpEvent, Vector3>(jumpEventListener);
        EventManager.StartListening<DeathEvent, GameObject>(deathEventListener);

    }

    void OnDisable()
    {

        EventManager.StopListening<BoxCollisionEvent, Vector3,float>(boxCollisionEventListener);
        EventManager.StopListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StopListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StopListening<BombBounceEvent, Vector3>(bombBounceEventListener);
        EventManager.StopListening<JumpEvent, Vector3>(jumpEventListener);
        EventManager.StopListening<DeathEvent, GameObject>(deathEventListener);
    }


	
 

    void boxCollisionEventHandler(Vector3 worldPos, float impactForce)
    {
        //AudioSource.PlayClipAtPoint(this.boxAudio[0], worldPos);

        //const float halfSpeedRange = 0.2f;

        //EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

        //snd.audioSrc.clip = this.boxAudio[Random.Range(0,boxAudio.Length)];

        //snd.audioSrc.pitch = Random.Range(1f-halfSpeedRange, 1f+halfSpeedRange);

        //snd.audioSrc.minDistance = Mathf.Lerp(1f, 8f, impactForce /200f);
        //snd.audioSrc.maxDistance = 100f;

        //snd.audioSrc.Play();

        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.boxAudio[Random.Range(0, boxAudio.Length)];

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void playerLandsEventHandler(Vector3 worldPos, float collisionMagnitude)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {
            if (collisionMagnitude > 300f)
            {

                EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                snd.audioSrc.clip = this.playerLandsAudio;

                snd.audioSrc.minDistance = 5f;
                snd.audioSrc.maxDistance = 100f;

                snd.audioSrc.Play();

                if (collisionMagnitude > 500f)
                {

                    EventSound3D snd2 = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                    snd2.audioSrc.clip = this.gruntAudio;

                    snd2.audioSrc.minDistance = 5f;
                    snd2.audioSrc.maxDistance = 100f;

                    snd2.audioSrc.Play();
                }
            }


        }
    }

    void explosionEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {
            
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.explosionAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void bombBounceEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.bombBounceAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void jumpEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.jumpAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

    void deathEventHandler(GameObject go)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.deathAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }
}
