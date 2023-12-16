using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public GameObject Items_Panel;
    public GameObject Skills_Panel;
	public GameObject DialoguePanel;
	public GameObject Block_Panel;
    public Text nameText;
	public Text levelText;
	public Slider hpSlider;
    public Slider StaminaSlider;
	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
        StaminaSlider.value = unit.CurrentStamina;

	}

	public void SetHP(float hp)
	{
		hpSlider.value = hp;
	}
    public void SetStamina(float Stamina)
    {
        StaminaSlider.value = Stamina;
    }
    public void OnSkillsButton()
    {
        DialoguePanel.SetActive(false);
        Block_Panel.SetActive(false);
        Items_Panel.SetActive(false);
        Skills_Panel.SetActive(true);
    }
	public void OnItemsButton() 
	{
        DialoguePanel.SetActive(false);
        Block_Panel.SetActive(false);
        Skills_Panel.SetActive(false);
        Items_Panel.SetActive(true);
    }
	public void OnReturnButton() 
	{
        Block_Panel.SetActive(false);
        Skills_Panel.SetActive(false);
        Items_Panel.SetActive(false);
        DialoguePanel.SetActive(true);

    }
    public void OnBlockButton() 
    {
        DialoguePanel.SetActive(false);
        Skills_Panel.SetActive(false);
        Items_Panel.SetActive(false);
        Block_Panel.SetActive(true);
    }
    
}
