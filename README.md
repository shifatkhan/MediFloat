# [Unity] Meditation app

<a name="toc"></a>
## Table of Contents

> 1. [Introduction](#introduction)
> 1. [Project Structure](#structure)
> 1. [Scripts](#scripts)
> 1. [Credit](#credit)

<a name="introduction"></a>
## 1. Introduction

As I was browsing through guided meditation apps, I didn't find one that would be as interactive as I hoped. Until, I played a famous game called [Celeste](https://www.celestegame.com/). At one point of the game, a character tries to calm down the Player by breathing in and out on cue:

![](https://raw.githubusercontent.com/shifatkhan/MediFloat/main/readme/celeste.gif)

This is a personal project to create a guided meditation application using the Unity engine. Most of the visuals were created from UI elements:

![](https://github.com/shifatkhan/MediFloat/blob/main/readme/preview.gif)

The application can be used only via mouse clicks or touch controls. Which makes it perfect for PC, Mobile and Web.

<a name="structure"></a>
## 2. Project Structure
Simple project structure for a simple application.
<pre>
Assets
    Audio
          sfx
    Prefabs
          Managers
          UI
    Resources // Mainly for IAP
    Scenes
    Script
          Game
          Managers
          UI
          Utils
    Sprites
</pre>

<a name="scripts"></a>
## 3. Scripts

#### Game folder
Let's start with the `Game` folder. This contains scripts related to gameplay (behaviour):
* Feather movement
* Guiding box movement
* Particle movement in the background
* Meditation breathing configs

#### Managers folder
The managers folder is for controllers. It contains backend code for dictating how the game will communicate with every other aspect of the application.

#### UI folder
Mainly code for UI related components. Generally contains Helper scripts that facilitates controlling UI elements.

#### Utils folder
This will contain helper scripts ranging from enums, to generic design patterns. Basically to facilitate coding.

<a name="credit"></a>
## 4. Credit
Application was created by me, Shifat Khan.

Assets used:
* UI icons are from [Google fonts](https://fonts.google.com/icons) with Open Source license.
* Feather drawn on Aesprite by me.
