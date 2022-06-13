

## Resources
https://codeincomplete.com/articles/javascript-tetris/
https://expediteapps.net/2020/01/20/blazor-and-svg/
https://developer.mozilla.org/en-US/docs/Web/SVG
https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/onclick
https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute
https://css-tricks.com/svg-text-typographic-designs/
http://bl.ocks.org/mjromper/95fef29a83c43cb116c3
https://cloudfour.com/thinks/svg-icon-stress-test/
https://css-tricks.com/high-performance-svgs/

div
canvas
svg
webgl
css

<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
   width="400px" height="327px" viewBox="0 0 400 327" enable-background="new 0 0 400 327" xml:space="preserve">
<rect fill="#DEF2FD" width="400" height="327"/>
<polygon opacity="0.2" fill="#7DACDC" enable-background="new    " points="0,327 200,173.797 400,327 "/>
<polygon opacity="0.2" fill="#7DACDC" enable-background="new    " points="91,327 291,280.292 491,327 "/>
<polygon opacity="0.2" fill="#7DACDC" enable-background="new    " points="-101,327 99,201 299,327 "/>
<g>
  <text transform="matrix(1 0 0 1 121.5 57.875)" fill="#0093C0" class="sans on" font-size="29.7691">GRAVES</text>
  <text transform="matrix(1 0 0 1 38.5483 114.1104)" fill="#016082" class="script on" font-size="97.8796">Mountain</text>
  
  <text transform="matrix(1 0 0 1 41.5005 141.4561)" fill="#0093C0" class="sans on" font-size="14.5332" letter-spacing="10">BLUEGRASS FESTIVAL</text>
</g>
</svg>

<svg xmlns="http://www.w3.org/2000/svg"
  xmlns:xlink="http://www.w3.org/1999/xlink">
  <svg x="10">
    <rect x="10" y="10" height="100" width="100"
        style="stroke:#ff0000; fill: #0000ff"/>
  </svg>
  <svg x="200">
    <rect x="10" y="10" height="100" width="100"
        style="stroke:#009900; fill: #00cc00"/>
  </svg>
</svg>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>SVG demo</title>
</head>

<body>

<div style="background-color: darkslategrey; width: 90vw; height: 90vh; margin-top:10px;" tabindex="0" @ref="mainDiv">
<svg xmlns="http://www.w3.org/2000/svg">
  <circle cx="100" cy="100" r="10" onclick="alert('You have clicked the circle.')" />
  <circle cx="100" cy="50" r="10" onclick="alert('You have clicked the circle.')" />
  <circle cx="100" cy="0" r="10" onclick="alert('You have clicked the circle.')" />
  <circle cx="0" cy="0" r="10" onclick="alert('You have clicked the circle.')" />
  <text x="25" y="55" 
        font-family="'Lucida Grande', sans-serif" 
        font-size="12">

    Regular ol' text here. Hi.

  </text>
  <rect x="50" y="50" fill="#DEF2FD" width="32" height="40"/>
  
  <polygon opacity="0.2" fill="#7DACDC" enable-background="new    " points="0,327 200,173.797 400,327 "/>
<polygon opacity="0.2" fill="#7DACDC" enable-background="new    " points="91,327 291,280.292 491,327 "/>
<polygon opacity="0.2" fill="#7DACDC" enable-background="new    " points="-101,327 99,201 299,327 "/>
</svg>

</div>


</body>

</html>
