
div
skia
svg

need to get viewport size in blazor
if the width is larger than height use that one if other way use height
what to do if orientation change?
	well the x position must be relative? how about y position well that too

viewport sample
	vw,vh
background for div
	under water
	dino
	insect
	
canvas
	vertical times ramdom fall
	like escalator
	start above canvas
		example item size 50x50, will start -50 above canval
	increment Y +1 until reach can height + item size
	increment X +- to have item floating right or left like a ship (nice to have)
	item must not touch left or right size
	when reach bottom reset to start again
		x shall be random fill
	whow many items show per canvas?
		it depends on canvas and item size
		canvas height / item height
	must get the larger size of the canvas to for the size
	each interval tick increment wisible items Y 
	find how to virtualize items in html
		
Default blank canvas
	***** out canvas		
	00000	
	00000	
	00000	
	00000	
	00000	
	***** out canvas
		
Canvas with items
***** out canvas		
10000	
01000	
00100	
00010	
00001	
***** out canvas
		
		
		
## times
horizontal
	can use the arrows and walk to solve the value
 

## Testing
must test on large screen TV

## Resources
https://www.pmichaels.net/2021/09/11/creating-a-game-in-blazor-part-1-moving-objects/