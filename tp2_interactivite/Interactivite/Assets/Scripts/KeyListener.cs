using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyListener : MonoBehaviour
{
    UnityEvent m_MyEvent = new UnityEvent();


    void Start()
    {
        //Add a listener to the new Event. Calls MyAction method when invoked
        m_MyEvent.AddListener(Set);
    }

    void Update()
    {
        if (Input.anyKeyDown && m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }
    }

    void Set()
    {
        Debug.Log(Input.inputString);
    }
}