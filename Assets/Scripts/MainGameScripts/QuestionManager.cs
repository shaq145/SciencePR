using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour {

    [Header (" QUESTION LOGICS ")]
    public int counter;
    public int questionNumber;
    public int correctCount;
    public int correctRequired;

    [Header ( " AFTER BATTLE DIALOGUES " )]
    public GameObject battleResut;
    public int successDialogue;
    public int failedDialogue;
    public TextMeshProUGUI battleResultText;

    [Header (" BATTLE LOGICS ")]
    public float speed = 5f;
    public Transform playerAtkPos;
    public Transform enemyAtkPos;

    private Vector3 playerOriginalPos;
    private Vector3 enemyOriginalPos;

    public GameObject attackParticlePrefab;

    [Header (" QUESTIONS UI ")]
    public GameObject questionPanel;

    public TextMeshProUGUI questionNumberText;
    public TextMeshProUGUI questionText;

    public TextMeshProUGUI correctCountText;

    [Serializable]
    public class Questions {
        public int answerID;
        public string questionTitle;
        [TextArea ( 3, 2 )]
        public string question;
        public List<Choices> choices;
    }

    [Serializable]
    public class Choices {
        public int id;
        public string choiceName;
        public bool correctAnswer;
    }

    public List<Questions> questionList;
    public TextMeshProUGUI [] answerBtnText;

    [Header (" ANOTHER FOR BATTLE SEQUENCE ")]
    public GameObject player;
    public GameObject currentEnemy;

    public ParticleSystem correctText;
    public ParticleSystem wrongText;

    [Header ( " FOR HEALTHS " )]
    public int playerCurHealth;
    public int playerMaxHealth;

    public int enemyCurHealth;
    public int enemyMaxHealth;

    public PlayerHealth playerHealth;
    public PlayerHealth enemyHealth;

    public Animation battleAnimation;

    public DialogueManager dialogueManager;
    public BattleInitiator battleInitiator;

    // Start is called before the first frame update
    void Start () {
        dialogueManager = FindAnyObjectByType<DialogueManager> ();
        battleInitiator = FindAnyObjectByType<BattleInitiator> ();

        playerOriginalPos = player.transform.position;
        enemyOriginalPos = currentEnemy.transform.position;

        playerCurHealth = playerMaxHealth;
        enemyCurHealth = enemyMaxHealth;

        playerHealth.maxHealth = playerMaxHealth;
        enemyHealth.maxHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update() {
        questionNumberText.text = "Question " + questionNumber.ToString ();
        correctCountText.text = "Correct: " + correctCount;
        questionText.text = questionList [counter].question;

        playerHealth.curHealth = playerCurHealth;
        enemyHealth.curHealth = enemyCurHealth;

    }

    public void SetChoicesText () {
        questionPanel.SetActive ( true );
        for ( int i = 0; i < answerBtnText.Length; i++ ) {
            answerBtnText [ i ].text = questionList [ counter ].choices [ i ].choiceName;
        }
    }

    public void Answer ( int id ) {
        questionPanel.SetActive ( false );
        if ( questionList.Count - 1 >= counter ) {
            if ( id == questionList [ counter ].answerID ) {
                correctCount++;
                correctText.Play ();
                StartCoroutine ( PerformAttack ( player, currentEnemy, true, playerAtkPos.position, playerOriginalPos ) );
            } else {
                wrongText.Play ();
                StartCoroutine ( PerformAttack ( currentEnemy, player, false, enemyAtkPos.position, enemyOriginalPos ) );
            }
            counter++;
            questionNumber++;
        } 
    }

    public void StartQuestions () {
        questionPanel.SetActive ( true );
        SetChoicesText ();
    }

    public void CheckScore () {
        battleResut.SetActive ( true );
        if ( correctCount >= correctRequired ) {
            battleResultText.text = "You Win!";
            battleInitiator.enemy.SetActive ( false );
            dialogueManager.counter = successDialogue;
        } else {
            battleResultText.text = "You Lose!";
            dialogueManager.counter = failedDialogue;
        }
        
    }

    public void EndBattle () {
        battleAnimation.Play ( "FadeOutBattle" );
    }

    public void EndBattleTransition () {
        SceneManager.UnloadSceneAsync ( "BattleScene" );
        battleInitiator.stageParent.SetActive ( true );
        battleInitiator.gameObject.SetActive ( false );

        dialogueManager.StartDialogue ();
    }

    IEnumerator PerformAttack ( GameObject attacker, GameObject target, bool isEnemy, Vector3 targetPos, Vector3 originalPos ) {

        // Move to target
        while ( Vector3.Distance ( attacker.transform.position, targetPos ) > 0.1f ) {
            attacker.transform.position = Vector3.MoveTowards ( attacker.transform.position, targetPos, speed * Time.deltaTime );
            yield return null;
        }

        yield return new WaitForSeconds ( 0.3f );

        // Instantiate attack particle
        GameObject particle = Instantiate ( attackParticlePrefab, target.transform.position, Quaternion.identity );
        if ( isEnemy ) {
            enemyCurHealth--;
        } else {
            playerCurHealth--;
        }
        yield return new WaitForSeconds ( 0.5f ); // Wait for attack impact

        // You can call damage logic here if needed

        Destroy ( particle );

        // Move back
        while ( Vector3.Distance ( attacker.transform.position, originalPos ) > 0.1f ) {
            attacker.transform.position = Vector3.MoveTowards ( attacker.transform.position, originalPos, speed * Time.deltaTime );
            yield return null;
        }
        yield return new WaitForSeconds ( 0.3f );

        if ( ( questionList.Count - 1 >= counter ) && ( playerCurHealth != 0 && enemyCurHealth != 0 ) ) {
            SetChoicesText ();
        } else {
            questionPanel.SetActive ( false );
            CheckScore ();
        }
    }
}
