using Microsoft.AspNetCore.Components;

namespace Blazor_Samples.Pages
{
    public class CodeBehindPageBase : ComponentBase
    {
        protected int currentCount = 0;

        protected void IncrementCount()
        {
            currentCount++;
        }
    }
}
