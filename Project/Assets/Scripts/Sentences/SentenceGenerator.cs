using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SentenceGenerator : MonoBehaviour
{
    [SerializeField] TMP_Text t;
    [SerializeField] Button buttonPrefab;
    [SerializeField] GridLayoutGroup immidiateHolder;
    [SerializeField] GridLayoutGroup otherHolder;
    [SerializeField] int immidiateFontSize;
    [SerializeField] int otherFontSize;
    [SerializeField] string subjectColor;

    //[SerializeField] List<Subject> subjects;
    //[SerializeField] List<string> ;

    Subject currentSubject;
    List<Subject> currentDescriptors;
    string sentence;

    List<Subject> subjects;

    void Start()
    {
        subjects = Resources.FindObjectsOfTypeAll<Subject>().ToList();

        GenerateSentence(RandomSubject());
    }

    void GenerateSentence(Subject subject)
    {
        currentSubject = subject;

        SentenceFragment fragment = subject.GetRandomDescriptors();
        currentDescriptors = fragment.subjects;

        sentence = $"<color={subjectColor}>{subject.GetName}</color> {fragment.fragment}.";
        sentence.ToUpper();

        t.text = sentence;

        CreateButtons();
    }

    void CreateButtons()
    {
        for (int i = 0; i < immidiateHolder.transform.childCount; i++)
        {
            Destroy(immidiateHolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < otherHolder.transform.childCount; i++)
        {
            Destroy(otherHolder.transform.GetChild(i).gameObject);
        }

        Button sb = CreateButton(new UnityAction(() => { NewSentence(); }), immidiateHolder.transform);
        ChangeButtonText(sb, "Tell me about something else.", immidiateFontSize);

        // All connections to the subject
        for (int i = 0; i < currentSubject.connections.Count; i++)
        {
            Subject s = currentSubject.connections[i];

            if (currentDescriptors.Contains(s)) { continue; }

            Button b = CreateButton(new UnityAction(() => { GenerateSentence(s); }), otherHolder.transform);
            ChangeButtonText(b, $"Tell me about <color={subjectColor}>{s.GetName}</color>.", otherFontSize);
        }

        // We are using a subject to describe something in the sentence.
        if (currentDescriptors.Count > 0)
        {
            for (int i = 0; i < currentDescriptors.Count; i++)
            {
                Subject s = currentDescriptors[i];
                Button b = CreateButton(new UnityAction(() => { GenerateSentence(s); }), immidiateHolder.transform);
                ChangeButtonText(b, $"Tell me about <color={subjectColor}>{s.GetName}</color>.", immidiateFontSize);
            }
        }

        // We can learn more about this subject.
        if (currentSubject.descriptors.Length > 1)
        {
            Button b = CreateButton(new UnityAction(() => { GenerateSentence(currentSubject); }), immidiateHolder.transform);
            ChangeButtonText(b, $"Tell me more about <color={subjectColor}>{currentSubject.GetName}</color>.", immidiateFontSize);
        }
    }

    void ChangeButtonText(Button b, string text, int fsize)
    {
        TMP_Text t = b.GetComponentInChildren<TMP_Text>();
        t.text = text;
        t.fontSize = fsize;
    }

    Button CreateButton(UnityAction a, Transform parent)
    {
        Button b = Instantiate(buttonPrefab, parent);
        b.onClick.AddListener(a);
        return b;
    }

    Subject RandomSubject()
    {
        return subjects[Random.Range(0, subjects.Count)];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void NewSentence()
    {
        GenerateSentence(RandomSubject());
    }
}