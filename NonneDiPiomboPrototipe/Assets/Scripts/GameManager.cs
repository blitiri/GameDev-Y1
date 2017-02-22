using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Transform[] TargetsArr=new Transform[4];

    void Awake()
    {
        TargetsArr = GameObject.Find("CameraRig").GetComponent<CameraControl>().m_Targets;
    }
	// Use this for initialization
	void Start ()
    {

        for(int i=0;i<TargetsArr.Length;i++)
        {
            
        }
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
