using UnityEngine;
using System.Collections;

public class ScreamScript : MonoBehaviour {

    SpriteRenderer enemyRenderer;
    int visible;
    // Use this for initialization
    void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        visible = 0;

        //Light2D.RegisterEventListener(LightEventListenerType.OnStay, OnStay);
        Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
        Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);
    }

    void OnLightEnter(Light2D _light, GameObject _go)
    {
        if (_go.GetInstanceID() == gameObject.GetInstanceID())
        {
            if (visible <= 0)
            {
                Debug.Log("Object Entered Light");

                enemyRenderer.enabled = true;
            }
            visible += 1;
        }
    }

    void OnLightExit(Light2D _light, GameObject _go)
    {
        if (_go.GetInstanceID() == gameObject.GetInstanceID())
        {
            visible -= 1;
            if (visible <= 0)
            {
                Debug.Log("Scream: Object Exited Light");

                enemyRenderer.enabled = false;

                Destroy(gameObject);
            }

        }
    }

    void OnDestroy()
    {
        /* (!) Make sure you unregister your events on destroy. If you do not
         * you might get strange errors (!) */

        //Light2D.UnregisterEventListener(LightEventListenerType.OnStay, OnLightStay);
        Light2D.UnregisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
        Light2D.UnregisterEventListener(LightEventListenerType.OnExit, OnLightExit);
    }
}

