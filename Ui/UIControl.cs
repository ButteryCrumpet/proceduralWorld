using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    public SpellData spellData;

    public GameObject wordList;
    public GameObject wordInfoList;
    public Text spellName;
    public Text spellInfo;

    public GameObject wordInfo;
    public Button wordButton;
    public Button createSpell;
    public Button removeSpellWord;

    private Spell currentSpell;
    private List<SpellWord> selectedWords;

    void Start ()
    {
        selectedWords = new List<SpellWord>();

        createSpell.onClick.AddListener(CreateSpellClick);
        CreateWordButtons();
        gameObject.SetActive(false);
	}

    void WordButtonClick(SpellWord spellWord)
    {
        if (selectedWords.Count == 3)
        {
            selectedWords.RemoveAt(0);
        }

        selectedWords.Add(spellWord);
        UpdateWordInfoList();
        UpdateSpellInfo();
    }

    void CreateSpellClick()
    {
        SpellManager.CommitSpell(currentSpell);
    }

    void RemoveSpellWordClick(SpellWord spellWord)
    {
        selectedWords.Remove(spellWord);
        UpdateWordInfoList();
        UpdateSpellInfo();
    }

    void UpdateWordInfoList()
    {
        foreach (Transform child in wordInfoList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (SpellWord spellWord in selectedWords)
        {
            GameObject info = Instantiate(wordInfo);
            info.transform.SetParent(wordInfoList.transform, false);

            Text[] textBoxes = info.GetComponentsInChildren<Text>();
            textBoxes[0].text = spellWord.Name;
            textBoxes[1].text = spellWord.wordAsString;

            Button deleteButton = info.GetComponentInChildren<Button>();
            deleteButton.onClick.AddListener(delegate { RemoveSpellWordClick(spellWord); });
        }
    }

    void UpdateSpellInfo()
    {
        currentSpell = new Spell(selectedWords);
        spellName.text = currentSpell.Name;
        spellInfo.text = currentSpell.spellToString();
    }

    void CreateWordButtons()
    {
        foreach (SpellWord word in spellData.spellWords)
        {
            Button thisWordButton = Instantiate(wordButton);
            thisWordButton.transform.SetParent(wordList.transform, false);
            thisWordButton.name = word.Name;
            thisWordButton.GetComponentInChildren<Text>().text = word.Name;

            thisWordButton.onClick.AddListener(delegate { WordButtonClick(word); });
        }
    }
}
