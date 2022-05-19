# AnimalFiveHead
This project serves as an introduction into the fullstack development world for myself. It features a simple game that I made up - AnimalFiveHead. The rules are as such:
* All player receive a single card to begin
* Each player can choose to chain another card upto a maximum of 5 cards
* Each card has a value attached to it and is added to create a total score
* If the adjacent card is of a higher ranked card, the lower card value is remove from the total. i.e. Leave, Caterpillar. A Caterpillar eats leave, and the leave value must be subtracted from the total. 
* The Keeper and Tourist (NPC) Player are a subset of the Player and follow the same summation logic. However they can protect certain card types from the negative summation
* The NPC players can only chain upto a maximumof 4 cards 

## Getting Started
Simply pull the source code from git into Visual Studio to build and run
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites
* Visual Studio 2022
* .Net SDK 6+
* Postman
* Sql Server Management Studio


## Resources
All images (cards, game splashes) were sourced from different sites and do not own them.

## ToDO
* WebClient UI
* Database Backend
* Refactoring

## Authors
* Sashen Govender

