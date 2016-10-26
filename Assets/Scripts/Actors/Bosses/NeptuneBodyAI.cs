using UnityEngine;
using System.Collections;

public class NeptuneBodyAI : NeptuneHeadAI {

	// Use this for initialization
	protected override void Start ()
    {
        InitializePoints();
    }
	
	// Update is called once per frame
	private void Update ()
    {
        MoveInTrajectory();
	}
}
