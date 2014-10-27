Object Replacement Tool, v1.0.4
Developed by Tylah Heil at Silver Fox Games.

-------

About: 
Object Replacement Tool is designed to make replacing GameObjects in the editor a breeze. Got some shiny new assets from the 3D Artist to replace those drab, boring placeholders? replace them quickly and easily without having to reposition by hand. Just select an object, drag in the replacement and hit the Replace button, it's just that easy!

Object Replacement Tool also features the ability to add objects without replacing the selected reference, it even allows for mass object placement at incremental position, rotation, and scale.

-------

How to use Replace Object Tool:

- Select a GameObject in the Scene view or search for an object using the search field.
- Go to the 'GameObject' menu and select 'Replace Object…' to bring up the 'Replace Object' dialogue window.
- Drag a replacement Prefab from the Project window to the 'Replacement Object' slot within the Replace object window.
- Select a function to perform, you can choose to completely replace the selection or to add the replacement object at the selection's coordinates and leave the selection alone. 
- You have the option of transferring the Transform data (Position, Rotation, and Scale) of the selected object to the replacement object or resetting them to a default state.
- You can also set a transform offset for each added object.
- Click the context sensitive 'Replace Selection' or 'Add Selection' button and you're done.

-------

Release Notes: 

v1.0.4
- Added a search field: You can now search for objects by name and mass select them.
- Object Replacement Tool window is now dockable by default, you can easily change this back (instructions are in the Init function).

v1.0.3
- Fixed an error caused by 'Undo.RegisterFullObjectHierarchyUndo()' in Unity 4.5.

v1.0.2
- Added new Unity 4.3 undo operations, fixing a harmless debug warning.
- If a selected object is a child in the Hierarchy, and no Hierarchy Parameter is set, the new object will be added using world coordinated rather than local coordinates.
- Added additional comments to code.
- Additional minor bug fixes.

v1.0.1
- Fixed harmless warning caused by 'EditorGUILayout.ObjectField'.
- Added Hierarchy Parameters; Add or Replace child objects and retain parent/child relationships.
- Modified Minimum and Maximum window size limitations.

v1.0.0
- Initial Release

-------

If you have any questions or wish to request a feature for future releases of this product send us an email at contact@silver-fox-media.com with 'Replace Object Tool' as the subject line.

-------

Visit http://www.silverfoxgames.com/ for more information on our products. 