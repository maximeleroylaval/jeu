using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : Selectable {

    BaseEventData m_BaseEvent;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (IsHighlighted(m_BaseEvent)) {
            //Debug.Log("HERE");
        }
	}
}
