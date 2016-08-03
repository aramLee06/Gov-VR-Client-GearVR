using UnityEngine;
using UnityEngine.UI;

public class bl_DrawName : MonoBehaviour
{
    [HideInInspector]public string PlayerName;
    /// <summary>
    /// Object to follow UI
    /// </summary>
    public Transform Target;
    /// <summary>
    /// UI Prefabs
    /// </summary>
    public GameObject UIPrefab;
    [Space(5)]
    public Vector3 OffSet = Vector3.zero;
    public float Multipler = 0.002f;
    //Privates
    private Transform m_Transform = null;
    private Text m_Text = null;
    private GameObject cacheUI;
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        cacheUI = Instantiate(UIPrefab) as GameObject;
        Transform t = GameObject.Find("WorldCanvas").transform;
        cacheUI.transform.SetParent(t);
        m_Transform = cacheUI.transform;
        m_Text = cacheUI.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// When Player Die Destroy text
    /// </summary>
    void OnDisable()
    {
        if (m_Transform != null)
        {
            Destroy(m_Transform.gameObject);
        }
    }

    public void ShowUI(bool show)
    {
        if (cacheUI == null)
            return;

        cacheUI.SetActive(show);
    }
    /// <summary>
    /// 
    /// </summary>
    void OnGUI()
    {
        if (Target == null)
            return;
        if (m_Transform == null)
            return;
        if (!cacheUI.activeSelf)
            return;

        Camera c;
        if (Camera.main != null)
        {
            c = Camera.main;
        }
        else
        {
            if (Camera.current != null)
            {
                c = Camera.current;

            }
            else
            {
                return;
            }
        }
        //Calculate the size and position of ui

        float distance = Vector3.Distance(Target.position, c.transform.position);
        float d = (distance * 0.015f);
        if (d >= 1.20f)
        {
            d = 1.20f;
        }
        if (d <= 0.15f)
        {
            d = 0.15f;
        }
        //Follow Ui to the target in position and rotation
        m_Transform.localScale = new Vector3(d, d, d);
        OffSet.y = 2 + d;
        m_Transform.position = Target.position + OffSet;
        m_Transform.rotation = c.transform.rotation;
        PlayerName = gameObject.name;
        if (m_Text != null)
        {
            m_Text.text = PlayerName;
        }
    }
}