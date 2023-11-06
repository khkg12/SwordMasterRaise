using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSlot : MonoBehaviour
{
    public SoldierUI owner;
    public int id;
    [SerializeField] Image equipImage;
    [SerializeField] Image rockImage;
    [SerializeField] Button selectBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] TextMeshProUGUI explanationText;    
    [SerializeField] TextMeshProUGUI goldText;
    SoldierInfo soldierInfo;    
    
    public bool IsHave
    {
        get => soldierInfo.isHave;
        set
        {
            soldierInfo.isHave = value;            
            selectBtn.gameObject.SetActive(IsHave);
            buyBtn.gameObject.SetActive(!IsHave);
            rockImage.gameObject.SetActive(!IsHave);
            goldText.gameObject.SetActive(!IsHave);
        }
    }    
    public int RequireGold
    {
        get => requireGold;
        set
        {
            requireGold = value;
            goldText.text = $"������ ��ǥ : {requireGold}";
        }
    }
    private int requireGold;

    public string Explanation
    {
        get => explanation;
        set
        {
            explanation = value;
            explanationText.text = explanation; // ���⼭ ���ڼ���� �߶� �ٹٲ��ϴ� �� �ص��ɵ�, json���� ���ϴϱ�
        }
    }
    private string explanation;

    public bool IsEquip
    {
        get => isEquip;
        set
        {
            isEquip = value;
            equipImage.gameObject.SetActive(isEquip);
        }
    }
    private bool isEquip;    

    public void Select() // ������ư Ŭ�� �� ȣ��
    {
        GameManager.instance.SelectSoldierId = id;
        owner.DisableEquip();
        IsEquip = true;        
    }
    public void Buy() // ���Ź�ư Ŭ�� �� ȣ��
    {
        if(DataManager.instance.Badge >= RequireGold)
        {
            DataManager.instance.Badge -= RequireGold;
            IsHave = true;
        }
    }

    public void SetSoldier(SoldierInfo setSoldierInfo)
    {
        soldierInfo = setSoldierInfo;        
        if(soldierInfo != null)
        {
            id = soldierInfo.id;    
            IsHave = soldierInfo.isHave;
            RequireGold = soldierInfo.requireGold;
            Explanation = soldierInfo.explanation;
        }        
    }    
}
