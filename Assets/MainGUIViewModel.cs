using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MainGUIViewModel : MonoBehaviour
{
    public UIDocument m_UIDocument;
    public Button m_UpButton;
    public Button m_DownButton;

    private void OnEnable()
    {
        m_UpButton = m_UIDocument.rootVisualElement.Q("upButton") as Button;
        m_DownButton = m_UIDocument.rootVisualElement.Q("downButton") as Button;

        if (m_UpButton != null)
        {
            Debug.Log("Up button found");
            m_UpButton.RegisterCallback<ClickEvent>(OnUpButtonClick);
        }

        if (m_DownButton != null)
        {
            Debug.Log("Down button found");
            m_DownButton.RegisterCallback<ClickEvent>(OnDownButtonClick);
        }
    }

    public void OnUpButtonClick(ClickEvent clickEvent)
    {
        Debug.Log("The Up Button has been clicked on.");
    }
    
    public void OnDownButtonClick(ClickEvent clickEvent)
    {
        Debug.Log("The Down Button has been clicked on.");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
