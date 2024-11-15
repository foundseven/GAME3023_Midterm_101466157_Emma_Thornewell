# Custom Calendar System

This project implements a custom calendar system with special events, dynamic lighting, audio integration, and seasonal changes.

# Features

**Calendar Events**

-Event Triggers: Handles special events on specific dates using CalenderEventSO and NewEventSO.

-Dynamic Prefabs: Attach and display prefabs (e.g., hats) during events.

-Seasonal Highlights: Adjust calendar day colors for events.

**Lighting Adjustments**

-Day/Night Cycle: Gradual light intensity transitions.

-Custom Event Brightness: Special events (e.g., Summer Solstice) boost light brightness.

**Audio Integration**

-Background Music: Plays audio during events if audio clips are assigned.

-Sound Management: Prevents overlapping sounds.

# Code Overview

**Core Scripts**
CalenderManager

-Handles time, date, and calendar updates.

-Manages light transitions based on day/night cycles.

CalenderEventSO

-Base class for calendar events.

-Allows each event to define unique behaviors.

NewEventSO

-Extends CalenderEventSO for user-defined events.

-Supports prefabs, sound effects, and lighting changes.

SummerSolsticeSO

-Example of a special event with custom brightness logic.

# Setup

**Event Creation**

-Right-click in the Assets folder and select Create > CalendarEvents > NewEvent.

-Assign a date, prefab, and/or audio.

**Prefabs**

Attach prefabs to events to display dynamic objects like hats or decorations.

# Special Thanks

A big thank you to Dan Pos for helping me get my feet on the ground with this one. 

-Video reference here: https://youtu.be/wTH5iwO4n0s?si=E3VPLeug5ePqx6mQ
Lighting

Customize brightness levels for special events using light2D.intensity.
Audio

Add audio clips to events for unique background music.
