using UnityEngine;
using UnityEngine.UIElements;


public class UIHeader : MonoBehaviour
{

    public static UIHeader instance { get; private set; }
    VisualElement m_Healthbar;
    public float displayTime = 4.0f;
    private VisualElement m_NPD;
    private float m_TimerDisplay;
    void Start()
    {
        UIDocument uIDocument = GetComponent<UIDocument>();
        m_NPD = uIDocument.rootVisualElement.Q<VisualElement>("NPCD");
        m_NPD.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
        m_Healthbar = uIDocument.rootVisualElement.Q<VisualElement>("Bar");
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float value)
    {
        m_Healthbar.style.width = Length.Percent(value * 100.0f);

    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NPD.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue()
    {
        m_NPD.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        instance = this;
    }

}
