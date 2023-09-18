using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject hasProcessObjInterface;
    [SerializeField] private Image barProcess;
    private IHasProcess hasProcess;
    private void Start()
    {
        if (hasProcessObjInterface.TryGetComponent<IHasProcess>(out hasProcess))
        {
            hasProcess.OnProcessBar += HasProcess_OnProcessBar;
            barProcess.fillAmount = 0f;
            Hide();
        }
    }

    private void HasProcess_OnProcessBar(object sender, IHasProcess.OnPrecessChangerEventArgs e)
    {
        barProcess.fillAmount = e.processNormalized;
        if (barProcess.fillAmount > 0f && barProcess.fillAmount < 1f)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
