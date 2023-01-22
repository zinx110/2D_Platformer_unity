
using UnityEngine;
using UnityEngine.UI;
public class StatusIndicator : MonoBehaviour
{

    [SerializeField]
    private RectTransform healthbarRect;

    [SerializeField]
    private Text healthText;

    private void Start()
    {
        if (healthbarRect == null)
        {
            Debug.LogWarning("STATUS INDICATOR: There is no healthBar object referrenced.");
        }
        if (healthText == null)
        {
            Debug.LogWarning("STATUS INDICATOR: There is no healthText object referrenced.");

        }
    }

    public void SetHealth(int _cur, int _max) 
    {
        float _value =(float) _cur / _max;

        healthbarRect.localScale =  new Vector3(_value, healthbarRect.localScale.y, healthbarRect.localScale.z);
        healthText.text = _cur + "/" + _max + " HP";

        
    }


}
