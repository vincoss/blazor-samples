export function getElementDOMRect(element)
{
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
    DotNet.invokeMethodAsync("Maabot.FillNumber.RazorLibrary", 'OnBrowserResizeHandler', window.innerWidth, window.innerHeight).then(data => data);
};