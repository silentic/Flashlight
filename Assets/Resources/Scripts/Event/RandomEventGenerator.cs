using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class RandomEventGenerator : MonoBehaviour {
	
    int eventStatusCount;

	public GameObject ghostOnTheScreen;

    public float ghostScreenProb;
    public float ghostScreenDuration;
    public float ghostScreenDelay;
    private bool ghostScreenEnable;

	// Use this for initialization
	void Start () {
        InvokeRepeating("ActiveGhostOnTheScreen", 5, 1);
        ghostScreenEnable = false;
	}

    void ActiveGhostOnTheScreen()
    {
        if (ghostScreenEnable) return;
        float num = Random.Range(0.0f, 100);
        //Debug.Log ("TestDebug " + num);

        if (num < ghostScreenProb && EventActiveCondition())
        {		// random light off with XX% probability 
            eventStatusCount++;
            ghostScreenEnable = true;
            StartCoroutine(GhostAppearOnTheScreen());
        }
    }

    IEnumerator GhostAppearOnTheScreen() {
        ghostOnTheScreen.SetActive(true);
        yield return new WaitForSeconds(ghostScreenDuration);
        ghostOnTheScreen.SetActive(false);
        eventStatusCount--;
        yield return new WaitForSeconds(ghostScreenDelay);
        ghostScreenEnable = false;
    }

    bool EventActiveCondition()
    {
        return eventStatusCount == 0;
    }
}
