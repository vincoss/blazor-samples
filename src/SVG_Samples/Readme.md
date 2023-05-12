
## Temp
next 102 page


https://www.sarasoueidan.com/blog/svg-coordinate-systems/
https://css-tricks.com/scale-svg/
https://css-tricks.com/guide-svg-animations-smil/
https://css-tricks.com/svg-line-animation-works/
http://spgrade71110.blogspot.com/2010/11/order-of-operations_09.html
https://css-tricks.com/mega-list-svg-information/

## Tasks

text aling
	progress
	time

box sample
	Text
	font icon
	must scale any screen viewBox
	---
	must have right and wrong
	border
	focus current item (backgground colour or shade)

buttons
	click and tap effect
	this is possible with CSS styles

layout, button, progress, duration and other
	use rectangles

add all sample icons into blazor app to review on devices

html add font as a style and refrence in svg
	use that in css fontface
		then use the .css file to attach that to the whole .svg
pattern
	https://css-tricks.com/optimizing-svg-patterns/
	https://codepen.io/collection/DRMKdB?cursor=ZD0xJm89MSZwPTEmdj0yNw==

read css book
	where is the book???

read blazor documentation whole
read maui documentation whole
reuse the styles and components
	must create a sample
		styles
		svg elements
svg samples integrate with html
	test on multiple screens
		size
		orientation change
		use the larger value: height > width 
		must resize based on screen change
		test on windws app window resize UI must update according to screen changes
tools review and how to use
	incscape
	SVG VS Code
convert otf to woff
	wheter is required

convert font text to svg

review resources urls
review all samples
complete UI sample as one svg file


```
var base_w = 1024;
var base_h = 768;
var aspect = base_w / base_h ; // get the GAME aspect ratio
if (display_get_width() < display_get_height())
    {
    //portrait
    var ww = min(base_w, display_get_width());
    var hh = ww / aspect;
    }
else
    {
    //landscape
    var hh = min(base_h, display_get_height());
    var ww = hh * aspect;
    }
surface_resize(application_surface, ww, hh);

// Or
var base_w = 1024;
var base_h = 768;
var max_w = display_get_width();
var max_h = display_get_height();
var aspect = display_get_width() / display_get_height();
if (max_w < max_h)
    {
    // portait
     var VIEW_WIDTH = min(base_w, max_w);
    var VIEW_HEIGHT = VIEW_WIDTH / aspect;
    }
else
    {
    // landscape
    var VIEW_HEIGHT = min(base_h, max_h);
    var VIEW_WIDTH = VIEW_HEIGHT * aspect;
    }
camera_set_view_size(view_camera[0], floor(VIEW_WIDTH), floor(VIEW_HEIGHT))
view_wport[0] = max_w;
view_hport[0] = max_h;
surface_resize(application_surface, view_wport[0], view_hport[0]);

// Or
var base_w = 640;
var base_h = 480;
var aspect = display_get_width() / display_get_height();
if (aspect &gt; 1)
    {
    //landscape
    ww = base_h * aspect;
    hh = base_h;
    display_set_gui_maximise((display_get_width() / ww), (display_get_height() / hh), 0, 0);
    }
else
    {
    //portrait
    ww = base_w;
    hh = base_w / aspect;
    display_set_gui_maximise((display_get_width() / ww), (display_get_height() / hh), 0, 0);
    }

// An example of how you would then draw this to the GUI layer... 
draw_sprite(sprite0, 0, 64, 64);
draw_sprite(sprite0, 0, ww - 64, 64);
draw_sprite(sprite0, 0, ww - 64, hh - 64);
draw_sprite(sprite0, 0, 64, hh - 64);

```

```
global.res_width = 1920;
global.res_height = 1080;

var _ratio = global.res_width / global.res_height;
var _display_ratio = display_get_width() / display_get_height();

if (_display_ratio < _ratio){
    global.res_height = 1920; // Optional

    global.res_width = global.res_height * _display_ratio;
}
```

```
html, body {
  height: 100%;
}

body {
  margin: 0;
  padding: 0;
  background: #111;
  color: #eee;
  font: caption;
}

canvas {
  /* And see postBoot() in JS */
  width: 100%;
  height: 100%;
  object-fit: contain;
  /* <https://caniuse.com/#feat=object-fit> */
}

#version {
  position: absolute;
  left: 5px;
  top: 605px;
}

var config = {
  type: Phaser.AUTO,
  width: 800,
  height: 600,
  physics: {
    default: "arcade",
    arcade: {
      gravity: { y: 200 }
    }
  },
  scene: {
    preload: preload,
    create: create
  },
  callbacks: {
    postBoot: function (game) {
      // In v3.15, you have to override Phaser's default styles
      game.canvas.style.width = '100%';
      game.canvas.style.height = '100%';
    }
  }
};

function update(){
    (function() {
        const gameId = document.getElementById("game"); // Target div that wraps the phaser game
        gameId.style.width = '100%'; // set width to 100%
        gameId.style.height = '100%'; // set height to 100%
    })(); // run function
}
```


##------------------------------------------------- Done
tspan with text format and colour
These are not working with SVG
	<style>
	.material-symbols-outlined {
	  font-variation-settings:
	  'FILL' 0,
	  'wght' 400,
	  'GRAD' 0,
	  'opsz' 48
	}
	</style>
z index
	basically items order is as they are added on the screen
rewrite the svg performance with blazor
	make that simple
	just add child svg and change x,y on timer
		when reach x,y max or min, random next x,y (min or max)
		make is as ocean and use te fish fonts
buttons 0-9
	svg
progress
	0/0
duration time
rect radius
see attribute fill="url
find already created svg and review those
see overflow="visible"
see 'use', 'g', 'symbol' elements
reuse components
	with use, g and symbol elements
see open-iconic
Maui see GraphicsView
font scale to fit parent
font center


## Issues
font-variation-settings does not work, see font samples

## Running Issue 1
```
chrome Referrer Policy: strict-origin-when-cross-origin
Open command line
cd to C:\Program Files\Google\Chrome\Application
disable web security
chrome.exe --user-data-dir="C:/Chrome dev session" --disable-web-security
```

## Resolution
https://www.ios-resolution.com/

# Resources
https://developer.mozilla.org/en-US/docs/Web/SVG
https://css-tricks.com/scale-svg/
https://webdesign.tutsplus.com/tutorials/svg-viewport-and-viewbox-for-beginners--cms-30844
https://www.sarasoueidan.com/blog/svg-coordinate-systems/
http://xahlee.info/js/svg.html
https://github.com/BerndK/SvgToXaml
https://github.com/wieslawsoltes/SvgToXaml
https://github.com/wieslawsoltes/Svg.Skia
https://www.justinmccandless.com/demos/viewbox/index.html

## Inkscape
https://inkscape.org/
https://inkscape-manuals.readthedocs.io/en/latest/text.html
https://gamedev.stackexchange.com/questions/19832/tool-for-creating-complex-paths/19833#19833

### Fonts
https://github.com/google/material-design-icons/tree/master/font
https://developers.google.com/fonts/docs/material_icons
https://fonts.googleapis.com/icon?family=Material+Icons
https://developers.google.com/fonts/docs/material_symbols
https://medium.com/@aitareydesign/understanding-of-font-formats-ttf-otf-woff-eot-svg-e55e00a1ef2
https://jsfiddle.net/ykx7kp8L/121/
https://fontdrop.info/
https://github.com/google/fonts/
https://gero3.github.io/facetype.js/

### Font-Awesome
https://github.com/FortAwesome/Font-Awesome
https://fontawesome.com/v5/cheatsheet

## Icons
https://icomoon.io/app/#/select
https://elements.envato.com/graphics/icons
https://freesvg.org/
https://www.svgrepo.com/
https://visualpharm.com/
https://elements.envato.com/graphics/icons/properties-vector?_ga=2.217397456.779593838.1681905736-589534885.1681905736

## Tools
https://inkscape.org/
https://github.com/Yqnn/svg-path-editor
https://github.com/svg/svgo
https://jakearchibald.github.io/svgomg/
https://github.com/adobe-webplatform/Snap.svg