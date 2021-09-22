# TerrariaAutoFisher

![Preview gif](https://j.gifs.com/36yGMO.gif)

# Latest Notes
- The program/code relies on the difference in the red color of a selected zone when fishing.
As such, fishing rods with red bobbers work the best, as they create the biggest difference when going below water.

# How to use as is

### Download the binaries 
Look to the Releases section of this repository.

### Building

To build the project yourself, you require .NET Framework 4.7.2.

---

# Motivation for making the project

Fishing in Terraria probably the most tedious bit the game offers.

# Using the code

The fish being caught is registered as a change in the color red, over a specified threshold, inside of the selected area.
Different fishing rods can produce different changes in the color red, so the threshold can be adjusted as needed.

A transparent overlay in the top left corner will be displayed, containing instructions about which numpad keys to use for what.
The overlay does not contain all of the possible interactions.

- Num4: Treat other keys as active.
- Num5: Set initial or final coordinate of the overlay rectangle at the current mouse position.
- Num2: Adjust initial and final coordinates by the current mouse position.
- F: Start fishing using the overlay.
- Num0: Toggle automatic shooting (left click).
- Num1: Make the red threshold value the current red difference.
- Num3: Obtain Terraria handle.

# TODOs

- Code style improvements (written when I was just learning about platform invocation, threading, ...)
- Perf. improvements with the screen capture
- Documentation
- Customizability of keybindings
- Greater customizability of runtime behavior 
