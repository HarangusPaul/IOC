using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TaskDisplay : MonoBehaviour
{
    // Reference to the task title map (this GameObject will be shown/hidden)
    public GameObject taskTitleMap;

    // Reference to the empty GameObject (this will have multiple child objects)
    public GameObject triggerObject;
    
    public GameObject damageObject;
    
    public GameObject question;
    public GameObject questionText;
    
    public TextMeshProUGUI taskText;
    
    public Button button1;
    public Button button2;

    // The text that you want to set when clicked
    public string clickedText = "Task Completed!";

    // The proximity threshold for when to show the taskTitleMap
    public float proximityThreshold = 2f;

    private Transform child;
    private Transform childDmg;

    // The data structure to hold the question and answers (you can replace this with a file loading mechanism later)
    private Question[] questions = {
        new Question("Ce este necesar pentru a susține viața?", new string[] { "Aer, apă, hrană", "Nisip, pietre, lumină" }, "a"),
        new Question("Ce este un organism viu?", new string[] { "Un obiect care crește și se înmulțește", "O piatră mare și grea" }, "a"),
        new Question("Ce fac plantele cu dioxidul de carbon?", new string[] { "Îl elimină în aer", "Îl folosesc pentru fotosinteză" }, "b"),
        new Question("Care este rolul principal al rădăcinilor plantelor?", new string[] { "Absorb apă și substanțe nutritive", "Produc oxigen" }, "a"),
        new Question("Ce tip de animal este găina?", new string[] { "Mamifer", "Pasăre" }, "b"),
        new Question("Din ce sunt alcătuiți solzii peștilor?", new string[] { "Oase", "Material dur, protector" }, "b"),
    };

    // List to store all the child GameObjects of the triggerObject
    private List<Transform> childrenList = new List<Transform>();
    
    
    private List<Transform> damageObjects = new List<Transform>();

    private void Start()
    {
        // Ensure the taskTitleMap starts hidden
        if (taskTitleMap != null)
        {
            taskTitleMap.SetActive(false);
            question.SetActive(false);
            questionText.SetActive(false);
        }

        if (button1 != null)
        {
            button1.onClick.AddListener(OnButton1Click);
            button1.GetComponentInChildren<TextMeshProUGUI>().text = ""; // Set button text
        }

        if (button2 != null)
        {
            button2.onClick.AddListener(OnButton2Click);
            button2.GetComponentInChildren<TextMeshProUGUI>().text = ""; // Set button text
        }

        // Populate the children list at the start
        for (int i = 0; i < triggerObject.transform.childCount; i++)
        {
            Transform childTransform = triggerObject.transform.GetChild(i);
            childrenList.Add(childTransform);
        }
        
        for (int i = 0; i < damageObject.transform.childCount; i++)
        {
            Transform childTransform = damageObject.transform.GetChild(i);
            damageObjects.Add(childTransform);
        }
    }

    private void Update()
    {
        // Check if the taskTitleMap is close enough to any child object of the triggerObject
        if (IsCloseToAnyChild())
        {
            // If the taskTitleMap is within proximity, show it
            if (taskTitleMap != null)
            {
                taskTitleMap.SetActive(true);
            }

            // Check for clicks while the taskTitleMap is active
            if (Input.GetMouseButtonDown(0)) // Left mouse button or tap
            {
                HandleClick();
                question.SetActive(true);
                questionText.SetActive(true);
            }
        }
        else
        {
            // If the taskTitleMap is too far, hide it
            if (taskTitleMap != null)
            {
                taskTitleMap.SetActive(false);
                question.SetActive(false);
                questionText.SetActive(false);
            }
        }
    }

    private bool IsCloseToAnyChild()
    {
        // Get the position of the taskTitleMap (this GameObject)
        Vector2 taskTitleMapPosition = transform.position;

        int i=0;
        // Loop through the children stored in the list
        foreach (Transform childTransform in childrenList)
        {
            // Get the position of the current child
            Vector2 childPosition = childTransform.position;

            // Calculate the distance between the taskTitleMap and this child object
            float distance = Vector2.Distance(taskTitleMapPosition, childPosition);
            
            // If any child is within the proximity threshold, return true
            if (distance <= proximityThreshold)
            {
                child = childTransform;
                childDmg = damageObjects[i];
                return true;
            }

            i++;
        }

        // If none of the children are close enough, return false
        return false;
    }

    private void HandleClick()
    {
        // Extract the index from the name of the nearest child
        Debug.Log(0);
        string childName = child.name;
        int questionIndex = int.Parse(childName) - 1; // Assuming child names are "1", "2", etc.
        Debug.Log(2);
        // Ensure the question index is valid
        if (questionIndex >= 0 && questionIndex < questions.Length)
        {
            Question currentQuestion = questions[questionIndex];
            
            // Set the question text and button answers based on the question data
            Debug.Log(3);
            taskText.text = currentQuestion.question;
            Debug.Log(1);
            button1.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[0];
            button2.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[1];

            // Resize buttons to fit text
            ResizeButtonToFitText(button1);
            ResizeButtonToFitText(button2);
        }
    }

    private void ResizeButtonToFitText(Button button)
    {
        // Ensure that the button resizes according to the text inside it
        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        var layoutElement = button.GetComponent<LayoutElement>();

        if (layoutElement != null)
        {
            // Set the preferred width to auto-adjust based on the text
            layoutElement.preferredWidth = -1;
            layoutElement.preferredHeight = -1;
        }

        // Optionally, use ContentSizeFitter if you want dynamic resizing
        var contentSizeFitter = button.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter != null)
        {
            contentSizeFitter.SetLayoutHorizontal();
            contentSizeFitter.SetLayoutVertical();
        }
    }

    private void OnButton1Click()
    {
        CheckAnswer(0); // Button 1 corresponds to the first answer
    }

    private void OnButton2Click()
    {
        CheckAnswer(1); // Button 2 corresponds to the second answer
    }

    private void CheckAnswer(int buttonIndex)
    {
        // Find out which button was clicked and check if it's the correct answer
        string correctAnswer = questions[int.Parse(child.name) - 1].correctAnswer;
        Button clickedButton = buttonIndex == 0 ? button1 : button2;

        // Check if the clicked answer is correct
        if ((buttonIndex == 0 && correctAnswer == "a") || (buttonIndex == 1 && correctAnswer == "b"))
        {
            clickedButton.GetComponent<Image>().color = Color.green; // Set button color to green for correct answer

            // Hide the child GameObject after a correct answer with a delay
            StartCoroutine(WaitAndRemoveChild(child,childDmg, clickedButton));
        }
        else
        {
            clickedButton.GetComponent<Image>().color = Color.red; // Set button color to red for wrong answer

            // Optionally reset the color after a short delay
            StartCoroutine(ResetButtonColorAfterDelay(clickedButton));
        }
    }

    private IEnumerator WaitAndRemoveChild(Transform child,Transform dmg, Button clickedButton)
    {
        // Wait for 3 seconds before removing the child
        yield return new WaitForSeconds(3f);

        // Disable the GameObject after 3 seconds
        child.gameObject.SetActive(false);

        // Remove the child from the list
        childrenList.Remove(child);

        dmg.gameObject.SetActive(false);

        // Reset the button color after a short delay
        yield return new WaitForSeconds(1f);  // Waiting for a moment before resetting the button color
        ResetButtonColor(clickedButton);
    }

    private IEnumerator ResetButtonColorAfterDelay(Button clickedButton)
    {
        // Wait for 1 second before resetting the color
        yield return new WaitForSeconds(1f);
        ResetButtonColor(clickedButton);
    }

    private void ResetButtonColor(Button clickedButton)
    {
        // Reset button color to white or default color
        clickedButton.GetComponent<Image>().color = Color.white;
    }

    // Question class to hold the data
    public class Question
    {
        public string question;
        public string[] answers;
        public string correctAnswer;

        public Question(string question, string[] answers, string correctAnswer)
        {
            this.question = question;
            this.answers = answers;
            this.correctAnswer = correctAnswer;
        }
    }
}
