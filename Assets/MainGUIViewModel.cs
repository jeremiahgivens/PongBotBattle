using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainGUIViewModel : MonoBehaviour
{
    public UIDocument m_ButtonDocument;
    public Button m_UIButton;

    private void OnEnable()
    {
        m_UIButton = m_ButtonDocument.rootVisualElement.Q("TestButton") as Button;

        if (m_UIButton != null)
        {
            Debug.Log("m_UIButton was found");
            m_UIButton.RegisterCallback<ClickEvent>(OnButtonClick);
        }
    }

    public void OnButtonClick(ClickEvent clickEvent)
    {
        Debug.Log("The UI Button has been clicked on.");
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
