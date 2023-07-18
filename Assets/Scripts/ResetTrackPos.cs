using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrackPos : MonoBehaviour
{
    internal static ResetTrackPos instance = null;
    [SerializeField] internal GameObject track = null;
    [SerializeField] private GameObject track1 = null;
    [SerializeField] private GameObject track2 = null;
    [SerializeField] private GameObject track3 = null;

    private Vector3 trackPos = Vector3.zero;
    private Vector3 stepPos = Vector3.zero;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trackPos = track.transform.localPosition;
        stepPos = new Vector3(0, 0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupTrack()
    {
        track.transform.position = trackPos;
        track1.transform.position = track.transform.position + stepPos;
        track2.transform.position = track1.transform.position + stepPos;
        track3.transform.position = track2.transform.position + stepPos;
    }


}
