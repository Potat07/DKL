
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public bool PLAYERTURN = true;
    public GameObject Items_Panel;
    public GameObject Skills_Panel;
    public GameObject DialoguePanel;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject Api_Shield;
    public GameObject Batu_Block;
    public GameObject Logam_Shield;
    public GameObject Udara_Shield;
    public GameObject Es_Block;

    public Transform playerBattleStation; 
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;
    

    public Text dialogueText;
    public Text Slot1_Text;
    public Text Slot2_Text;
    public Text Slot3_Text;
    public Text Slot4_Text;
    public Text Slot5_Text;
    public Text Slot6_Text;
    public Text Health_Tonic_Text;
    public Text Stamina_Tonic_Text;
    public TMP_Text Block_Status_Text;



    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerBattle = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerBattle.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        if (playerUnit.unitLevel >= 1)
        {
            Slot1_Text.text = "api burn";
            Slot2_Text.text = "Es stab";
            Slot3_Text.text = "Batu throw";
            Slot4_Text.text = "logam throw";
            Slot5_Text.text = "udara drown";
            Slot6_Text.text = "sembuh";
        }
        if (playerUnit.unitLevel >= 2)
        {
            Slot1_Text.text = "apian burn";
            Slot2_Text.text = "esan stab";
            Slot3_Text.text = "batuan throw";
            Slot4_Text.text = "logaman throw";
            Slot5_Text.text = "udaraan drown";
            Slot6_Text.text = "sembuhan";
        }
        if (playerUnit.unitLevel >= 3)
        {
            Slot1_Text.text = "apiar burn";
            Slot2_Text.text = "Esar stab";
            Slot3_Text.text = "Batuar throw";
            Slot4_Text.text = "logamar throw";
            Slot5_Text.text = "udaraar drown";
            Slot6_Text.text = "sembuhar";
        }

        if (playerUnit.Number_of_Health_Tonics == 1)
        {
            Health_Tonic_Text.text = "Health Tonic x1";
        }
        if (playerUnit.Number_of_Health_Tonics == 2)
        {
            Health_Tonic_Text.text = "Health Tonic x2";
        }
        if (playerUnit.Number_of_Health_Tonics == 3)
        {
            Health_Tonic_Text.text = "Health Tonic x3";
        }
        if (playerUnit.Number_of_Health_Tonics == 4)
        {
            Health_Tonic_Text.text = "Health Tonic x4";
        }
        if (playerUnit.Number_of_Health_Tonics == 5)
        {
            Health_Tonic_Text.text = "Health Tonic x5";
        }
        if(playerUnit.Number_of_Health_Tonics == 0)
        {
            Health_Tonic_Text.text = "Health Tonic x0";
        }
        if (playerUnit.Number_of_Stamina_Tonics == 1)
        {
            Stamina_Tonic_Text.text = "Stamina Tonic x1";
        }
        if (playerUnit.Number_of_Stamina_Tonics == 2)
        {
            Stamina_Tonic_Text.text = "Stamina Tonic x2";
        }
        if (playerUnit.Number_of_Stamina_Tonics == 3)
        {
            Stamina_Tonic_Text.text = "Stamina Tonic x3";
        }
        if (playerUnit.Number_of_Stamina_Tonics == 4)
        {
            Stamina_Tonic_Text.text = "Stamina Tonic x4";
        }
        if (playerUnit.Number_of_Stamina_Tonics == 5)
        {
            Stamina_Tonic_Text.text = "Stamina Tonic x5";
        }
        if(playerUnit.Number_of_Stamina_Tonics == 0)
        {
            Stamina_Tonic_Text.text = "Stamina Tonic x0";
        }
        //Max number of items in the inventory is 5
        //Theres most likely a more efficient way but I cant think of one

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }
    public void Api_Shield_Button()
    {
        playerUnit.Blocking_status = 1;
        Block_Status_Text.text = "Api Shield Selected";

    }

    public void Batu_Block_Button()
    {
        playerUnit.Blocking_status = 2;
        Block_Status_Text.text = "Batu Block Selected";

    }


    public void Logam_Shield_Button()
    {
        playerUnit.Blocking_status = 3;
        Block_Status_Text.text = "Logam Shield Selected";
    }

    public void Udara_Shield_Button()
    {
        playerUnit.Blocking_status = 4;
        Block_Status_Text.text = "Udara Shield Selected";
    }


    public void Es_Block_Button()
    {
        playerUnit.Blocking_status = 5;
        Block_Status_Text.text = "Es Shield Selected";
    }
    IEnumerator PlayerKecilApiAttack()
    {
        if (enemyUnit.ApiWeakness == 1)
        {
            dialogueText.text = "Enemy weak to Api attack";
            playerUnit.magic = 25;

        }
        if (enemyUnit.ApiWeakness == 2)
        {
            playerUnit.magic = 15;
            dialogueText.text = "Enemy strong to Api attack";
        }
        if (enemyUnit.ApiWeakness == 3)
        {
            dialogueText.text = "Enemy invunerable to Api attack";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "The burn attack does 0 damage ";
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
        if (enemyUnit.ApiWeakness == 4)
        {
            playerUnit.magic = 20;
            //Enemy normal to Api attack
        }
        yield return new WaitForSeconds(2f);
        float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1f,1.1f);
        bool isDead = enemyUnit.TakeDamage(damageAmount); 

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You succcesfully burnt the enemy! It dealt " + (damageAmount) + " damage!";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


    }
    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage / (2 * (enemyUnit.defense / playerUnit.damage)));


        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful! It dealt " + (playerUnit.damage / (2 * (enemyUnit.defense / playerUnit.damage)) + " damage!");

        yield return new WaitForSeconds(2f);
         
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    public void OnApiAttackButton()
    {
        if (PLAYERTURN == true)
        {
            DialoguePanel.SetActive(true);
            Skills_Panel.SetActive(false);

            if (playerUnit.unitLevel >= 1)
            {
                Debug.Log(playerUnit.CurrentStamina);
                if (playerUnit.CurrentStamina >= 4)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 4;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    Debug.Log(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerKecilApiAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }

            }
            if (playerUnit.unitLevel >= 2)
            {
                if (playerUnit.CurrentStamina >= 8)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 8;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerSedangApiAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 3)
            {
                if (playerUnit.CurrentStamina >= 12)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 12;
                    StartCoroutine(PlayerBesarApiAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
        }
    }
    public void OnEsAttackButton()
    {
        if (PLAYERTURN == true)
        {
            DialoguePanel.SetActive(true);
            Skills_Panel.SetActive(false);
            if (playerUnit.unitLevel >= 1) 
            { 
                if(playerUnit.CurrentStamina >= 4) 
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 4;
                    StartCoroutine(PlayerKecilEsAttack());
                    {

                    }
                }
                else 
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            
            }

            if (playerUnit.unitLevel >= 2)
            {
                if (playerUnit.CurrentStamina >= 8)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 8;
                    StartCoroutine(PlayerSedangEsAttack());
                    {

                    } 
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 3)
            {
                if (playerUnit.CurrentStamina >= 12)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 12;
                    StartCoroutine(PlayerBesarEsAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
               
            }
        }
    }
    IEnumerator EnemyTurn()
    {
         
        int SelectAttack = Random.Range(2,3);
        Debug.Log(SelectAttack);
        if (SelectAttack == 1f) 
        {

            dialogueText.text = enemyUnit.unitName + " attacks!";

            yield return new WaitForSeconds(1f);
            bool isDead = false;
            if (enemyUnit.agilty >= playerUnit.agilty)
            {
                isDead = playerUnit.TakeDamage(enemyUnit.damage / (2 * (playerUnit.defense / enemyUnit.damage)));
                dialogueText.text = enemyUnit.unitName + ("dealt " + enemyUnit.damage / (2 * (playerUnit.defense / enemyUnit.damage)) + " damage!");
            }
            else
            {
                isDead = playerUnit.TakeDamage(enemyUnit.damage / (2 * (playerUnit.defense / enemyUnit.damage) * 2));
                dialogueText.text = ("You dodged the attack it only grazed you! It dealt " + enemyUnit.damage / (2 * (playerUnit.defense / enemyUnit.damage) * 2) + " damage!");
                


            }

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();

            }
            else
            {
                Block_Status_Text.text = ("  ");
                PLAYERTURN = true;
                state = BattleState.PLAYERTURN;
                PlayerTurn();

            }
         
        }

        if (SelectAttack == 2f)
        {
            if (playerUnit.Blocking_status == 1)
            {
                dialogueText.text = " The enemy uses fire but your shield blocks it";
                yield return new WaitForSeconds(2f);
                Block_Status_Text.text = ("  ");
                playerUnit.Blocking_status = 0;
                PLAYERTURN = true;
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else
            {
                StartCoroutine(EnemyFireAttack());
            }
          

        }

        if (SelectAttack == 3f)
        {
            if (playerUnit.Blocking_status == 2)
            {
                dialogueText.text = " The enemy uses rock but your shield blocks it";
                Block_Status_Text.text = ("  ");
                PLAYERTURN = true;
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else 
            {
                StartCoroutine (EnemyRockAttack());
            }
            

    }
    IEnumerator EnemyFireAttack()
    {
        yield return new WaitForSeconds(2f);
        dialogueText.text = enemyUnit.unitName + " attacks with fire!";
        if (enemyUnit.agilty >= playerUnit.agilty)
        {
            float damageAmount = enemyUnit.magic / (2 * (playerUnit.defense / enemyUnit.magic)) * Random.Range(1f, 1.1f);
            bool isDead = playerUnit.TakeDamage(damageAmount);
            dialogueText.text = enemyUnit.unitName + ("dealt " + damageAmount + " damage!");
        }
        else
        {
            float damageAmount = enemyUnit.magic / (2 * (playerUnit.defense / enemyUnit.magic) * 2) * Random.Range(1f, 1.1f);
            bool isDead = playerUnit.TakeDamage(damageAmount);
            dialogueText.text = ("You dodged the attack it only grazed you! It dealt " + damageAmount + " damage!");



        }

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (playerUnit.currentHP <= 0)
        {
            state = BattleState.LOST;
            EndBattle();

        }
        else
        {
            Block_Status_Text.text = ("  ");
            PLAYERTURN = true;
            state = BattleState.PLAYERTURN;
            PlayerTurn();

        }
    }
    IEnumerator EnemyRockAttack() 
    {
            dialogueText.text = enemyUnit.unitName + " attacks with rocks";

            yield return new WaitForSeconds(1f);
            if (enemyUnit.agilty >= playerUnit.agilty)
            {
                float damageAmount = enemyUnit.magic / (2 * (playerUnit.defense / enemyUnit.magic)) * Random.Range(1f, 1.3f);
                bool isDead = playerUnit.TakeDamage(damageAmount);
                dialogueText.text = enemyUnit.unitName + ("dealt " + damageAmount + " damage!");
            }
            else
            {
                float damageAmount = enemyUnit.magic / (2 * (playerUnit.defense / enemyUnit.magic) * 2) * Random.Range(1f, 1.3f);
                bool isDead = playerUnit.TakeDamage(damageAmount);
                dialogueText.text = ("You dodged the attack it only grazed you! It dealt " + damageAmount + " damage!");



            }

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (playerUnit.currentHP <= 0)
            {
                state = BattleState.LOST;
                EndBattle();

            }
            else
            {
                Block_Status_Text.text = ("  ");
                PLAYERTURN = true;
                state = BattleState.PLAYERTURN;
                PlayerTurn();

            }

        }

    }
    public void EndBattle()
    {
        if (state == BattleState.WON)
        {
            StartCoroutine(EndBattleWON());

        }
        else if (state == BattleState.LOST)
        {

            StartCoroutine(EndBattleLOST());
        };
    }
    IEnumerator EndBattleWON()
    {
        dialogueText.text = "You won!";
        yield return new WaitForSeconds(2f);
        gameManager.gameOver();
    }
    IEnumerator EndBattleLOST()
    {
        dialogueText.text = "You were defeated!";
        yield return new WaitForSeconds(2f);
        gameManager.gameOver();
    }
    public void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        if (PLAYERTURN == true)
        {
            PLAYERTURN = false;
            StartCoroutine(OnAttack());

        }








        IEnumerator OnAttack()
        {
            // if (state != BattleState.PLAYERTURN)
            if ((playerUnit.agilty >= enemyUnit.agilty))
            {
                StartCoroutine(PlayerAttack());
            }
            else
            {
                bool isDead = enemyUnit.TakeDamage(playerUnit.damage / (2 * (enemyUnit.defense / playerUnit.damage) * 2));
                dialogueText.text = "Your attack only grazed the enemy dealing, " + playerUnit.damage / (2 * (enemyUnit.defense / playerUnit.damage) * 2) + " damage!";
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());

            }

        }


    }
    IEnumerator PlayerSedangApiAttack()
    {
        if (playerUnit.unitLevel >= 2)
        {
            if (enemyUnit.ApiWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Api attack";
                playerUnit.magic = 35;

            }
            if (enemyUnit.ApiWeakness == 2)
            {
                playerUnit.magic = 25;
                dialogueText.text = "Enemy strong to Api attack";
            }
            if (enemyUnit.ApiWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Api attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);

                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.ApiWeakness == 4)
            {
                playerUnit.magic = 30;
                //Enemy normal to Api attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1f, 1.1f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succcesfully burnt the enemy! It dealt " + (damageAmount) + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerBesarApiAttack()
    {
        if (playerUnit.unitLevel >= 3)
        {
            if (enemyUnit.ApiWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Api attack";
                playerUnit.magic = 45;

            }
            if (enemyUnit.ApiWeakness == 2)
            {
                playerUnit.magic = 35;
                dialogueText.text = "Enemy strong to Api attack";
            }
            if (enemyUnit.ApiWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Api attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.ApiWeakness == 4)
            {
                playerUnit.magic = 40;
                //Enemy normal to Api attack
            }
             yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1f, 1.1f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succcesfully burnt the enemy! It dealt " + (damageAmount) + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator PlayerKecilEsAttack()
    {
        if (playerUnit.unitLevel >= 1)
        {

            
            playerUnit.CurrentStamina = playerUnit.CurrentStamina - 3;
            playerHUD.SetStamina(playerUnit.CurrentStamina);
            Debug.Log(playerUnit.CurrentStamina);
            if (enemyUnit.EsWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Es attack";
                playerUnit.magic = 25;

            }
            if (enemyUnit.EsWeakness == 2)
            {
                playerUnit.magic = 15;
                dialogueText.text = "Enemy strong to Es attack";
            }
            if (enemyUnit.EsWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Es attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.EsWeakness == 4)
            {
                playerUnit.magic = 20;
                //Enemy normal to Es attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1.1f, 1.2f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succesfully stabbed the enemy with ice! It dealt " + (damageAmount) + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerSedangEsAttack()
    {
        if (playerUnit.unitLevel >= 2)
        {
            if (enemyUnit.EsWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Es attack";
                playerUnit.magic = 35;

            }
            if (enemyUnit.EsWeakness == 2)
            {
                playerUnit.magic = 25;
                dialogueText.text = "Enemy strong to Es attack";
            }
            if (enemyUnit.EsWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Es attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.EsWeakness == 4)
            {
                playerUnit.magic = 30;
                //Enemy normal to Es attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1.1f, 1.2f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succesfully stabbed the enemy with ice! It dealt " + (damageAmount) + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerBesarEsAttack()
    {
        if (enemyUnit.EsWeakness == 1)
        {
            dialogueText.text = "Enemy weak to Es attack";
            playerUnit.magic = 45;

        }
        if (enemyUnit.EsWeakness == 2)
        {
            playerUnit.magic = 35;
            dialogueText.text = "Enemy strong to Es attack";
        }
        if (enemyUnit.EsWeakness == 3)
        {
            dialogueText.text = "Enemy invunerable to Es attack";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "The burn attack does 0 damage ";
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
        if (enemyUnit.EsWeakness == 4)
        {
            playerUnit.magic = 40;
            //Enemy normal to Es attack
        }
        yield return new WaitForSeconds(2f);
        float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1.1f, 1.2f);
        bool isDead = enemyUnit.TakeDamage(damageAmount);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You succesfully stabbed the enemy with ice! It dealt " + (damageAmount) + " damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON; 
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
    public void OnBatuAttackButton()
    {
        if (PLAYERTURN == true)
        {
            DialoguePanel.SetActive(true);
            Skills_Panel.SetActive(false);

            if (playerUnit.unitLevel >= 1)
            {
                if (playerUnit.CurrentStamina >= 4)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 4;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    Debug.Log(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerKecilBatuAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 2)
            {
                if (playerUnit.CurrentStamina >= 8)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 8;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerSedangBatuAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
                PLAYERTURN = false;

            }
            if (playerUnit.unitLevel >= 3)
            {
                if (playerUnit.CurrentStamina >= 12)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 12;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerBesarBatuAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
        }
    }
    IEnumerator PlayerKecilBatuAttack()
    {
        if (playerUnit.unitLevel >= 1)
        {
            if (enemyUnit.BatuWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Batu attack";
                playerUnit.magic = 25;

            }
            if (enemyUnit.BatuWeakness == 2)
            {
                playerUnit.magic = 15;
                dialogueText.text = "Enemy strong to Batu attack";
            }
            if (enemyUnit.BatuWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Batu attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.LogamWeakness == 4)
            {
                playerUnit.magic = 20;
                //Enemy normal to Batu attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1f, 1.3f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succesfully threw a boulder at the enemy! It dealt" + (damageAmount) + " damage";
             yield return new WaitForSeconds(2f);


            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerSedangBatuAttack()
    {
        if (playerUnit.unitLevel >= 2)
        {
            if (enemyUnit.BatuWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Batu attack";
                playerUnit.magic = 35;

            }
            if (enemyUnit.BatuWeakness == 2)
            {
                playerUnit.magic = 25;
                dialogueText.text = "Enemy strong to Batu attack";
            }
            if (enemyUnit.BatuWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Batu attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.LogamWeakness == 4)
            {
                playerUnit.magic = 30;
                //Enemy normal to Batu attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1f, 1.3f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succesfully threw a boulder at the enemy! It dealt" + (damageAmount) + " damage";
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerBesarBatuAttack()
    {
        if (enemyUnit.BatuWeakness == 1)
        {
            dialogueText.text = "Enemy weak to Batu attack";
            playerUnit.magic = 45;

        }
        if (enemyUnit.BatuWeakness == 2)
        {
            playerUnit.magic = 35;
            dialogueText.text = "Enemy strong to Batu attack";
        }
        if (enemyUnit.BatuWeakness == 3)
        {
            dialogueText.text = "Enemy invunerable to Batu attack";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "The burn attack does 0 damage ";
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
        if (enemyUnit.LogamWeakness == 4)
        {
            playerUnit.magic = 40;
            //Enemy normal to Batu attack
        }
        yield return new WaitForSeconds(2f);
        float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1f, 1.3f);
        bool isDead = enemyUnit.TakeDamage(damageAmount);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You succesfully threw a boulder at the enemy! It dealt" + (damageAmount) + " damage";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
    public void OnLogamAttackButton()
    {
        if (PLAYERTURN == true)
        {
            DialoguePanel.SetActive(true);
            Skills_Panel.SetActive(false);
            if (playerUnit.unitLevel >= 1)
            {
                if (playerUnit.CurrentStamina >= 4)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 4;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerKecilLogamAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 2)
            {
                if (playerUnit.CurrentStamina >= 8)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 8;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerSedangLogamAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 3)
            {
                if (playerUnit.CurrentStamina >= 12)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 12;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerBesarLogamAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }

            }
        }
    }
    IEnumerator PlayerKecilLogamAttack()
    {
        if (playerUnit.unitLevel >= 1)
        {
            if (enemyUnit.LogamWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Logam attack";
                playerUnit.magic = 25;

            }
            if (enemyUnit.LogamWeakness == 2)
            {
                playerUnit.magic = 15;
                dialogueText.text = "Enemy strong to Logam attack";
            }
            if (enemyUnit.LogamWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Logam attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.LogamWeakness == 4)
            {
                playerUnit.magic = 20;
                //Enemy normal to Logam attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1.2f, 1.3f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succesfullly threw a metal ball at the enemy! It dealt " + (damageAmount) + " damage!";
            yield return new WaitForSeconds(2f);
            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerSedangLogamAttack()
    {
        if (playerUnit.unitLevel >= 2)
        {
            if (enemyUnit.LogamWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Logam attack";
                playerUnit.magic = 35;

            }
            if (enemyUnit.LogamWeakness == 2)
            {
                playerUnit.magic = 25;
                dialogueText.text = "Enemy strong to Logam attack";
            }
            if (enemyUnit.LogamWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Logam attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.LogamWeakness == 4)
            {
                playerUnit.magic = 30;
                //Enemy normal to Logam attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1.2f, 1.3f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You succesfullly threw a metal ball at the enemy! It dealt " + (damageAmount) + " damage!";
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerBesarLogamAttack()
    {
        if (enemyUnit.LogamWeakness == 1)
        {
            dialogueText.text = "Enemy weak to Logam attack";
            playerUnit.magic = 45;

        }
        if (enemyUnit.LogamWeakness == 2)
        {
            playerUnit.magic = 35;
            dialogueText.text = "Enemy strong to Logam attack";
        }
        if (enemyUnit.LogamWeakness == 3)
        {
            dialogueText.text = "Enemy invunerable to Logam attack";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "The burn attack does 0 damage ";
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
        if (enemyUnit.LogamWeakness == 4)
        {
            playerUnit.magic = 40;
            //Enemy normal to Logam attack
        }
        yield return new WaitForSeconds(2f);
        float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(1.2f, 1.3f);
        bool isDead = enemyUnit.TakeDamage(damageAmount);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You succesfullly threw a metal ball at the enemy! It dealt " + (damageAmount) + " damage!";
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
    public void OnUdaraAttackButton()
    {
        if (PLAYERTURN == true)
        {
            DialoguePanel.SetActive(true);
            Skills_Panel.SetActive(false);

            if (playerUnit.unitLevel >= 1)
            {
                if (playerUnit.CurrentStamina >= 4)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 4;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerKecilUdaraAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 2)
            {
                if (playerUnit.CurrentStamina >= 8)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 8;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerSedangUdaraAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 3)
            {
                if (playerUnit.CurrentStamina >= 12)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 12;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerBesarUdaraAttack());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
        }
    }
    IEnumerator PlayerKecilUdaraAttack()
    {
        if (playerUnit.unitLevel >= 1)
        {

            if (enemyUnit.UdaraWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Udara attack";
                playerUnit.magic = 25;

            }
            if (enemyUnit.UdaraWeakness == 2)
            {
                playerUnit.magic = 15;
                dialogueText.text = "Enemy strong to Udara attack";
            }
            if (enemyUnit.UdaraWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Udara attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.LogamWeakness == 4)
            {
                playerUnit.magic = 20;
                //Enemy normal to Udara attack
            }
            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(0.9f, 1.6f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You successfuly made the enemy suffocate! It dealt " + (damageAmount) + " damage!";
            yield return new WaitForSeconds(2f);


            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerSedangUdaraAttack()
    {
        if (playerUnit.unitLevel >= 2)
        {
            if (enemyUnit.UdaraWeakness == 1)
            {
                dialogueText.text = "Enemy weak to Udara attack";
                playerUnit.magic = 35;

            }
            if (enemyUnit.UdaraWeakness == 2)
            {
                playerUnit.magic = 25;
                dialogueText.text = "Enemy strong to Udara attack";
            }
            if (enemyUnit.UdaraWeakness == 3)
            {
                dialogueText.text = "Enemy invunerable to Udara attack";
                yield return new WaitForSeconds(1f);
                dialogueText.text = "The burn attack does 0 damage ";
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1f);
                StartCoroutine(EnemyTurn());
            }
            if (enemyUnit.LogamWeakness == 4)
            {
                playerUnit.magic = 30;
                //Enemy normal to Udara attack
                //I would have done else for all these weakness if statement but for some reason it would skip to else and ignore the if statements
            }

            yield return new WaitForSeconds(2f);
            float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(0.9f, 1.6f);
            bool isDead = enemyUnit.TakeDamage(damageAmount);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You successfuly made the enemy suffocate! It dealt " + (damageAmount) + " damage!";
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }
    IEnumerator PlayerBesarUdaraAttack()
    {
        if (enemyUnit.UdaraWeakness == 1)
        {
            dialogueText.text = "Enemy weak to Udara attack";
            playerUnit.magic = 45;

        }
        if (enemyUnit.UdaraWeakness == 2)
        {
            playerUnit.magic = 35;
            dialogueText.text = "Enemy strong to Udara attack";
        }
        if (enemyUnit.UdaraWeakness == 3)
        {
            dialogueText.text = "Enemy invunerable to Udara attack";
            yield return new WaitForSeconds(1f);
            dialogueText.text = "The burn attack does 0 damage ";
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
        if (enemyUnit.LogamWeakness == 4)
        {
            playerUnit.magic = 40;
            //Enemy normal to Udara attack
        }
        yield return new WaitForSeconds(2f);
        float damageAmount = playerUnit.magic / (2 * (enemyUnit.defense / playerUnit.magic)) * Random.Range(0.9f, 1.6f);
        bool isDead = enemyUnit.TakeDamage(damageAmount);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You successfuly made the enemy suffocate! It dealt " + (damageAmount) + " damage!";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }
    public void OnSembuhHealButton()
    {
        if (PLAYERTURN == true)
        {
            DialoguePanel.SetActive(true);
            Skills_Panel.SetActive(false);

            if (playerUnit.unitLevel >= 1)
            {
                if (playerUnit.CurrentStamina >= 5)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 5;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerKecilSembuh());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }

            }
            if (playerUnit.unitLevel >= 2)
            {
                if (playerUnit.CurrentStamina >= 10)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 10;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerSedangSembuh());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
            if (playerUnit.unitLevel >= 3)
            {
                if (playerUnit.CurrentStamina >= 15)
                {
                    PLAYERTURN = false;
                    playerUnit.CurrentStamina = playerUnit.CurrentStamina - 15;
                    playerHUD.SetStamina(playerUnit.CurrentStamina);
                    StartCoroutine(PlayerBesarSembuh());
                    {

                    }
                }
                else
                {
                    dialogueText.text = "You dont have enough stamina";
                }
            }
        }
    }
    IEnumerator PlayerKecilSembuh()
    {
        playerUnit.currentHP = playerUnit.currentHP + playerUnit.maxHP * 0.33f;
        dialogueText.text = "You healed for " + playerUnit.maxHP * 0.33 + " HP";
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerSedangSembuh()
    {
        playerUnit.currentHP = playerUnit.currentHP + playerUnit.maxHP * 0.66f;
        dialogueText.text = "You healed for " + playerUnit.maxHP * 0.66 + " HP";
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());


   

    }
    IEnumerator PlayerBesarSembuh()
    {
        playerUnit.currentHP = playerUnit.currentHP + playerUnit.maxHP;
        dialogueText.text = "You healed to max HP";
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }

    IEnumerator StaminaTonicUse()
    {
        DialoguePanel.SetActive(true);
        Items_Panel.SetActive(false);
        playerUnit.CurrentStamina = playerUnit.CurrentStamina + 10;
        dialogueText.text = "You regained 10 stamina";
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }
    public void OnHealthTonicButton()
    {
        if(playerUnit.Number_of_Health_Tonics >= 1) 
        {
            PLAYERTURN = false;
            playerUnit.Number_of_Health_Tonics = playerUnit.Number_of_Health_Tonics - 1;                        
            if (playerUnit.Number_of_Health_Tonics == 1)
            {
                Health_Tonic_Text.text = "Health Tonic x1";
            }
            if (playerUnit.Number_of_Health_Tonics == 2)
            {
                Health_Tonic_Text.text = "Health Tonic x2";
            }
            if (playerUnit.Number_of_Health_Tonics == 3)
            {
                Health_Tonic_Text.text = "Health Tonic x3";
            }
            if (playerUnit.Number_of_Health_Tonics == 4)
            {
                Health_Tonic_Text.text = "Health Tonic x4";
            }
            if (playerUnit.Number_of_Health_Tonics == 5)
            {
                Health_Tonic_Text.text = "Health Tonic x5";
            }
            if(playerUnit.Number_of_Health_Tonics == 0)
            {
                Health_Tonic_Text.text = "Health Tonic x0";
            }
            StartCoroutine(HealthTonicUse());
        }
        else 
        {
            DialoguePanel.SetActive(true);
            Items_Panel.SetActive(false);
            dialogueText.text = "You dont have any Health tonics";
        }
 

    }

    IEnumerator HealthTonicUse()
    {
        DialoguePanel.SetActive(true);
        Items_Panel.SetActive(false);
        playerUnit.currentHP = playerUnit.currentHP + 10;
        dialogueText.text = "You regained 10 health";
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
    }

public void OnStaminaTonicButton()
{
    if (playerUnit.Number_of_Stamina_Tonics >= 1)
    {
            PLAYERTURN = false;
            playerUnit.Number_of_Stamina_Tonics = playerUnit.Number_of_Stamina_Tonics - 1;
            if (playerUnit.Number_of_Stamina_Tonics == 1)
            {
                Stamina_Tonic_Text.text = "Stamina Tonic x1";
            }
            if (playerUnit.Number_of_Stamina_Tonics == 2)
            {
                Stamina_Tonic_Text.text = "Stamina Tonic x2";
            }
            if (playerUnit.Number_of_Stamina_Tonics == 3)
            {
                Stamina_Tonic_Text.text = "Stamina Tonic x3";
            }
            if (playerUnit.Number_of_Stamina_Tonics == 4)
            {
                Stamina_Tonic_Text.text = "Stamina Tonic x4";
            }
            if (playerUnit.Number_of_Stamina_Tonics == 5)
            {
                Stamina_Tonic_Text.text = "Stamina Tonic x5";
            }
            if(playerUnit.Number_of_Stamina_Tonics == 0)
            {
                Stamina_Tonic_Text.text = "Stamina Tonic x0";
            }
            StartCoroutine(StaminaTonicUse());
        }
    else
    {
        DialoguePanel.SetActive(true);
        Items_Panel.SetActive(false);
        dialogueText.text = "You dont have any Stamina tonics";
    }
    
}


}








