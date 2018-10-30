using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour {
    public Transform scrollItemContent;
    public GameObject itemPrefab;
    public InputField inputCharacterName;
    public InputField inputHp;
    public InputField inputCurrentHp;
    public InputField inputAc;
    public InputField inputCurrentAc;

	void Start () {
        this.inputCharacterName.text = PlayerPrefs.GetString("name");
        this.inputHp.text = PlayerPrefs.GetFloat("hp").ToString();
        this.inputCurrentHp.text = PlayerPrefs.GetFloat("currentHp").ToString();
        this.inputAc.text = PlayerPrefs.GetFloat("ac").ToString();
        this.inputCurrentAc.text = PlayerPrefs.GetFloat("currentAc").ToString();

        string[] itemList = PlayerPrefs.GetString("itemNames").Split(',');
        foreach (string itemName in itemList) {
            if (!string.IsNullOrEmpty(itemName)) {
                GameObject item = Instantiate(this.itemPrefab);
                item.transform.SetParent(this.scrollItemContent);
                item.transform.Find("Name").GetComponent<InputField>().text = itemName;

                string[] attributesList = PlayerPrefs.GetString(itemName).Split(':');

                if (attributesList.Length > 0) {
                    item.transform.Find("Amount").GetComponent<InputField>().text = attributesList[0];
                    if (attributesList.Length > 1) {
                        item.transform.Find("Cost").GetComponent<InputField>().text = attributesList[1];
                        if (attributesList.Length > 2) {
                            item.transform.Find("Weight").GetComponent<InputField>().text = attributesList[2];
                        }
                    }
                }
            }
        }
	}

    void OnApplicationQuit() {
        PlayerPrefs.SetString("name", this.inputCharacterName.text);
        PlayerPrefs.SetFloat("hp", float.Parse(this.inputHp.text));
        PlayerPrefs.SetFloat("currentHp", float.Parse(this.inputCurrentHp.text));
        PlayerPrefs.SetFloat("ac", float.Parse(this.inputAc.text));
        PlayerPrefs.SetFloat("currentAc", float.Parse(this.inputCurrentAc.text));

        string itemNames = "";

        foreach (Transform tr in this.scrollItemContent) {
            string name = tr.Find("Name").GetComponent<InputField>().text;
            if (!string.IsNullOrEmpty(name)) {
                string amount = tr.Find("Amount").GetComponent<InputField>().text;
                string cost = tr.Find("Cost").GetComponent<InputField>().text;
                string weight = tr.Find("Weight").GetComponent<InputField>().text;
                itemNames += name + ",";
                PlayerPrefs.SetString(name, amount + ":" + cost + ":" + weight);
            }
        }
        if (!string.IsNullOrEmpty(itemNames)) {
            itemNames.Remove(itemNames.Length - 1);
        }

        Debug.Log(itemNames);
        PlayerPrefs.SetString("itemNames", itemNames);
    }

    public void AddItem() {
        GameObject item = Instantiate(this.itemPrefab);
        item.transform.SetParent(this.scrollItemContent);
    }
}