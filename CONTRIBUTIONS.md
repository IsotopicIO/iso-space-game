# How to Contribute
Scroll down for Contribution Guidelines.

## Basic Github CLI Workflow
**Prerequisites:**
- Download and Install [Git Bash](https://git-scm.com/downloads)

Read the official github guide on contributions [here](https://github.com/firstcontributions/first-contributions)

**Tl;dr**
1. Fork the repository
2. Clone the repository locally: ``git clone "url from your forked repo"``
3. Create a new branch: ``git switch -c your-new-branch``
4. Make changes
5. Push Changes: ``git push -u origin your-branch-name``
6. Open pull request from github website


**Additional**
- Keep your forked repository up to date: https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/syncing-a-fork

&nbsp;
<hr style="border:2px solid gray">
&nbsp;

## Contribution Guidelines

This project will use [Github Issues](https://github.com/IsotopicIO/iso-space-game/issues) for feature requests, asking for help, reporting issues, etc.
Please add the appropriate labels when creating a new issue, and be as descriptive as possible.

**Coding Practices**
- Use **PascalCase** for public members of classes (e.g `public float MyNumber;`)
- Use **camelCase** for private members of classes (e.g. `private Transform myTransform;`)
- Do not clutter Awake/Start/Update. Isolate and organize logic of different "sub-components" in their own methods, and call these methods in Update or wherever needed.
- On new scripts, describe the usage of said script at the top using a comment.

**Naming Practices**
- `I_MyInterface`- "I_" prefix for interfaces.
- `E_MyEnum` - "E_" prefix for enums.
- `BaseMyClass` - "Base" prefix for base classes.
- `delegate OnStatusChangedHandler(object source, MyEventArgs args)` - "Handler" suffix for event handler, and "EventArgs" suffix for event args class.

**Script Description Comment template:**
```
////////////////////////////////////////////////////////////////////////////
//
//  This component will enable flight controls on the gameobject it is placed on,
//  giving the player steering control of the ship, using A, D or LeftArrow, RightArrow
//
//  Attach ShipController to the ship game object.              
//
////////////////////////////////////////////////////////////////////////////
```

**Check out [these](https://github.com/IsotopicIO/iso-space-game/issues/5) features that can be worked on, while the main ideas of the game are being defined**
