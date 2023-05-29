
#TODO: review
https://www.youtube.com/watch?v=hqt08YOW6m4
https://stackoverflow.com/questions/17411991/html5-canvas-rotate-image/43155027#43155027
https://medium.com/@jgoz/game-on-on-your-browser-1e7e903dac16
https://codepen.io/juliangarnier/pen/gmOwJX
https://stackoverflow.com/questions/38901951/canvas-vs-svg-for-games

## Resources
https://codeincomplete.com/articles/javascript-tetris/
https://expediteapps.net/2020/01/20/blazor-and-svg/
https://developer.mozilla.org/en-US/docs/Web/SVG
https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/onclick
https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute
https://css-tricks.com/svg-text-typographic-designs/
http://bl.ocks.org/mjromper/95fef29a83c43cb116c3

find blazor project or game


## Add samples
canvas
webgl
css
skia
+div
+svg

## Size change
if the width is larger than height use that one if other way use height
what to do if orientation change?
	well the x position must be relative? how about y position well that too
	use percent
windows resize and orientation change
	see the solution there
	see the sample ThreeView.razor always retrieves windows size
the xy shall be percent and reflect screen size
see viewport sample
	vw,vh

+need to get viewport size in blazor (BoundingClientRect) does that
	
canvas
	start above canvas
		example item size 50x50, will start -50 above canval
	percent all xy
	increment Y +1 until reach can height + item size
	increment X +- to have item floating right or left like a ship (nice to have)
	item must not touch left or right size
	when reach bottom
		remove the item
	whow many items show per canvas?
		it depends on canvas and item size
		canvas height / item height
	must get the larger size of the canvas to for the size
	each interval tick increment wisible items Y 
	find how to virtualize items in html

## Resources
https://www.pmichaels.net/2021/09/11/creating-a-game-in-blazor-part-1-moving-objects/
https://github.com/exceptionnotfound
https://gamedev.stackexchange.com/
https://www.codeandweb.com/physicseditor
https://github.com/mizrael/BlazorCanvas
