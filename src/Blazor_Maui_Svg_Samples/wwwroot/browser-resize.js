export function getElementDOMRect(element, param) {
    return element.getBoundingClientRect();
};

export function getInnerHeight()
{
    return window.innerHeight;
};

export function getInnerWidth()
{
    return window.innerWidth;
};

export function registerResizeCallback()
{
    window.addEventListener("resize", resized);
};

export function resized()
{
    DotNet.invokeMethodAsync("Blazor_Maui_Svg_Samples", 'OnBrowserResizeHandler', window.innerWidth, window.innerHeight).then(data =>
    {
        console.log(data);
    });
};
