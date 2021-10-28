using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopUpTester : MonoBehaviour
{
    private void Start()
    {
        PopUp_Manager.Instance.PopUpPanelGoster();
        PopUp_Manager.Instance.SetTitle("SetTitle").SetMessage("SetMessage")
            .SetPozitifAction(pozitif).SetNegatifAction(negatif)
            .SetInputAction(input);
    }
    private void negatif()
    {
        Debug.Log("negatif");
    }
    private void pozitif()
    {
        Debug.Log("pozitif");
    }
    private void input(int s)
    {
        Debug.Log("input--" + (s == 5));
    }
}