using OpenQA.Selenium;


namespace TestRail.Elements
{
    public class Button
    {
        private readonly UiElement _uiElement;

        public Button(IWebDriver driver, By locator)
        {
            _uiElement = new UiElement(driver, locator);
        }
        public void Click() => _uiElement.Click();
        public void Submit() => _uiElement.Submit();
        public bool Displayed => _uiElement.Displayed;
        public bool Enabled => _uiElement.Enabled;
        public string Text => _uiElement.Text.Trim();
        public string GetAttribute(string attributeName) => _uiElement.GetAttribute(attributeName);
    }
}
