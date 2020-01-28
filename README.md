# Dungeon Quest
Dungeon Quest is an isomectric multiplayer labyrinth game where an Adventurer, in quest of treasures, is hunted by Hunters.  
This game can be played on a local network thanks to an HTTP Server hosted by one of the player on his computer. 

## Prerequisites

Simply [download the project](https://github.com/Je5ter/Dungeon-Quest/archive/master.zip).

## Installation

:warning: Dungeon Quest requires **Python v3.5.4** to run (*not yet tested with higher versions of Python*)  

### Using Conda

If you prefer, there is an easy way of installing **Python 3.5.4** and **Kivy** (with all dependencies) thanks to the **Conda** package manager. We recommend to use the lite version of **Anaconda**, wich is called **Miniconda**. To install this environnement, please check [this page](https://docs.conda.io/projects/conda/en/latest/user-guide/install/). 

Once the Conda environnement is installed with Python 3.5.4, run the following command inside a command prompt :
```sh
conda install kivy -c conda-forge
```

### Windows

First of all, go to the following [official Python website](https://www.python.org/downloads/release/python-354/) and download your corresponding architecture version.

#### Python installation
During the Python installation setup, make sure that the `Add Python 3.5 to PATH` checkbox is checked, then click on `Install now` and let the installer do the rest.
To check if python is correclty installed, run the following command into a command prompt :
```sh
python --version
```

ℹ *If this command leads to the Windows Store, you needs to **remove Python from the `Application execution aliases`** in the Windows settings. For more information, please refer to [this post](https://superuser.com/questions/1437590/typing-python-on-windows-10-version-1903-command-prompt-opens-microsoft-stor)*


#### Kivy installation
Once Python 3.5.4 is installed, we can focus on the Kivy installation process.

At this point, we need to install the latest version of PIP, the Python's package manager.
```sh
python -m pip install --upgrade pip wheel setuptools virtualenv
```

Then install kivy's dependencies.
```sh
python -m pip install docutils pygments pypiwin32 kivy_deps.sdl2==0.1.* kivy_deps.glew==0.1.*
python -m pip install kivy_deps.gstreamer==0.1.*
```

Finally, install kivy.
```sh
python -m pip install kivy==1.11.1
```

ℹ *For further informations about Kivy 1.11.1, please check this [website](https://kivy.org/doc/stable/gettingstarted/installation.html).*

### Linux and MacOS

Python 3.5.4 installers can be found on the [official Python website](https://www.python.org/downloads/release/python-354/)

Installation guide for Kivy on these OS can be found there : 
* [MacOS](https://kivy.org/doc/stable/installation/installation-osx.html)
* [Linux](https://kivy.org/doc/stable/installation/installation-linux.html).

## Running the game
The game starts with the `main.py` script located in the game folder. 
There is two ways of running this script : 
* Open the script with the Python executable (Right-click on the script, then `Open With`, then select `python.exe`)
* From a commmand prompt opened in the game folder, run the following command : 
```sh
python main.py
```

## Authors 

* **CAIL Alexandre** - _Gameplay and network developper_
* **GALAN Yann** - _Level design and Game User Interface developper_

## License

This is an **Open-source** project.

## About the game
This game is a school porject so it's not totally finished and will certainly not receive any update.
We share our work for non-commercial purposes.
