using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIKTouchScript : MonoBehaviour {

    [SerializeField] private float _handDistance;
    [SerializeField] private float _handOffsetDistance;

    [HideInInspector] public Transform LeftHand;
    [HideInInspector] public Transform RightHand;
    private Transform _leftHandRest;
    private Transform _rightHandRest;

    private BasePlayerScript _player;

	// Use this for initialization
	void Awake () {
        LeftHand = transform.Find("Left");
        RightHand = transform.Find("Right");
        _leftHandRest = transform.Find("LeftRest");
        _rightHandRest = transform.Find("RightRest");
        _player = transform.parent.GetComponent<BasePlayerScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit leftHit;
        if (Physics.Raycast(transform.position + (transform.right * _handOffsetDistance), transform.forward, out leftHit, _handDistance))
            {
            LeftHand.transform.position = Vector3.Lerp(LeftHand.position, leftHit.point, 0.2f);
            }
        else
            {
            LeftHand.transform.position = Vector3.Lerp(LeftHand.position, _leftHandRest.position, 0.2f);
            }

        RaycastHit rightHit;
        if (Physics.Raycast(transform.position - (transform.right * _handOffsetDistance), transform.forward, out rightHit, _handDistance))
            {
            RightHand.transform.position = rightHit.point;
            }
        else
            {
            RightHand.transform.position = Vector3.Lerp(RightHand.position, _rightHandRest.position, 0.2f);
            }

        _player.IsTouching = Vector3.Distance(LeftHand.position, _leftHandRest.position) > 0.03f || Vector3.Distance(RightHand.position, _rightHandRest.position) > 0.03f;
        Debug.Log(Vector3.Distance(LeftHand.position, _leftHandRest.position) > 0.03f || Vector3.Distance(RightHand.position, _rightHandRest.position) > 0.03f);
        }
}
