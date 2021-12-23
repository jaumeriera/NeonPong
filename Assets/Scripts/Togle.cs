using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Togle : MonoBehaviour
{
    Settings settings;
    Toggle m_Toggle;
    // Start is called before the first frame update
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleValueChanged(Toggle change)
    {
        print(m_Toggle.isOn);
        settings.SetPowerUpsActive(m_Toggle.isOn);
    }
}
