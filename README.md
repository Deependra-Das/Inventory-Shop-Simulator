# Inventory and Shop Simulator

## Overview
This is a Unity game project that simulates **Inventory and Shop Mechanics** of RPG games. 
The goal for the player is to navigate through the menus to gather resources in their inventory, manage currency & inventory weight, and interact with the shop to buy or sell items.

---

## Gameplay Features

### UI Layout
- The menu screen is divided into two main UI sections:
  - **Shop Panel**: Displays items available for player to Buy.
  - **Inventory Panel**: Displays items that the player has gathered in their inventory & can Sell to the shop.

- Also, there are additional UI for the menu interactions:
  - **Filters Panel**: Displays filter buttons that player can interact to filter the data in inventory & shop based on item types.
  - **Currency Panel**: Displays the amount of currency the player currently holds.
  - **Item Details Panel**: Displays the details of the item that the player clicks on either in shop or inventory. Based on shop or inventory, the Action section will change to Buy or Sell.
  - **Notification Panel**: Dialogue box to display a notification to the player with a message & a button for acknowledgement.
  - **Confirmation Panel**: Dialogue box to asl for confirmation from player before a transaction. If Yes, the selected transaction will proceed.

### Item Details
Each item in the Shop & Inventory have the following attributes:
- **Item Name**: The name of the item.
- **Item Description**: A short description of the item & its importance & usage.
- **Rarity**: The rarity level of the item (VeryCommon < Common < Rare < Epic < Legendary).
- **Type**: The category of the item (Materials, Consumables, Weapons, Treasures).
- **Icon**: A visual representation of the item.
- **Buying Price**: The cost to purchase one item from the Shop.
- **Selling Price**: The value in currency received when selling one item to the Shop.
- **Weight**: The item's weight in lbs (used for inventory weight capacity).
- **Quantity**: The number of the item available either in Shop or Inventory.

### Resource Gathering & Inventory Rarity Value
- Player starts with empty inventory & no currency. Players needs to gather random items by clicking the **Gather Resources** button.
- **The Inventory Rarity** value starts at Common level i.e., initially the player can only gather resources of VeryCommon or Common tpyes. To increase that, player needs to buy items of higher rarity levels. But selling the highest rarity item from Inventory also decrease the Inventory Rarity Value.
- **Weight Restriction**: If the total weight of items in the inventory exceeds the maximum allowed weight (100 lbs), the player will be notified about that with a notification & the button to gather resources will be disabled until Inventory weight is brought back to below the maximum allowed weight.

### Buy & Sell Transactions
- Based on the item selected from Shop or Inventory, the action bar in the Item Details panel is setup with the item data.
- If player clicked on an item in Shop, the action bar will have option to buy items
- If player clicked on an item in Inventory, the action bar will have option to sell items
- After Transaction the quantity & currency will be updated accordingly & a notification will be shown about the transaction with success or failure message.

---

## Technical Implementation

### Architectural Design
This Inventory & Shop Simulator System mainly utilzes a combination of **MVC (Model-View-Controller)** architectures to ensure modular and scalable code. 
It also uses **Service Locator** & **Dependency Injection** to facilitate communication between various scripts and ensure a loosely coupled codebase.
Also for certain scenarios e.g. transactions, **Observer Design Pattern** has been used with func & action delegate listeners.

**GameService** initializes and manages core game services, including item, shop, inventory, UI, event, currency, and sound services. It creates and injects dependencies into these services at the start, ensuring proper functionality and integration across the game.

**UIService** manages the user interface for item transactions, including item details display, transaction confirmation, and inventory management. It interacts with various services like EventService, ShopService, and CurrencyService, handles user inputs such as button clicks, and updates the UI accordingly, including item information, transaction quantities, and inventory weight. The class also manages sound effects and notifications related to user actions.

**FilterButtonView** manages a UI button for filtering items based on their type. It interacts with the EventService to trigger item type filtering and with the SoundService to play a sound effect on button click, updating its visual state when the filter is selected.

**ShopService** manages the shop's data and interactions, initializing with item and event services, and populating the shop's inventory. It provides methods to retrieve item quantities and populate shop data through the ShopController.

**ShopController** handles the shop's functionality, including item filtering, buying, and selling. It manages event listeners, updates the UI based on item availability, and maintains a list of shop items with quantities, while ensuring items are added and displayed based on the selected filters.

**ShopModel** manages the list of items in the shop, allowing items to be added or removed. It holds a reference to the ShopController and provides access to the shop's item list

**InventoryService** initializes and manages the inventory system, linking the InventoryController, ItemService, and EventService. It provides methods for retrieving item quantities and inventory weight.

**InventoryController** handles inventory operations, including filtering, gathering resources, and selling or buying items. It manages inventory data, updates item quantities, and checks for inventory weight limits while updating the UI and event system accordingly.

**InventoryModel** stores and manages inventory items, including calculating the current inventory weight and ensuring it doesn't exceed the maximum weight. It tracks the list of items, allows adding/removing items, and provides data for the inventory controller.

**ItemService** handles the creation of ItemController objects using item data and event services. It provides an Initialize method for setting up dependencies and a CreateItem method to generate item controllers for different UI panels.

**ItemController** manages the connection between the ItemModel (data) and ItemView (UI), handling item interactions such as button clicks and updating item quantities. It also triggers events through the EventService to update the UI and item data accordingly.

**ItemModel** class holds the data related to an item, such as its name, icon, rarity, prices, and quantity, and provides methods to update the quantity and rarity background. It is controlled by an ItemController that interacts with the UI and handles item updates.

**ItemView** handles the UI representation of an item, including displaying its icon, rarity, and quantity. It interacts with the ItemController to update the UI when the item data changes, such as updating the quantity or rarity image.

**CurrencyService** manages the player's currency, allowing adding and subtracting currency, and ensures it never goes below zero. It listens to events for selling and buying items, adjusting the currency accordingly.

**EventService** manages a series of event controllers for various in-game actions, such as item creation, button clicks, inventory updates, and buying/selling items. It facilitates event-driven communication between components like UI, inventory, and currency systems.

**EventController** is a generic event handler that allows adding/removing listeners and invoking events with multiple arguments. It supports both void and return-type events, ensuring flexible event-driven communication within the system.

**SoundService** manages the playback of sound effects (SFX) and background music (BGM) using AudioSource components, with sound clips sourced from a SoundScriptableObject. It allows for playing and looping both SFX and BGM based on the sound type.

### Scriptable Objects

**ItemScriptableObject** defines an item with various properties like name, icon, type, rarity, prices, weight, quantity, and description, which can be managed via the Unity Editor which provides a convenient way to store and configure item data for easy use within the game.

**SoundScriptableObject** holds an array of Sounds (a struct that pairs a SoundType with an AudioClip) and is used to manage sound assets in the game which allows for easy organization and assignment of audio clips for different sound types via the Unity Editor.

**ItemDatabaseScriptableObject** holds a list of ItemScriptableObject instances, which allows to manage and store multiple items in a centralized database within Unity's editor. This scriptable object provides a convenient way to access and organize a collection of item data.

---

## How to Play
1. Launch the game and explore the Shop and Inventory sections.
2. Since there is no currency & no items in inventory, click the Gather Resources button to collect random items in Inventory.
3. Manage the total item weight of the inventory so that it stays within the maximum weight limit.
4. If your inventory has space for items, you can keep gathering random items. (Note: The items gathered will be always of the level of the cumulative rarity value of your inventory items)
5. Use scroll bars in the Inventory section or filters to find the item you want to sell from inventory.
6. Click on the item to see the details. If you want to sell the item, select a quantity thats allowed & click on Sell Button. Click Yes on the confirmation dialogue box to proceed with the Sell Transaction.
7. Now that you have some currency, go to shop Section & try to buy items thats within your currency & weight limit.
8. Your goal is to repeat the above steps to get items from the shop that have high rarity so that you can gather more rare items randomly.

---

## Architecture Block Diagram

---

## Playable Build
https://outscal.com/iamdeep75/game/play-inventory-shop-simulator-system-game
---

## Watch Gameplay
https://youtu.be/jq_UYKmiVgo
