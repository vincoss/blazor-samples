export function getInnerHeight() {
    return window.innerHeight;
};

export function getInnerWidth() {
    return window.innerWidth;
};

export function registerResizeCallback() {
    window.addEventListener("resize", resized);
};

export function resized() {
    DotNet.invokeMethodAsync("BlazorWebAssemblyAppPWA_Svg_Samples", 'OnBrowserResizeHandler').then(data => data);
};