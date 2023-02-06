using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUIManager : MonoBehaviour
{
    private PlayerInventory pInventory;
    private ItemLevelUp itemLevelUp;
    private int[] alreadySelectedIndices = new int[3];

    public GameObject levelUpWindow;
    [SerializeField] private Button[] itemButtons = new Button[3];
    [SerializeField] private Image[] itemThumbnailsUI = new Image[3];
    [SerializeField] private TextMeshProUGUI[] itemDescriptionsUI = new TextMeshProUGUI[3];
    [SerializeField] private TextMeshProUGUI[] itemEffectsUI = new TextMeshProUGUI[3];
    [SerializeField] private Sprite[] itemSprites = new Sprite[11];

    void Start()
    {
        pInventory = GameObject.Find("Wizard").GetComponent<PlayerInventory>();
        itemLevelUp = GetComponent<ItemLevelUp>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && PlayerStats.level < PlayerStats.maxLevel)
        {
            LevelUp();
        }

        if (PlayerStats.experiencePoints >= PlayerStats.expToNextLevel)
        {
            LevelUp();
            PlayerStats.experiencePoints = 0;
            PlayerStats.expToNextLevel += 10;
        }
    }

    //Enables the Level Up UI, clears the buttons of whatever listeners they had from the previous level up, and creates three level up choices.
    private void LevelUp()
    {
        Time.timeScale = 0.0f;
        levelUpWindow.SetActive(true);

        itemButtons[0].onClick.RemoveAllListeners();
        itemButtons[1].onClick.RemoveAllListeners();
        itemButtons[2].onClick.RemoveAllListeners();

        CreateLevelUpChoice(0);
        CreateLevelUpChoice(1);
        CreateLevelUpChoice(2);

        //Clears the alreadySelectedIndices array so that the next batch of level up choices aren't compared against the previous choices.
        for (int i = 0; i < alreadySelectedIndices.Length; i++)
        {
            alreadySelectedIndices[i] = 0;
        }
    }

    //Sets thumbnail, description, and effect description based on item ID for the the given level up choice UI element.
    private void CreateLevelUpChoice(int choiceIndex)
    {
        int itemIndex = GetRandomItemIndex(choiceIndex);

        itemThumbnailsUI[choiceIndex].sprite = itemSprites[itemIndex];

        itemDescriptionsUI[choiceIndex].text = ItemUIInfo.itemDescriptions[itemIndex];
        
        if (itemIndex == 0)
        {
            itemEffectsUI[choiceIndex].text = "";

            return;
        }

        //If the item is a passive item, refer to the array of passive item effects. Passive effects are constant but weapon effects are level dependent.
        if (itemIndex > 5)
        {
            itemEffectsUI[choiceIndex].text = "Level " + pInventory.itemLevels[itemIndex] + " -> " + (pInventory.itemLevels[itemIndex] + 1) + " : " + ItemUIInfo.passiveEffects[itemIndex - 5];

            itemButtons[choiceIndex].onClick.AddListener(() => itemLevelUp.LevelUpPassiveItem(itemIndex));
        }

        //If the item is a weapon, use this dumbass formula to find the index for the array of weapon effect descriptions.
        else
        {
            int weaponEffectsItemIndex = ((itemIndex - 1) * 5) + pInventory.itemLevels[itemIndex] + 1;

            itemEffectsUI[choiceIndex].text = "Level " + pInventory.itemLevels[itemIndex] + " -> " + (pInventory.itemLevels[itemIndex] + 1) + " : " + ItemUIInfo.weaponEffects[weaponEffectsItemIndex];

            itemButtons[choiceIndex].onClick.AddListener(() => itemLevelUp.LevelUpWeapon(itemIndex));
        }
        
    }

    private int GetRandomItemIndex(int choiceIndex, int attempts = 0)
    {
        int index = Random.Range(1, 11);

        if (attempts > 50)
        {
            return 0;
        }

        //Prevents item IDs that have already been chosen for a different level up choice from being selected again.
        for (int i = 0; i < alreadySelectedIndices.Length; i++)
        {
            if (index == alreadySelectedIndices[i])
            {
                return GetRandomItemIndex(choiceIndex, attempts + 1);
            }
        }

        //If the player has the item corresponding to index and that item is max level, ignore the generated index and run the function again. We don't want max level items to show up.
        if (pInventory.hasItems[index] && pInventory.itemLevels[index] == 5)
        {
            return GetRandomItemIndex(choiceIndex, attempts + 1);
        }

        //If the item index is less than 6 (which makes it a weapon index) and the player has the max amount of weapons, try again.
        if (index < 6 && PlayerStats.currentWeapons == PlayerStats.maxWeapons)
        {
            index = PlayerStats.ownedWeapons[Random.Range(0, PlayerStats.maxWeapons)];

            if (pInventory.itemLevels[index] >= 5)
            {
                return GetRandomItemIndex(choiceIndex, attempts + 1);
            }

            for (int i = 0; i < alreadySelectedIndices.Length; i++)
            {
                if (index == alreadySelectedIndices[i])
                {
                    return GetRandomItemIndex(choiceIndex, attempts + 1);
                }
            }

            alreadySelectedIndices[choiceIndex] = index;

            return index;
        }

        //If the item index is 6 or higher (which makes it a passive index) and the player has the max amount of passives, pick a random passive from the list of owned passives.
        if (index >= 6 && PlayerStats.currentPassives == PlayerStats.maxPassives)
        {
            index = PlayerStats.ownedPassives[Random.Range(0, PlayerStats.maxPassives)];

            if (pInventory.itemLevels[index] >= 5)
            {
                return GetRandomItemIndex(choiceIndex, attempts + 1);
            }

            for (int i = 0; i < alreadySelectedIndices.Length; i++)
            {
                if (index == alreadySelectedIndices[i])
                {
                    return GetRandomItemIndex(choiceIndex, attempts + 1);
                }
            }

            alreadySelectedIndices[choiceIndex] = index;

            return index;
        }

        alreadySelectedIndices[choiceIndex] = index;

        return index;
    }

}
