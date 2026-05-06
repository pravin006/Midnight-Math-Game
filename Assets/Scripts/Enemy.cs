using UnityEngine;
using TMPro;
using UnityEngine.AI;



public class Enemy : MonoBehaviour
{
    string equation;
    int answer;
    public Option[] options;
    private int correctOptionIndex;
    public TMP_Text equationText;

    private float attackCooldown = 5f;
    private float attackTimer = 0f;
    [SerializeField] private AudioSource attackAudioSource;
    private NavMeshAgent agent;

    void Start()
    {
        BasicAddition();
        correctOptionIndex = Random.Range(0,options.Length);
        for (int i = 0; i < options.Length; i++)
        {
            if (i == correctOptionIndex)
            {
                options[i].SetOptionText(answer.ToString());
                options[i].SetOptionID(i);
            }
            else
            {
                int wrongAnswer;
                do
                {
                    wrongAnswer = answer + Random.Range(-10, 10);
                } while (wrongAnswer == answer);
                options[i].SetOptionText(wrongAnswer.ToString());
                options[i].SetOptionID(i);
            }
        }
        agent = GetComponent<NavMeshAgent>();
    }

    private void BasicAddition()
    {
        int a = Random.Range(1, 100); ;
        int b = Random.Range(1, 100); ;
        answer = a + b;
        equation = a + " + " + b;
        equationText.text = equation;
        // Debug.Log("The sum of " + a + " and " + b + " is: " + answer);
    }

    public void CheckAnswer(int selectedOptionIndex)
    {
        if (selectedOptionIndex == correctOptionIndex)
        {
            AudioManager.Instance.CorrectShot();
            // Debug.Log("Correct!");
            Destroy(gameObject);
        }
        else
        {
            AudioManager.Instance.IncorrectShot();
            agent.speed = agent.speed + 1;
            attackCooldown = attackCooldown / 2; 
            // Debug.Log("Incorrect. The correct answer is: " + answer);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Health obj = other.GetComponentInParent<Health>();

        if (obj != null)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                attackAudioSource.Play();
                obj.TakeDamage(10);
                attackTimer = attackCooldown;
            }
        }
    }

    
}
