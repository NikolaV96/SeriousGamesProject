using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{

    public Sprite Check;
    public Sprite Cross;

    private Image m_Image;
    private bool m_AnimationCompleted;
    private float m_FillAmount;
    // Start is called before the first frame update
    void Start()
    {
        m_FillAmount = 0;
        m_AnimationCompleted = false;
        m_Image = gameObject.GetComponentInChildren<Image>();
        m_Image.fillAmount = m_FillAmount;
        CustomizeAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Correct()
    {
        CustomizeAnimation();
        m_Image.sprite = Check;

        StartCoroutine(FillingEffect());
    }

    public void Wrong()
    {
        CustomizeAnimation();
        m_Image.sprite = Cross;

        StartCoroutine(FillingEffect());
    }

    public void Clear()
    {
        m_FillAmount = 0;
        m_Image.fillAmount = m_FillAmount;
        m_AnimationCompleted = false;
    }

    public bool AnimationComplited()
    {
        return m_AnimationCompleted;
    }

    IEnumerator FillingEffect()
    {
        while(m_FillAmount < 1)
        {
            m_FillAmount += 0.05f;
            m_Image.fillAmount = m_FillAmount;

            yield return null;
        }
        m_AnimationCompleted = true;
    }

    private void CustomizeAnimation()
    {
        int fillMethod = 4;
        int origin = (int)Random.Range(0, 3);

        m_Image.fillMethod = (Image.FillMethod)fillMethod;
        m_Image.fillOrigin = origin;
    }
}
