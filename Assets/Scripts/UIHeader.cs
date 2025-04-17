using UnityEngine;
using UnityEngine.UIElements;


public class UIHeader : MonoBehaviour
{

    public static UIHeader instance { get; private set; }
    VisualElement m_Healthbar;
    void Start()
    {
        UIDocument uIDocument = GetComponent<UIDocument>();
        m_Healthbar = uIDocument.rootVisualElement.Q<VisualElement>("Bar");
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float value)
    {
        m_Healthbar.style.width = Length.Percent(value * 100.0f);

    }

    private void Awake()
    {
        instance = this;
    }

}
